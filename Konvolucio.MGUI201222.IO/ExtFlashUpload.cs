namespace Konvolucio.MGUI201222.IO
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Diagnostics;

    public sealed class ExtFlashUpload : IDisposable
    {
        public event RunWorkerCompletedEventHandler Completed;
        public event ProgressChangedEventHandler ProgressChange
        {
            add { _bw.ProgressChanged += value; }
            remove { _bw.ProgressChanged -= value; }
        }

        readonly BackgroundWorker _bw;
        readonly AutoResetEvent _waitForDoneEvent;

        bool _disposed = false;

        class BackroundWorkerArg
        {
            public int Address { get; private set; }
            public byte[] Data { get; private set; }
            public readonly BackgroundWorker Worker;
            public BackroundWorkerArg(BackgroundWorker worker, int address, byte[] data)
            {
                Worker = worker;
                Address = address;
                Data = data;
            }
        }

        readonly MemoryInterface _mem;

        public ExtFlashUpload(MemoryInterface memory)
        {
            _mem = memory;
            _bw = new BackgroundWorker();
            _bw.RunWorkerCompleted += RunWorkerCompleted;
            _bw.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            _waitForDoneEvent = new AutoResetEvent(false);
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Completed != null)
                Completed(this, e);
        }

        public void Begin(int address, byte[] data)
        {
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
            _bw.RunWorkerAsync(new BackroundWorkerArg(_bw, address, data));
        }

        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (e.Argument as BackroundWorkerArg).Worker;
            byte[] data = (e.Argument as BackroundWorkerArg).Data;
            int address = (e.Argument as BackroundWorkerArg).Address;
            int frameSize = _mem.FrameSize;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            int offset = 0;
            int frames = 0;
            try
            {
                int blocks = data.Length / _mem.ExtFlashBlockSize;

                if((data.Length%_mem.ExtFlashBlockSize)!=0)
                    blocks++;

                int er_addr = address;
                for (int i = 0; i < blocks; i++)
                {
                    long timestamp = DateTime.Now.Ticks;
                    _mem.ExtFlashBlockErase(er_addr);
                    do
                    {
                        if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                            throw new TimeoutException();
                    } while (_mem.ExtrnalFlashIsBusy());
                    bw.ReportProgress(1, $"EXTERNAL FLASH BLOCKS: {i} ERASING... ");
                    er_addr += _mem.ExtFlashBlockSize;
                }
                do
                {
                    if (data.Length - offset >= frameSize)
                    {
                        byte[] temp = new byte[frameSize];
                        Buffer.BlockCopy(data, offset, temp, 0, frameSize);
                        _mem.ExtFlashWrite(address, temp);
                        address += frameSize;
                        offset += frameSize;
                    }
                    else
                    {
                        byte[] temp = new byte[data.Length - offset];
                        Buffer.BlockCopy(data, offset, temp, 0, data.Length - offset);
                        _mem.ExtFlashWrite(address, temp);
                        address += (data.Length - offset);
                        offset += (data.Length - offset);
                    }
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    bw.ReportProgress((int)(((double)offset / data.Length) * 100.0), $"EXTERNAL FLASH UPLOAD STATUS: { data.Length } / { offset }bytes ({frames++}).");
                } while (offset != data.Length);

                watch.Stop();

                if (!bw.CancellationPending)
                {

                    bw.ReportProgress(0, $"EXTERNAL FLASH UPLOAD COMPLETED {watch.ElapsedMilliseconds / 1000.0} sec");
                    e.Result = data;
                }
                else
                {
                    bw.ReportProgress(0, $"EXTERNAL FLASH UPLOAD ABORTED {watch.ElapsedMilliseconds / 1000.0} sec");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _mem.IntFlashLock();
                _waitForDoneEvent.Set();
            }
        }

        public void Abort()
        {
            if (_bw.IsBusy)
            {
                _bw.CancelAsync();
                _waitForDoneEvent.WaitOne();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_bw.IsBusy)
                {
                    _bw.CancelAsync();
                    _waitForDoneEvent.WaitOne();
                }
            }
            _disposed = true;
        }

    }
}


namespace Konvolucio.MGUI201222.IO
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Diagnostics;

    public sealed class ExtFlashDownload : IDisposable
    {
        public event RunWorkerCompletedEventHandler Completed
        {
            remove { _bw.RunWorkerCompleted -= value; }
            add { _bw.RunWorkerCompleted += value; }
        }
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
            public int Size { get; private set; }
            public readonly BackgroundWorker Worker;
            public BackroundWorkerArg(BackgroundWorker worker, int address, int size)
            {
                Worker = worker;
                Address = address;
                Size = size;
            }
        }

        readonly MemoryInterface  _memory;

        public ExtFlashDownload(MemoryInterface memory)
        {
            _memory = memory;
            _bw = new BackgroundWorker();
            _bw.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            _waitForDoneEvent = new AutoResetEvent(false);
        }

        public void Begin(int address, int size)
        {
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
            _bw.RunWorkerAsync(new BackroundWorkerArg(_bw, address, size));
        }

        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (e.Argument as BackroundWorkerArg).Worker;
            int size = (e.Argument as BackroundWorkerArg).Size;
            int address = (e.Argument as BackroundWorkerArg).Address;

            int frameSize = _memory.FrameSize;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            int offset = 0;
            int frames = 0;
            byte[] data = new byte[size];
            try
            {
                do
                {
                    byte[] temp;
                    if (size - offset >= frameSize)
                    {
                        temp = _memory.ExtFlashRead(address, frameSize);
                        Buffer.BlockCopy(temp, 0, data, offset, frameSize);
                        address += frameSize;
                        offset += frameSize;
                    }
                    else
                    {
                        temp = _memory.ExtFlashRead(address,  size - offset);
                        Buffer.BlockCopy(temp, 0, data, offset, size - offset);
                        address += (size - offset);
                        offset += (size - offset);
                    }
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    bw.ReportProgress((int)(((double)offset / data.Length) * 100.0), $"EXTERNAL FLASH DOWNLOAD STATUS: {data.Length} / {offset} ({frames++}).");
                } while (offset != size);

                watch.Stop();

                if (!bw.CancellationPending)
                {

                    bw.ReportProgress(0, $"EXTERNAL FLASH DOWNLOAD COMPLETED {watch.ElapsedMilliseconds / 1000.0} sec");
                    e.Result = data;
                }
                else
                {
                    bw.ReportProgress(0, $"EXTERNAL FLASH DOWNLOAD ABORTED {watch.ElapsedMilliseconds / 1000.0} sec");
                }
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
            finally
            {
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

        private void Dispose(bool disposing)
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

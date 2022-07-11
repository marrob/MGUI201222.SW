namespace Konvolucio.MGUI201222.IO
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Diagnostics;

    public sealed class IntFlashUpload : IDisposable
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
            public byte[] Data { get; private set; }
            public readonly BackgroundWorker Worker;
            public BackroundWorkerArg(BackgroundWorker worker, int address, byte[] data)
            {
                Worker = worker;
                Address = address;
                Data = data;
            }
        }

        readonly Memory _memory;

        public IntFlashUpload(Memory memory)
        {
            _memory = memory;
            _bw = new BackgroundWorker();
            _bw.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            _waitForDoneEvent = new AutoResetEvent(false);
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
            int frameSize = _memory.FrameSize;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            int offset = 0;
            try
            {
                do
                {
                    if (data.Length - offset >= frameSize)
                    {
                        byte[] temp = new byte[frameSize];
                        Buffer.BlockCopy(data, offset, temp, 0, frameSize);
                        _memory.IntFlashWrite((UInt32)address, temp);
                        address += frameSize;
                        offset += frameSize;
                    }
                    else
                    {
                        byte[] temp = new byte[data.Length - offset];
                        Buffer.BlockCopy(data, offset, temp, 0, data.Length - offset);
                        _memory.IntFlashWrite((UInt32)address, temp);
                        address += (data.Length - offset);
                        offset += (data.Length - offset);
                    }
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    bw.ReportProgress((int)(((double)offset / data.Length) * 100.0), "STATUS: " + data.Length.ToString() + "/" + offset.ToString() + ".");
                } while (offset != data.Length);

                watch.Stop();

                if (!bw.CancellationPending)
                {

                    bw.ReportProgress(0, "COMPLETE Elapsed:" + (watch.ElapsedMilliseconds / 1000.0).ToString() + "s");
                    e.Result = data;
                }
                else
                {
                    bw.ReportProgress(0, "ABORTED Elapsed:" + (watch.ElapsedMilliseconds / 1000.0).ToString() + "s");
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

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                if (_bw.IsBusy)
                {
                    _bw.CancelAsync();
                    _waitForDoneEvent.WaitOne();
                }
            }

            // Free any unmanaged objects here. 
            //
            _disposed = true;
        }

    }
}

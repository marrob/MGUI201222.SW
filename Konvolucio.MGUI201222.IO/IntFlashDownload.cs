
namespace Konvolucio.MGUI201222.IO
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Diagnostics;

    public sealed class IntFlashDownload : IDisposable
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
        readonly AutoResetEvent _waitForDelayEvent;

        bool _disposed = false;

        class BackroundWorkerArg
        {
            public int Delay { get; private set; }
            public int BlockSize { get; private set; }
            public int Address { get; private set; }
            public int Size { get; private set; }
            public readonly BackgroundWorker Worker;
            public BackroundWorkerArg(BackgroundWorker worker, int address, int size, int length, int delay)
            {
                Delay = delay;
                BlockSize = length;
                Worker = worker;
                Address = address;
                Size = size;
            }
        }

        readonly Memory  _memory;

        public IntFlashDownload(Memory memory)
        {
            _memory = memory;
            _bw = new BackgroundWorker();
            _bw.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            _waitForDoneEvent = new AutoResetEvent(false);
            _waitForDelayEvent = new AutoResetEvent(false);
        }

        public void Begin(int address, int size, int blockSize, int delay)
        {
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
            _bw.RunWorkerAsync(new BackroundWorkerArg(_bw, address, size, blockSize, delay));
        }

        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int delay = (e.Argument as BackroundWorkerArg).Delay;
            BackgroundWorker bw = (e.Argument as BackroundWorkerArg).Worker;
            int size = (e.Argument as BackroundWorkerArg).Size;
            int blockSize = (e.Argument as BackroundWorkerArg).BlockSize;
            int address = (e.Argument as BackroundWorkerArg).Address;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            int offset = 0;

            byte[] data = new byte[size];
            try
            {
                do
                {
                    byte[] temp;
                    if (size - offset >= blockSize)
                    {
                        temp = _memory.ExtFlashRead((UInt32)address, blockSize);
                        Buffer.BlockCopy(temp, 0, data, offset, blockSize);
                        address += blockSize;
                        offset += blockSize;
                    }
                    else
                    {
                        temp = _memory.ExtFlashRead((UInt32)address,  size - offset);
                        Buffer.BlockCopy(temp, 0, data, offset, size - offset);
                        address += (size - offset);
                        offset += (size - offset);
                    }
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    bw.ReportProgress((int)(((double)offset / size) * 100.0), "STATUS: " + size.ToString() + "/" + offset.ToString() + ".");
                    _waitForDelayEvent.WaitOne(delay);
                } while (offset != size);

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
                _waitForDelayEvent.Set();
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

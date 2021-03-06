
namespace Konvolucio.MGUI201222.DFU
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using System.Threading;
    using Properties;
    using System.Diagnostics;
    using Common;
    using IO;
    using Controls;
    using Events;
    using System.ComponentModel;
    using System.Drawing;


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((MainForm)new App()._mainForm);
        }
    }

    public enum ConnectionStatusTypes
    { 
        Connected,
        PressToConnect
    }

    class App
    {
        public static SynchronizationContext SyncContext = null;

        internal IMainForm _mainForm { get; set; }
        readonly ExtFlashDownload _efd;
        readonly ExtFlashUpload _efu;
        readonly IntFlashDownload _ifd;
        readonly IntFlashUpload _ifu;
        readonly Stopwatch _sw;
        Queue<string> _traceQueue = new Queue<string>();
        byte[] _intFw;
        byte[] _extFw;

        public App()
        {

            /*** Application Configuration ***/
            string AppDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\MGUI201222.DFU";
            if (!System.IO.Directory.Exists(AppDirectory))
                System.IO.Directory.CreateDirectory(AppDirectory);

            Log.Instance.FilePath = $"{AppDirectory}\\DFU_LOG_{DateTime.Now.ToString(AppConstants.FileNameTimestampFormat)}.txt ";
            Log.Instance.WirteLine("*** Appliction Start ***");

            SyncContext = SynchronizationContext.Current;

            MemoryInterface.Instance.TracingEnable = true;

            _efd = new ExtFlashDownload(MemoryInterface.Instance);
            _efd.ProgressChange += ProgressChanged;
            _efd.Completed += Completed;
            _efu = new ExtFlashUpload(MemoryInterface.Instance);
            _efu.ProgressChange += ProgressChanged;
            _efu.Completed += Completed;
            _ifd = new IntFlashDownload(MemoryInterface.Instance);
            _ifd.ProgressChange += ProgressChanged;
            _ifd.Completed += Completed;
            _ifu = new IntFlashUpload(MemoryInterface.Instance);
            _ifu.ProgressChange += ProgressChanged;
            _ifu.Completed += Completed;
            _sw = new Stopwatch();

            _mainForm = new MainForm();
            _mainForm.Text = "MGUI201222 - DFU";

            _mainForm.FormClosed += MainForm_FormClosed;
            _mainForm.Shown += MainForm_Shown;
            _mainForm.FormClosed += MainForm_FormClosed;


            /*** Connection ***/
            MemoryInterface.Instance.ConnectionChanged += (o, e) =>
            {
                EventAggregator.Instance.Publish(new ConnectionChangedAppEvent(MemoryInterface.Instance.IsOpen));

                if (MemoryInterface.Instance.IsOpen)
                {
                    try
                    { 
                        for (int i = 0; i < 10; i++)
                        {
                            Application.DoEvents();
                            Thread.Sleep(100);
                        }

                        string msg = "ENTERED DFU MODE";
                        _mainForm.ProgressUpdate(msg, Color.LightBlue, 0);
                        Trace($"App: {msg}");
                        MemoryInterface.Instance.EnterDfuMode();
                        //This will update the Whois on statusbar...
                        EventAggregator.Instance.Publish(new ConnectionChangedAppEvent(MemoryInterface.Instance.IsOpen));

                        for (int i = 0; i < 5; i++)
                        {
                            Application.DoEvents();
                            Thread.Sleep(100);
                        }

                        FwUpdate(Settings.Default.IntFirmwareFilePath, Settings.Default.ExtFirmwareFilePath);     
                    }
                    catch (Exception ex)
                    {
                        Trace($"ERROR: {ex.Message}");
                    }
                    finally
                    { 
                    
                    }

                }
            };

            /*** Main Menu ***/
            _mainForm.MenuBar = new ToolStripItem[]
            {
                new Commands.ComPortSelectCommand(this),
                new Commands.ConnectCommand(this),
                new Commands.HowIsWorkingCommand(),
            };

            /*** StatusBar ***/
            #region StatusBar
            _mainForm.StatusBar = new ToolStripItem[]
            {
                new StatusBar.WhoIsStatusBar(),
                new StatusBar.FwVersion(),
                new StatusBar.EmptyStatusBar(),
                new StatusBar.VersionStatus(),
                new StatusBar.LogoStatusBar(),
            };
            #endregion


            /*** TimerService ***/
            TimerService.Instance.Interval = 100;

            /*** Trace ***/

            TimerService.Instance.Tick += (o, e) =>
            {
                if(MemoryInterface.Instance.TraceQueue.Count != 0)
                {
                    string str = MemoryInterface.Instance.TraceQueue.Dequeue();

                    Log.Instance.WirteLine(str);

                    if (str.Contains("Rx:"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", Color.DarkGreen, false);
                    else if (str.Contains("Tx:"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", Color.Blue);
                    else if (str.Contains("ERROR"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", Color.Red);
                    else
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", Color.Black);

                    /*** Auto Scroll ***/
                    _mainForm.RichTextBoxTrace.SelectionStart = _mainForm.RichTextBoxTrace.Text.Length;
                    _mainForm.RichTextBoxTrace.ScrollToCaret();
                }
                if (_traceQueue.Count != 0)
                {
                    string str = _traceQueue.Dequeue();
                    Log.Instance.WirteLine(str);
                    if (str.Contains("App"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", Color.HotPink);
                    else if (str.Contains("ERROR"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", Color.Red);
                    else
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", Color.Black);

                    /*** Auto Scroll ***/
                    _mainForm.RichTextBoxTrace.SelectionStart = _mainForm.RichTextBoxTrace.Text.Length;
                    _mainForm.RichTextBoxTrace.ScrollToCaret();
                }
            };
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            SyncContext = SynchronizationContext.Current;
            TimerService.Instance.Start();

            EventAggregator.Instance.Publish(new ShowAppEvent());
            _mainForm.ExtFlashFilePath = Settings.Default.ExtFirmwareFilePath;
            _mainForm.IntFlashFilePath = Settings.Default.IntFirmwareFilePath;
            ResultClear();
        }


        public void OpenPort()
        {
            if (string.IsNullOrEmpty(Settings.Default.SeriaPortName))
            {
                Trace("ERROR: Please select a COM port first form the ComboBox.");
            }
            else
            {
                ResultClear();
                MemoryInterface.Instance.Open(Settings.Default.SeriaPortName);
            }
        }


        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ifd.Dispose();
            _ifu.Dispose();
            _efd.Dispose();
            _efu.Dispose();
            MemoryInterface.Instance.Dispose();
            TimerService.Instance.Dispose();
            Settings.Default.Save();
            EventAggregator.Instance.Dispose();
        }

        public void FwAbort()
        {
            _ifd.Abort();
            _ifu.Abort();
            _efd.Abort();
            _efu.Abort();
        }

        private void ResultClear()
        { 
            /*** Results Clear ***/
            _traceQueue.Clear();
            MemoryInterface.Instance.TraceQueue.Clear();
            _mainForm.RichTextBoxTrace.Clear();
            _mainForm.ProgressUpdate("", SystemColors.Control, 0);
            _sw.Reset();
            _sw.Start();
        }

        public void FwUpdate(string intFile, string extFile)
        {
            if (System.IO.File.Exists(intFile))
                _intFw = Common.Tools.OpenFile(intFile);
            else
                _intFw = new byte[0];


            if (System.IO.File.Exists(extFile))
                _extFw = Common.Tools.OpenFile(extFile);
            else
                _extFw = new byte[0];

            if (_intFw.Length != 0)
            {
                _ifu.Begin(0, _intFw);
                Trace($"App: INT FALSH UPLOADING - {System.IO.Path.GetFileName(intFile) } - { new System.IO.FileInfo(intFile).Length}byte");
                EventAggregator.Instance.Publish(new UpdatingStateChangedAppEvent(true));
            }
            else if (_extFw.Length != 0)
            {
                _efu.Begin(0, _extFw);
                Trace($"App: EXT FALSH UPLOADING - {System.IO.Path.GetFileName(extFile) } - { new System.IO.FileInfo(extFile).Length}byte");
                EventAggregator.Instance.Publish(new UpdatingStateChangedAppEvent(true));
            }
            else
            {
                Trace("ERROR: There is nothing to do, please load binary file.");
                ResultIsFailed();
            }
        }

        private void Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Action syncCompleted = () =>
            {

                if (e.Cancelled)
                {
                    Trace($"ERROR: ABORTED");
                    ResultIsFailed();
                    return;
                }

                if (e.Error != null)
                {
                    Trace($"ERROR: {e.Error.Message}");
                    ResultIsFailed();
                    return;
                }

                if (sender is IntFlashUpload)
                {
                    Trace($"App: INT FALSH UPLOAD COMPLETED");

                    _ifd.Begin(0, _intFw.Length);
                    Trace("App: INT FALSH DOWNLOADING...");

                }

                if (sender is IntFlashDownload)
                {
                    Trace("App: INT FALSH DOWNLOAD COMPLETED");
                    if (((byte[])e.Result).SequenceEqual(_intFw))
                    {
                        Trace("App: INT FALSH VERIFY: PASSED");

                        if (_extFw.Length != 0)
                        {
                            _efu.Begin(0, _extFw);
                            Trace($"App: EXT FALSH UPLOADING - {System.IO.Path.GetFileName(Settings.Default.ExtFirmwareFilePath)} - {new System.IO.FileInfo(Settings.Default.ExtFirmwareFilePath).Length}byte");
                        }
                        else
                        {
                            ResultIsPassed();
                        }
                    }
                    else
                    {
                        Trace("App: INT FALSH VERIFY: FAILED");
                        ResultIsFailed();
                    }
                }

                if (sender is ExtFlashUpload) 
                {
                    Trace("App: EXT FALSH UPLOAD COMPLETED");

                    _efd.Begin(0, _extFw.Length);
                    Trace("App: EXT FALSH DOWNLOADING...");

                }

                if (sender is ExtFlashDownload)
                {
                    Trace("App: EXT FALSH DOWNLOAD COMPLETED");

                    if (((byte[])e.Result).SequenceEqual(_extFw))
                    {
                        Trace("App: EXT FALSH VERIFY: PASSED");
                        ResultIsPassed();
                    }
                    else
                    {
                        Trace("App: INT FALSH VERIFY: FAILED");
                        ResultIsFailed();
                    }
                }
            };

            if (App.SyncContext != null)
                App.SyncContext.Post((e1) => { syncCompleted(); }, null);
        }


        void ResultIsPassed()
        {
            _sw.Stop();
            string msg = $"UPDATE SUCCESSFULY I:{_intFw.Length / 1024}kB E:{_extFw.Length / 1024}kB t:{_sw.ElapsedMilliseconds / 1000}s";
            Trace($"App: {msg}");
            EventAggregator.Instance.Publish(new UpdatingStateChangedAppEvent(false));
            _mainForm.ProgressUpdate(msg, Color.Lime, 100);

            for (int i = 0; i < 5; i++)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            MemoryInterface.Instance.ExitDfuMode();
            msg = $"Leaving DFU Mode, the App will start soon...";
            _mainForm.ProgressUpdate(msg, Color.Gold, 100);

            for (int i = 0; i < 5; i++)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            MemoryInterface.Instance.Close();

        }

        void ResultIsFailed()
        {
            _sw.Stop();
            string msg = $"UPDATE FAILED t:{_sw.ElapsedMilliseconds / 1000}s";
            Trace($"App: {msg}");
            EventAggregator.Instance.Publish(new UpdatingStateChangedAppEvent(false));
            _mainForm.ProgressUpdate(msg, Color.Red, 0);

        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Action syncCompleted = () =>
            {
                _mainForm.ProgressUpdate(e.UserState.ToString(), Color.Orange, e.ProgressPercentage);
            };

            if (App.SyncContext != null)
                App.SyncContext.Post((e1) => { syncCompleted(); }, null);
        }

        private void Trace(string msg)
        {
            _traceQueue.Enqueue(DateTime.Now.ToString(AppConstants.GenericTimestampFormat) + " " + msg);
        }
    }
        
}

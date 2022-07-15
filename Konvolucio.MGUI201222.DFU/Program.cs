
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


            //AppLog.Instance.FilePath = AppConfiguration.Instance.LogDirectory + @"//MALT200817.DFU_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".txt";
            //AppLog.Instance.Enabled = AppConfiguration.Instance.DfuApp.LogEnable;

            // if (string.IsNullOrEmpty(Settings.Default.WorkingDirectory))
            { //First Start 
              //C:\\Users\\Margit Robert\\Documents\\Konvolucio\\MARC170608
              //  Settings.Default.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.CompanyName + "\\" + Application.ProductName;
              //C:\\Users\\Margit Robert\\Documents\\Konvolucio\\MARC170608\\Firmware Update
              //  Settings.Default.FirmwareUpdateDirectory = Settings.Default.WorkingDirectory + "\\" + "Firmware Update";
            }

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
                    MemoryInterface.Instance.EnterDfuMode();
            };

            /*** Settings ***/
            var settingsMenu = new ToolStripMenuItem("Settings");
            settingsMenu.DropDown.Items.AddRange(
                 new ToolStripItem[]
                 {
                    new Commands.ConnectAfterStartCommand(),
                    new Commands.WriteReadVerifyCommand(),
                 });

            /*** Main Menu ***/
            _mainForm.MenuBar = new ToolStripItem[]
            {
                new Commands.ComPortSelectCommand(this),
                new Commands.ConnectCommand(this),
                new Commands.EnterDfutCommand(),
                new Commands.UpdateCommand(this),
                new Commands.ExitDfutCommand(),
                settingsMenu
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
                    if (str.Contains("Rx:"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.DarkGreen, false);
                    else if (str.Contains("Tx:"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Blue);
                    else if (str.Contains("ERROR"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Red);

                    else
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Black);
                }
                if (_traceQueue.Count != 0)
                {
                    string str = _traceQueue.Dequeue();
                    if (str.Contains("App"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.HotPink);
                    else if (str.Contains("ERROR"))
                        _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Red);
                }
            };
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            SyncContext = SynchronizationContext.Current;
            TimerService.Instance.Start();

            if (Settings.Default.OpenAfterStartUp)
                if (!string.IsNullOrWhiteSpace(Settings.Default.SeriaPortName))
                    OpenPort();

            EventAggregator.Instance.Publish(new ShowAppEvent());
            _mainForm.ExtFlashFilePath = Settings.Default.ExtFirmwareFilePath;
            _mainForm.IntFlashFilePath = Settings.Default.IntFirmwareFilePath;
        }


        public void OpenPort()
        {
            ResultClear();
            MemoryInterface.Instance.Open(Settings.Default.SeriaPortName);
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
            _mainForm.ResultReset();
            _sw.Reset();
            _sw.Start();
        }

        public void FwUpdate(string intFile, string extFile)
        {
            ResultClear();

            if (System.IO.File.Exists(intFile))
                _intFw = Tools.OpenFile(intFile);
            else
                _intFw = new byte[0];


            if (System.IO.File.Exists(extFile))
                _extFw = Tools.OpenFile(extFile);
            else
                _extFw = new byte[0];

            if (_intFw.Length != 0)
            {
                _ifu.Begin(0, _intFw);
                Trace("App: INT FALSH UPLOADING...");
                EventAggregator.Instance.Publish(new UpdatingStateChangedAppEvent(true));
            }
            else if (_extFw.Length != 0)
            {
                _efu.Begin(0, _extFw);
                Trace("App: EXT FALSH UPLOADING...");
                EventAggregator.Instance.Publish(new UpdatingStateChangedAppEvent(true));
            }
            else
            {
                Trace("App: There is nothing to do, please load binary file");
                ResultIsFailed();
            }
        }


        /*
         * 1. Int-Ok   | Ext-Ok   | Verify-None |  
         * 2. Int-Ok   | Ext-None | Verify-None |
         * 3. Int-None | Ext-Ok   | Verify-None |
         * 4. Int-None | Ext-None | Verify-None |
         * 5. Int-Ok   | Ext-Ok   | Verify-On   | Int-Verify-Ok | Ext-Verify-Ok
         * 6. Int-Ok   | Ext-None | Verify-On   | Int-Verify-Ok | Ext-Verify-None
         * 
         */
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
                    Trace("App: INT FALSH UPLOAD COMPLETED");

                    if (Settings.Default.WriteReadVerify)
                    {
                        _ifd.Begin(0, _intFw.Length);
                        Trace("App: INT FALSH DOWNLOADING...");
                    }
                    else
                    {
                        if (_extFw.Length != 0)
                        {
                            _efu.Begin(0, _extFw);
                            Trace("App: EXT FALSH UPLOADING...");
                        }
                        else
                        {
                            ResultIsPassed();
                        }
                    }
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
                            Trace("App: EXT FALSH UPLOADING...");
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

                    if (Settings.Default.WriteReadVerify)
                    {
                        _efd.Begin(0, _extFw.Length);
                        Trace("App: EXT FALSH DOWNLOADING...");
                    }
                    else 
                    {
                        ResultIsPassed();
                    }
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
            Trace($"App: UPDATE: PASSED");
            _mainForm.ProgressStatus = $"UPDATE SUCCESSFULY I:{_intFw.Length/1024}kB E:{_extFw.Length / 1024}kB t:{_sw.ElapsedMilliseconds/1000}s";
            EventAggregator.Instance.Publish(new UpdatingStateChangedAppEvent(false));
            _mainForm.ResultPassed();
        }

        void ResultIsFailed()
        {
            _sw.Stop();
            Trace("ERROR: UPDATE: FAILED");
            _mainForm.ProgressStatus = "UPDATE FAILED";
            EventAggregator.Instance.Publish(new UpdatingStateChangedAppEvent(false));
            _mainForm.ResultFailed();
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Action syncCompleted = () =>
            {
                _mainForm.ProgressValue = e.ProgressPercentage;
                _mainForm.ProgressStatus =e.UserState.ToString();
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

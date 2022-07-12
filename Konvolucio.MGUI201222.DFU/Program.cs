
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

        public IMainForm _mainForm { get; set; }
        string _extFirmwareFilePath { get; set; }
        string _intFirmwareFilePath { get; set; }

        readonly ExtFlashDownload _efd;
        readonly ExtFlashUpload _efu;
        readonly IntFlashDownload _ifd;
        readonly IntFlashUpload _ifu;

        public App()
        {

            /*** Application Configuration ***/
           

            //AppLog.Instance.FilePath = AppConfiguration.Instance.LogDirectory + @"//MALT200817.DFU_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".txt";
            //AppLog.Instance.Enabled = AppConfiguration.Instance.DfuApp.LogEnable;
            AppLog.Instance.WriteLine("MALT200817.DFU.App() Constructor started.");

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
            _efu = new ExtFlashUpload(MemoryInterface.Instance);
            _ifd = new IntFlashDownload(MemoryInterface.Instance);
            _ifu = new IntFlashUpload(MemoryInterface.Instance);

            _mainForm = new MainForm();
            _mainForm.Text = "MGUI201222 - DFU";
            _mainForm.Version = Application.ProductVersion;

            _mainForm.FormClosed += MainForm_FormClosed;
            _mainForm.WriteEventHandler += ButtonWrite_Click;
            _mainForm.Shown += MainForm_Shown;
            _mainForm.DeviceRestart += MainForm_DeviceRestart;
            _mainForm.ShowConfiguration += MainForm_ShowConfiguration;


            /*** Main Menu ***/
            _mainForm.MenuBar = new ToolStripItem[]
            {
                new Commands.ComPortSelectCommand(this),
                new Commands.StartStopCommand(),
            };

            /*** TimerService ***/
            TimerService.Instance.Interval = 500;

            /*** Trace ***/
            TimerService.Instance.Tick += (o, e) =>
            {
                for (int i = 0; MemoryInterface.Instance.TraceQueue.Count != 0; i++)
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
            };
        }


        private void MainForm_ShowConfiguration(object sender, EventArgs e)
        {
          //  Tools.RunNotepadOrNpp();
        }

        private void MainForm_DeviceRestart(object sender, int e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                
              //  MainForm.ConnectionStatus = ConnectionStatusTypes.PressToConnect;
                throw ex; 
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            SyncContext = SynchronizationContext.Current;
            TimerService.Instance.Start();

            if (Settings.Default.OpenAfterStartUp)
            {
                if (!string.IsNullOrWhiteSpace(Settings.Default.SeriaPortName))
                {
                    MemoryInterface.Instance.Open(Settings.Default.SeriaPortName);
                }
            }
            EventAggregator.Instance.Publish(new ShowAppEvent());

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            Settings.Default.Save();
        }

        private void ButtonWrite_Click(object sender, EventArgs e)
        {

         //   MainForm.WriteEnabled = false;



            /*
            dfu.ProgressChange += (o, ev) =>
            {
                MainForm.PoregressValue = ev.ProgressPercentage;
                MainForm.LabelStatus = ev.UserState.ToString();
            };
            */
            byte[] firmware = Tools.OpenFile(_extFirmwareFilePath);
           // dfu.Begin(firmware);

            Action syncCompleted = () =>
            {

            };
            /*
            dfu.Completed += (o, ev) =>
            {
                MainForm.WriteEnabled = true;

                if (App.SyncContext != null)
                    App.SyncContext.Post((e1) => { syncCompleted(); }, null);

                if (ev.Result is Exception)
                    MessageBox.Show((ev.Result as Exception).Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            */
        }


    }
        
}

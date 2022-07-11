
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
            Application.Run((MainForm)new App().MainForm);
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

        public IMainForm MainForm { get; set; }
        string _firmwareFilePath { get; set; }


        public App()
        {

            /*** Application Configuration ***/
           

            //AppLog.Instance.FilePath = AppConfiguration.Instance.LogDirectory + @"//MALT200817.DFU_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".txt";
            //AppLog.Instance.Enabled = AppConfiguration.Instance.DfuApp.LogEnable;
            AppLog.Instance.WriteLine("MALT200817.DFU.App() Constructor started.");

            if (string.IsNullOrEmpty(Settings.Default.WorkingDirectory))
            { //First Start 
              //C:\\Users\\Margit Robert\\Documents\\Konvolucio\\MARC170608
                Settings.Default.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.CompanyName + "\\" + Application.ProductName;
                //C:\\Users\\Margit Robert\\Documents\\Konvolucio\\MARC170608\\Firmware Update
                Settings.Default.FirmwareUpdateDirectory = Settings.Default.WorkingDirectory + "\\" + "Firmware Update";
            }

            SyncContext = SynchronizationContext.Current;

            MainForm = new MainForm();
            MainForm.Text = "MGUI201222 - DFU";
            MainForm.Version = Application.ProductVersion;

            MainForm.FileBrowseEventHandler += ButtonBrowse_Click;
            MainForm.FormClosed += MainForm_FormClosed;
            MainForm.WriteEventHandler += ButtonWrite_Click;
            MainForm.Shown += MainForm_Shown;
            MainForm.DeviceRestart += MainForm_DeviceRestart;
            MainForm.BtnConnectClick += MainForm_BtnConnectClick;
            MainForm.ShowConfiguration += MainForm_ShowConfiguration;
        }

        private void MainForm_BtnConnectClick(object sender, EventArgs e)
        {
            try
            {
              //  if (MainForm.ConnectionStatus == ConnectionStatusTypes.PressToConnect)
                {


                }
               // else
                {

                }
            }
            catch (Exception ex)
            {
           //     MainForm.ConnectionStatus = ConnectionStatusTypes.PressToConnect;
                throw ex;
            }
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
            byte[] firmware = Tools.OpenFile(_firmwareFilePath);
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

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            var ofd =  new OpenFileDialog();
            if (string.IsNullOrEmpty(_firmwareFilePath))
                ofd.InitialDirectory = Settings.Default.FirmwareDirecotry;
            else
                ofd.InitialDirectory = _firmwareFilePath;
            ofd.Filter = AppConstants.FileFilter;
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _firmwareFilePath = ofd.FileName;
                MainForm.FileName = System.IO.Path.GetFileName(_firmwareFilePath);
            }
        }

    }
        
}

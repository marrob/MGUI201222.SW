namespace Konvolucio.MGUI201222
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Text;
    using System.Threading;
    using System.Diagnostics;
    using Properties;
    using Events;
    using System.ComponentModel;
    using Common;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Controls;
    using MGUIcomm;
    using DACcomm;

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new App();
        }
    }

    public interface IApp
    {

    }

    public class App : IApp
    {
        public static SynchronizationContext SyncContext = null;
        readonly IMainForm _mainForm;

        public App()
        {
            /*** Application Settings Upgrade ***/
            if (Settings.Default.ApplictionSettingsSaveCounter == 0)
            {
                Settings.Default.Upgrade();
                Settings.Default.ApplictionSettingsUpgradeCounter++;
            }
            Settings.Default.ApplictionSettingsSaveCounter++;
            Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Settings_PropertyChanged);

            /*** Main Form ***/
            _mainForm = new MainForm();
            _mainForm.Text = AppConstants.SoftwareTitle + " - " + Application.ProductVersion;
            _mainForm.Shown += MainForm_Shown;
            _mainForm.FormClosing += MainForm_FormClosing;
            _mainForm.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);

            /*** Connection ***/
            DacIoSrv.Instance.ConnectionChanged += (o, e) =>
                EventAggregator.Instance.Publish(new ConnectionChangedAppEvent(DacIoSrv.Instance.IsOpen));

            GuiIoSrv.Instance.ConnectionChanged += (o, e) =>
                  EventAggregator.Instance.Publish(new ConnectionChangedAppEvent(GuiIoSrv.Instance.IsOpen));


            /*** Tracing ***/
            DacIoSrv.Instance.TracingEnable = Settings.Default.TracingEnabled;
            GuiIoSrv.Instance.TracingEnable = Settings.Default.TracingEnabled;

            EventAggregator.Instance.Subscribe((Action<TracingChanged>)(e =>
            {
                DacIoSrv.Instance.TracingEnable = e.Enabled;
                GuiIoSrv.Instance.TracingEnable = e.Enabled;
            }));


            /*** TimerService ***/
            TimerService.Instance.Interval = Settings.Default.GuiRefreshRateMs;

            /*** Trace ***/
            TimerService.Instance.Tick += (o, e) =>
            {

                if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_GUI])
                {
                    for (int i = 0; GuiIoSrv.Instance.TraceQueue.Count != 0; i++)
                    {
                        string str = GuiIoSrv.Instance.TraceQueue.Dequeue();
                        if (str.Contains("Rx:"))
                            _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.DarkGreen, false);
                        else if (str.Contains("Tx:"))
                            _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Blue);
                        else if (str.Contains("ERROR"))
                            _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Red);
                        else
                            _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Black);
                    }
                }
                else if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_DAC])
                {
                    for (int i = 0; DacIoSrv.Instance.TraceQueue.Count != 0; i++)
                    {
                        string str = DacIoSrv.Instance.TraceQueue.Dequeue();
                        if (str.Contains("Rx:"))
                            _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.DarkGreen, false);
                        else if (str.Contains("Tx:"))
                            _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Blue);
                        else if (str.Contains("ERROR"))
                            _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Red);
                        else
                            _mainForm.RichTextBoxTrace.AppendText(str + "\r\n", System.Drawing.Color.Black);
                    }
                }
            };

            /*** Menu Bar ***/
            #region MenuBar      
            var configMenu = new ToolStripMenuItem("Config");
            configMenu.DropDown.Items.AddRange(
                new ToolStripItem[]
                {
                    new Commands.OptionsCommand(this)

                });

            var helpMenu = new ToolStripMenuItem("Help");
            helpMenu.DropDown.Items.AddRange(
                 new ToolStripItem[]
                 {
                     new Commands.HowIsWorkingCommand(),
                    // new Commands.UpdatesCommands(),
                 });


            var runMenu = new ToolStripMenuItem("Run");
            runMenu.DropDown.Items.AddRange(
            new ToolStripItem[]
               {
                    
               });

            _mainForm.MenuBar = new ToolStripItem[]
                {
                   new Commands.SelectDeviceCommand(),
                   new Commands.ComPortSelectCommand(this),
                   new Commands.StartStopCommand(),
                   //configMenu,
                   //runMenu,
                   //viewMenu,
                   //helpMenu,
                };
            #endregion

            /*** StatusBar ***/    
            #region StatusBar
            _mainForm.StatusBar = new ToolStripItem[]
            {
                new StatusBar.LogLinesStatusBar(),   
                new StatusBar.UpTimeCounterStatusBar(),
                new StatusBar.UniqueId(),
                new StatusBar.FwVersion(),
                new StatusBar.EmptyStatusBar(),
                new StatusBar.VersionStatus(),
                new StatusBar.LogoStatusBar(),
            };
            #endregion


            /*** Trace Context Menu ***/
            _mainForm.RichTextBoxTrace.ContextMenuStrip = new ContextMenuStrip();
            _mainForm.RichTextBoxTrace.ContextMenuStrip.Items.AddRange( new ToolStripItem[]
            {
                new Commands.ClearReachTextBoxCommand(_mainForm.RichTextBoxTrace),
            });


            /*** Run ***/
            Application.Run((MainForm)_mainForm);
        }


        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EventAggregator.Instance.Publish(new TreeNodeChangedAppEvent(e.Node));
        }

        void MainForm_Shown(object sender, EventArgs e)
        {
#if TRACE
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
#endif

            SyncContext = SynchronizationContext.Current;
            _mainForm.LayoutRestore();

            //_mainForm.LayoutRestore();
            /*Ö tölti be a projectet*/
            Start(Environment.GetCommandLineArgs());
            TimerService.Instance.Start();
            /*Kezdő Node Legyen az Adapter node*/
            //EventAggregator.Instance.Publish<TreeViewSelectionChangedAppEvent>(new TreeViewSelectionChangedAppEvent(_startTreeNode));

            if (Settings.Default.OpenAfterStartUp)
            {
                if (!string.IsNullOrWhiteSpace(Settings.Default.SeriaPortName))
                    GuiIoSrv.Instance.Open(Settings.Default.SeriaPortName);
            }    

            EventAggregator.Instance.Publish(new ShowAppEvent());
        }

        public void Start(string[] args)
        {
#if TRACE
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + ": " + string.Join("\r\n -", args));
#endif
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
#if TRACE
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." +MethodBase.GetCurrentMethod().Name + "()");
#endif
        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
#if TRACE
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
#endif
            TimerService.Instance.Dispose();
            _mainForm.LayoutSave();
            Settings.Default.Save();
            EventAggregator.Instance.Dispose();
        }

        void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): " + e.PropertyName + ", NewValue: " + Settings.Default[e.PropertyName]);

            if (e.PropertyName == Tools.GetPropertyName(() => Settings.Default.GuiRefreshRateMs))
            {
                TimerService.Instance.Interval = Settings.Default.GuiRefreshRateMs;
            }
        }
    }
}
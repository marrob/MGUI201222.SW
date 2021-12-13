using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


using Konvolucio.MGUI201222.Common;

namespace Konvolucio.MGUI201222.Controls
{
    public partial class DioControl : UserControl
    {
        #region Public Events
        [Browsable(false)]
        public event EventHandler ChangedValue;
        #endregion 
        #region Public Property
        [Browsable(false)]
        public int Index { get; private set; }
        [Browsable(false)]
        public bool Checked { get; private set; }
        [Browsable(false)]
        public bool[] Value { get; private set; }
        [Browsable(false)]
        public bool NotAvaliable
        {
            get
            {
                return _notAvaliable;
            }
            set
            {
                if (_notAvaliable != value)
                {
                    _notAvaliable = value;
                    foreach (Control ctrl in Tools.GetAllControls(this))
                        if (ctrl is DioItemControl)
                            (ctrl as DioItemControl).NotAvaliable = value;
                }
            }

        }
        bool _notAvaliable;
        [Browsable(false)]
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    foreach (Control ctrl in Tools.GetAllControls(this))
                        if (ctrl is DioItemControl)
                            (ctrl as DioItemControl).ReadOnly = value;

                }
            }
        }
        bool _readOnly;

        #endregion 

        #region Constructor
        public DioControl()
        {
            Value = new bool[32];
            InitializeComponent();
        }
        #endregion 


        protected override void OnEnabledChanged(EventArgs e)
        {
            if (base.Enabled)
            {
                foreach (Control ctrl in Tools.GetAllControls(this))
                {
                    if (ctrl is DioItemControl)
                        ctrl.Enabled = base.Enabled;
                }
            }

            base.OnEnabledChanged(e);
        }
        public void SetContent(int index, bool state)
        {
            foreach (Control ctrl in Tools.GetAllControls(this))
            {
                if ((ctrl is DioItemControl) && int.Parse(ctrl.Tag.ToString()) == index)
                {
                    (ctrl as DioItemControl).Checked = state;
                    Value[index] = state;
                }
            }
        }
        public void SetContent(bool[] value)
        {
            var list = Tools.GetAllControls(this).Where(n => n.GetType() == typeof(DioItemControl)).ToList<Control>();
            foreach(DioItemControl ctrl in list)
            {
                int index = int.Parse((string)ctrl.Tag);
                bool state = value[index];
                ctrl.Checked = state;
                Value[index] = state;
            }
        }
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            DioItemControl cb = sender as DioItemControl;
            Checked = cb.Checked;
            Index = int.Parse(cb.Tag.ToString());
            Value[Index] = Checked;
            if (ChangedValue != null)
                ChangedValue(this, EventArgs.Empty);

        }
    }
}

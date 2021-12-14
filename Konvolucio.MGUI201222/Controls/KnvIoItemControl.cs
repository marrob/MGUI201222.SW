using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konvolucio.MGUI201222.Controls
{
    public partial class DioItemControl : UserControl
    {
        public event EventHandler CheckedChanged;

        [Browsable(false)]
        public bool Checked
        {
            set 
            {
                if (_checked != value)
                {
                    _checked = value;
                    if (!NotAvaliable)
                    {
                        UpdateState();
                    }
                }
            }
            get { return _checked; }
        }
        private bool _checked;
        [Browsable(false)]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    UpdateState();
                }
            }
        }
        bool _readOnly;
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
                    UpdateState();
                }
            }
        
        }
        bool _notAvaliable;
 

        public DioItemControl()
        {
            InitializeComponent();
            OnCheckedChanged(Checked);
        }

        protected virtual void OnCheckedChanged(bool state)
        {
          if (CheckedChanged != null)
                CheckedChanged(this, EventArgs.Empty);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            if (Enabled && !_readOnly && !NotAvaliable)
            {  
                UpdateState();
                OnCheckedChanged(_checked); 
            }
        }
        void UpdateState()
        {
            if (_notAvaliable)
            {
                if (_notAvaliable)
                {
                    this.BackColor = Color.Yellow;
                    label1.Text = "N";
                }
            } 
            else if (_readOnly)
            {
                if (_checked)
                {
                    this.BackColor = Color.DarkRed;
                    label1.Text = "T";
                }
                else
                {
                    this.BackColor = Color.Green;
                    label1.Text = "F";
                }
            }
            else
            {
                if (_checked)
                {
                    this.BackColor = Color.Red;
                    label1.Text = "T";
                }
                else
                {
                    this.BackColor = Color.Lime;
                    label1.Text = "F";
                }
            }
        }
    }
}

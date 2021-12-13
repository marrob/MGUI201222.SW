namespace Konvolucio.MGUI201222.View
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Properties;

    public partial class HelpNode : UserControl, IUIPanelProperties
    {
        public HelpNode()
        {
            InitializeComponent();
            this.Name = "HelpNode";
        }

        public void UserEnter()
        {
         
        }

        public void UserLeave()
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

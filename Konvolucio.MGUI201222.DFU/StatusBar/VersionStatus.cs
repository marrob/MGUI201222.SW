

namespace Konvolucio.MGUI201222.DFU.StatusBar
{
    using System.Windows.Forms;

    class VersionStatus : ToolStripStatusLabel
    {
        public VersionStatus()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text = Application.ProductVersion;
        }
    }
}

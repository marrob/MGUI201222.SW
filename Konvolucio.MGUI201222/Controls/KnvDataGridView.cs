
namespace Konvolucio.MCEL181123.Calib.Controls
{
   
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class KnvDataGridView : DataGridView
    {
        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            base.OnRowPrePaint(e);
            RowPrePaintHandlerForZebraRows(e);
        }

        #region Background Text
        /// <summary>
        /// Ha nincs tartalom ez a szöveg látszik.
        /// </summary>
        [Category("KNV")]
        [Description("Ha nincs tartalom ez a szöveg látszik.")]
        public string BackgroundText
        {
            get { return _backgroundText; }
            set
            {
                _backgroundText = value;
                Refresh();
            }
        }
        private string _backgroundText = "Backgorund Text";

        private void PaintHandlerForBackgoroundText(PaintEventArgs e)
        {
            var dgv = this;
            float width = dgv.Bounds.Width;
            float height = dgv.Bounds.Height;

            int backgroundTextSize = 25;

            base.OnPaint(e);

            if (dgv.Rows.Count == 0)
            {
                Color clear = dgv.BackgroundColor;
                if (backgroundTextSize == 0) backgroundTextSize = 10;
                Font f = new Font("Seqoe", 20, FontStyle.Bold);
                Brush b = new SolidBrush(Color.Orange);
                SizeF strSize = e.Graphics.MeasureString(_backgroundText, f);
                e.Graphics.DrawString(_backgroundText, f, b, (width / 2) - (strSize.Width / 2), (height / 2) - strSize.Height / 2);
            }
        }
        #endregion

        #region Zebra Rows
        /// <summary>
        /// Zebra csikozás engedélyezése.
        /// </summary>
        [Category("KNV")]
        [Description("Zebra csikozás engedélyezése.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ZebraRow
        {
            get { return _zebraRows; }
            set
            {
                _zebraRows = value;
                Refresh();
            }
        }
        private bool _zebraRows = true;

        [Category("KNV")]
        public Color FirstZebraColor
        {
            get { return _firstZebraColor; }
            set { _firstZebraColor = value; }
        }

        private Color _firstZebraColor = Color.Bisque;

        [Category("KNV")]
        public Color SecondZebraColor
        {
            get { return _secondZebraColor; }
            set { _secondZebraColor = value; }
        }
        private Color _secondZebraColor = Color.White;

        private void RowPrePaintHandlerForZebraRows(DataGridViewRowPrePaintEventArgs e)
        {
            if (_zebraRows)
            {
                var rowIndex = e.RowIndex;
                if (rowIndex % 2 == 0)
                    Rows[rowIndex].DefaultCellStyle.BackColor = _firstZebraColor;
                else
                    Rows[rowIndex].DefaultCellStyle.BackColor = _secondZebraColor;
            }
        }
        #endregion

        #region Data Error Handler
        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            /*base.OnDataError(displayErrorDialogIfNoHandler, e);*/
            MessageBox.Show(e.Exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion
    }
}

namespace YanZhiwei.DotNet2.Utilities.WinForm.Core
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    #region Delegates

    /// <summary>
    /// 委托
    /// </summary>
    /// <param name="state"></param>
    public delegate void CheckBoxClickedHandler(bool state);

    #endregion Delegates

    /// <summary>
    /// 勾选列
    /// </summary>
    public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        #region Fields

        private CheckBoxState allCheckedState = CheckBoxState.UncheckedNormal;
        private Point cellLocation = new Point();
        private Point checkBoxLocation;
        private Size checkBoxSize;
        private bool ckstatus = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public DatagridViewCheckBoxHeaderCell()
        {
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// 勾选事件
        /// </summary>
        public event CheckBoxClickedHandler OnCheckBoxClicked;

        #endregion Events

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:MouseClick" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point _point = new Point(e.X + cellLocation.X, e.Y + cellLocation.Y);
            if (_point.X >= checkBoxLocation.X && _point.X <= checkBoxLocation.X + checkBoxSize.Width && _point.Y >= checkBoxLocation.Y && _point.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                ckstatus = !ckstatus;
                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(ckstatus);
                    this.DataGridView.InvalidateCell(this);
                }
            }
            base.OnMouseClick(e);
        }

        /// <summary>
        /// Paints the specified graphics.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="clipBounds">The clip bounds.</param>
        /// <param name="cellBounds">The cell bounds.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="dataGridViewElementState">State of the data grid view element.</param>
        /// <param name="value">The value.</param>
        /// <param name="formattedValue">The formatted value.</param>
        /// <param name="errorText">The error text.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="advancedBorderStyle">The advanced border style.</param>
        /// <param name="paintParts">The paint parts.</param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            Point _point = new Point();
            Size _size = CheckBoxRenderer.GetGlyphSize(graphics,
            CheckBoxState.UncheckedNormal);
            _point.X = cellBounds.Location.X + (cellBounds.Width / 2) - (_size.Width / 2);
            _point.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (_size.Height / 2);
            cellLocation = cellBounds.Location;
            checkBoxLocation = _point;
            checkBoxSize = _size;
            if (ckstatus)
            {
                allCheckedState = CheckBoxState.CheckedNormal;
            }
            else {
                allCheckedState = CheckBoxState.UncheckedNormal;
            }
            CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, allCheckedState);
        }

        #endregion Methods

        #region Other

        /*
         * 参考：
         * 1. http://www.codeproject.com/Articles/20165/CheckBox-Header-Column-For-DataGridView
         */

        #endregion Other
    }
}
namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// DataGrid 帮助类
    /// </summary>
    public static class DataGridHelper
    {
        #region Methods

        /// <summary>
        ///  将DateTimePicker应用到列编辑时候
        /// </summary>
        /// <param name="dataGrid">DataGridView</param>
        /// <param name="datePicker">DateTimePicker</param>
        /// <param name="columnIndex">应用编辑列索引</param>
        public static void ApplyDateTimePicker(this DataGridView dataGrid, DateTimePicker datePicker, int columnIndex)
        {
            datePicker.Visible = false;
            datePicker.ValueChanged += (sender, e) =>
            {
                DateTimePicker _datePicker = sender as DateTimePicker;
                dataGrid.CurrentCell.Value = _datePicker.Value;
                datePicker.Visible = false;
            };
            dataGrid.CellClick += (sender, e) =>
            {
                if (e.ColumnIndex == columnIndex)
                {
                    DataGridView _dataGrid = sender as DataGridView;
                    Rectangle _cellRectangle = _dataGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    datePicker.Location = _cellRectangle.Location;
                    datePicker.Width = _cellRectangle.Width;
                    try
                    {
                        datePicker.Value = _dataGrid.CurrentCell.Value.ToDateOrDefault(DateTime.Now);
                    }
                    catch
                    {
                        datePicker.Value = DateTime.Now;
                    }
                    datePicker.Visible = true;
                }
            };
            dataGrid.Controls.Add(datePicker);
        }

        /// <summary>
        /// 添加checkbox 列头
        /// </summary>
        /// <param name="dataGrid">DataGridView</param>
        /// <param name="columnIndex">列索引</param>
        /// <param name="headerText">列名称</param>
        public static void ApplyHeaderCheckbox(this DataGridView dataGrid, int columnIndex, string headerText)
        {
            DatagridViewCheckBoxHeaderCell _checkedBox = new DatagridViewCheckBoxHeaderCell();
            dataGrid.Columns[columnIndex].HeaderCell = _checkedBox;
            dataGrid.Columns[columnIndex].HeaderText = headerText;
            _checkedBox.OnCheckBoxClicked += (state) =>
            {
                int _count = dataGrid.Rows.Count;
                for (int i = 0; i < _count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGrid.Rows[i].Cells[columnIndex];
                    checkCell.Value = state;
                }
            };
        }

        /// <summary>
        ///根据cell内容调整其宽度
        /// </summary>
        /// <param name="girdview">DataGridView</param>
        public static void AutoCellWidth(this DataGridView girdview)
        {
            int _columnSumWidth = 0;
            for (int i = 0; i < girdview.Columns.Count; i++)
            {
                girdview.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                _columnSumWidth += girdview.Columns[i].Width;
            }
            girdview.AutoSizeColumnsMode = _columnSumWidth > girdview.Size.Width ? DataGridViewAutoSizeColumnsMode.DisplayedCells : DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        ///  绘制行号
        /// </summary>
        /// <param name="dataGrid">DataGridView</param>
        public static void DrawSequenceNumber(this DataGridView dataGrid)
        {
            dataGrid.RowPostPaint += (sender, e) =>
            {
                DataGridView _dataGrid = sender as DataGridView;
                Rectangle _rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, _dataGrid.RowHeadersWidth, e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                    _dataGrid.RowHeadersDefaultCellStyle.Font,
                    _rectangle,
                    _dataGrid.RowHeadersDefaultCellStyle.ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            };
        }

        /// <summary>
        /// DataGridView绑定
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dataGrid">DataGridView对象</param>
        /// <param name="source">数据源</param>
        public static void DynamicBind<T>(this DataGridView dataGrid, IList<T> source)
            where T : class
        {
            BindingSource _source = null;
            if (dataGrid.DataSource is BindingSource)
            {
                _source = (BindingSource)dataGrid.DataSource;
                _source.AllowNew = true;
                foreach (T entity in source)
                {
                    _source.Add(entity);
                }
            }
            else
            {
                BindingList<T> _bindinglist = new BindingList<T>(source);
                _source = new BindingSource(_bindinglist, null);
                dataGrid.DataSource = _source;
            }
        }

        /// <summary>
        /// 获取选中行
        /// </summary>
        /// <param name="dataGrid">DataGridView对象</param>
        /// <returns>若未有选中行则返回NULL</returns>
        /// 时间：2015-12-09 17:07
        /// 备注：
        public static DataGridViewRow SelectedRow(this DataGridView dataGrid)
        {
            DataGridViewSelectedRowCollection _selectedRows = dataGrid.SelectedRows;
            if (_selectedRows != null && _selectedRows.Count > 0)
            {
                return _selectedRows[0];
            }
            return null;
        }

        #endregion Methods
    }
}
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Windows.Forms;

namespace Molin_CRM.Helper
{
    /// <summary>
    /// GridControl帮助类
    /// </summary>
    public static class GridHelper
    {
        #region 导出到Excel

        /// <summary>
        /// 导出到Excel
        /// <para>eg:GridHelper.ToXls(gcLamp,string.Format("{0}_单灯电参数数据.xls", DateTime.Now.ToString("yyyyMMdd")));</para>
        /// </summary>
        /// <param name="grid">GridControl</param>
        /// <param name="fileName">导出到Excel文件名称</param>
        public static void ToXls(this GridControl grid, string fileName)
        {
            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.Title = "导出Excel";
            _saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            _saveFileDialog.FileName = fileName;
            DialogResult _dialogResult = _saveFileDialog.ShowDialog();
            if (_dialogResult == DialogResult.OK)
            {
                XlsExportOptions _options = new XlsExportOptions();
                _options.Suppress256ColumnsWarning = true;
                _options.Suppress65536RowsWarning = true;
                _options.TextExportMode = TextExportMode.Text;
                grid.ExportToXls(_saveFileDialog.FileName);
                DevMessageBoxHelper.ShowInfo("导出到Excel成功！");
            }
        }

        #endregion 导出到Excel

        #region 设置GridControl绑定

        /// <summary>
        /// 设置GridControl绑定
        /// </summary>
        /// <param name="gridcontrol">GridControl</param>
        /// <param name="gridview">GridView</param>
        /// <param name="datasource">object</param>
        public static void SetDataSource(this GridControl gridcontrol, GridView gridview, object datasource)
        {
            try
            {
                gridcontrol.BeginUpdate();
                gridview.Columns.Clear();
                gridcontrol.DataSource = null;
                gridcontrol.DataSource = datasource;
            }
            catch (Exception)
            {
            }
            finally
            {
                gridcontrol.EndUpdate();
            }
        }

        #endregion 设置GridControl绑定

        #region 清除绑定数据源以及列

        /// <summary>
        /// 清除绑定数据源以及列
        /// </summary>
        /// <param name="gridcontrol">GridControl</param>
        /// <param name="gridview">gridview</param>
        public static void ClearDataSource(this GridControl gridcontrol, GridView gridview)
        {
            try
            {
                gridcontrol.BeginUpdate();
                gridview.Columns.Clear();
                gridcontrol.DataSource = null;
            }
            catch (Exception)
            {
            }
            finally
            {
                gridcontrol.EndUpdate();
            }
        }

        #endregion 清除绑定数据源以及列

        #region 设定列的汇总信息

        /// <summary>
        /// 设定列的汇总信息
        /// <para>eg:gvCabPower.SetSummaryItem("LampConsumption", DevExpress.Data.SummaryItemType.Sum, "合计= {0:n2}");</para>
        /// </summary>
        /// <param name="girdView">GridView</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="type">SummaryItemType</param>
        /// <param name="formatString">格式字符串</param>
        public static void SetSummaryItem(this GridView girdView, string fieldName, SummaryItemType type, string formatString)
        {
            if (girdView != null && !string.IsNullOrEmpty(fieldName))
            {
                if (!girdView.OptionsView.ShowFooter)
                    girdView.OptionsView.ShowFooter = true;
                GridColumn _summaryColumn = girdView.Columns[fieldName];
                if (_summaryColumn != null)
                {
                    _summaryColumn.SummaryItem.SummaryType = type;
                    _summaryColumn.SummaryItem.DisplayFormat = formatString;
                }
            }
        }

        #endregion 设定列的汇总信息

        public static T GetEntityByFocusedRowHandle<T>(this GridView gridView) where T : class
        {
            int _rowIndex = gridView.FocusedRowHandle;
            if (_rowIndex >= 0)
            {
                T _curProduct = (T)gridView.GetRow(_rowIndex);
                return _curProduct;
            }
            return null;
        }
    }
}
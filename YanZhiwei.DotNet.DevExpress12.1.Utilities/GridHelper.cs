namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Windows.Forms;

    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Drawing;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraEditors.ViewInfo;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Columns;
    using DevExpress.XtraGrid.Localization;
    using DevExpress.XtraGrid.Views.Base;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;
    using DevExpress.XtraPrinting;

    using YanZhiwei.DotNet.DevExpress12._1.Utilities.Core;
    using YanZhiwei.DotNet.DevExpress12._1.Utilities.Models;
    using YanZhiwei.DotNet3._5.Utilities.Common;

    /// <summary>
    /// GridControl帮助类
    /// </summary>
    public static class GridHelper
    {
        #region Methods

        /*
         *参考：
         *一、如何解决单击记录整行选中的问题==>View->OptionsBehavior->EditorShowMode 设置为：Click
         *二、如何新增一条记录==>(1)、gridView.AddNewRow()(2)、实现gridView_InitNewRow事件
         *三、如何解决GridControl记录能获取而没有显示出来的问题==>gridView.populateColumns();
         *四、如何让行只能选择而不能编辑（或编辑某一单元格）==>(1)、View->OptionsBehavior->EditorShowMode 设置为：Click (2)、View->OptionsBehavior->Editable 设置为：false
         *五、如何禁用GridControl中单击列弹出右键菜单==>设置Run Design->OptionsMenu->EnableColumnMenu 设置为：false
         *六、如何隐藏GridControl的GroupPanel表头==>设置Run Design->OptionsView->ShowGroupPanel 设置为：false
         */
        /// <summary>
        /// 删除全部行
        /// </summary>
        /// <param name="gridView">GridView</param>
        public static void ClearAllRows(this GridView gridView)
        {
            bool _mutilSelected = gridView.OptionsSelection.MultiSelect;//获取当前是否可以多选
            if (!_mutilSelected)
                gridView.OptionsSelection.MultiSelect = true;
            gridView.SelectAll();
            gridView.DeleteSelectedRows();
            gridView.OptionsSelection.MultiSelect = _mutilSelected;//还原之前是否可以多选状态
        }

        /// <summary>
        /// 清除数据绑定
        /// </summary>
        /// <param name="gridview">The gridview.</param>
        /// <param name="clearColumns">if set to <c>true</c>清除列.</param>
        /// 创建时间:2015-05-25 13:21
        /// 备注说明:<c>null</c>
        public static void ClearDataSource(this GridView gridview, bool clearColumns)
        {
            GridControl _gridControl = gridview.GridControl;
            try
            {
                _gridControl.BeginUpdate();
                if (clearColumns)
                    gridview.Columns.Clear();
                _gridControl.DataSource = null;
            }
            catch (Exception)
            {
            }
            finally
            {
                _gridControl.EndUpdate();
            }
        }

        /// <summary>
        /// 设置RepositoryItem是否可编辑
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="title">当不可编辑的时候，提示标题</param>
        /// <param name="content">当不可编辑的时候，提示内容</param>
        /// <param name="toolTip">ToolTipController.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="conditonHanlder">The conditon hanlder.</param>
        /// 创建时间:2015-05-26 13:48
        /// 备注说明:<c>null</c>
        public static void ConditionRepositoryItemEdit<T, TProperty>(this GridView view, string title, string content, ToolTipController toolTip, Expression<Func<T, TProperty>> keySelector, Func<T, bool> conditonHanlder)
            where T : class
        {
            string _filedName = keySelector.GetTPropertyName<T, TProperty>();
            view.ShowingEditor += (sender, e) =>
            {
                GridView _curView = sender as GridView;
                if (_curView.FocusedColumn.FieldName.Equals(_filedName))
                {
                    T _item = (T)view.GetFocusedRow();
                    if (conditonHanlder(_item))
                    {
                        e.Cancel = true;
                        Point _mousePoint = Control.MousePosition;
                        toolTip.ShowHint(content, title, _mousePoint);
                    }
                }
            };
        }

        /// <summary>
        /// RepositoryItem的数值验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="errorText">The error text.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="errorHanlder">The error hanlder.</param>
        /// 创建时间:2015-05-26 13:46
        /// 备注说明:<c>null</c>
        public static void ConditionRepositoryItemValidate<T, TProperty>(this GridView view, string errorText, Expression<Func<T, TProperty>> keySelector, Predicate<object> errorHanlder)
            where T : class
        {
            string _filedName = keySelector.GetTPropertyName<T, TProperty>();
            view.ValidatingEditor += (sender, e) =>
            {
                GridView _curView = sender as GridView;
                if (_curView.FocusedColumn.FieldName.Equals(_filedName))
                {
                    if (errorHanlder(e.Value))
                    {
                        e.Valid = false;
                        e.ErrorText = errorText;
                    }
                }
            };
        }

        /// <summary>
        ///  自定义GridControl按钮文字
        /// </summary>
        /// <param name="girdview">GridView</param>
        /// <param name="cusLocalizedKeyValue">需要转移的GridStringId，其对应的文字描述</param>
        public static void CustomButtonText(this GridView girdview, Dictionary<GridStringId, string> cusLocalizedKeyValue)
        {
            //eg:
            //private Dictionary<GridStringId, string> SetGridLocalizer()
            //{
            //    Dictionary<GridStringId, string> _gridLocalizer = new Dictionary<GridStringId, string>();
            //    _gridLocalizer.Add(GridStringId.FindControlFindButton, "查找");
            //    _gridLocalizer.Add(GridStringId.FindControlClearButton, "清空");
            //    return _gridLocalizer;
            //}
            //-------------------------------------------------------
            //Dictionary<GridStringId, string> _gridLocalizer = SetGridLocalizer();
            //gridView1.CustomButtonText(_gridLocalizer);
            BuilderGridLocalizer _bGridLocalizer = new BuilderGridLocalizer(cusLocalizedKeyValue);
            GridLocalizer.Active = _bGridLocalizer;
        }

        /// <summary>
        /// 打印Grid
        /// </summary>
        /// <param name="gridControl">The grid control.</param>
        /// <param name="printSetting">The print setting.</param>
        /// <param name="showPreview">if set to <c>true</c> 打印预览.</param>
        /// 创建时间:2015-05-26 11:26
        /// 备注说明:<c>null</c>
        public static void CustomPrint(this GridControl gridControl, PrintItem printSetting, bool showPreview)
        {
            using (PrintableComponentLink _print = new PrintableComponentLink(new PrintingSystem()))
            {
                _print.Component = gridControl;
                _print.Landscape = true;
                _print.PaperKind = printSetting.PaperKind;
                CustomPrintHeader(gridControl, _print, printSetting);
                CustomPrintFooter(gridControl, _print, printSetting);
                _print.CreateDocument();
                if (showPreview)
                    _print.ShowPreview();
                else
                    _print.PrintDlg();
            }
        }

        /// <summary>
        /// 绘制列头CheckEdit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="checkItem">The check item.</param>
        /// <param name="keySelector">The key selector.</param>
        /// 创建时间:2015-05-26 14:45
        /// 备注说明:<c>null</c>
        public static void DrawHeaderCheckBox<T, TProperty>(this GridView view, RepositoryItemCheckEdit checkItem, Expression<Func<T, TProperty>> keySelector)
            where T : class
        {
            string _filedName = keySelector.GetTPropertyName<T, TProperty>();
            view.CustomDrawColumnHeader += (sender, e) =>
            {
                GridView _curView = sender as GridView;
                if (e.Column != null && string.Compare(e.Column.FieldName, _filedName, true) == 0)
                {
                    e.Info.InnerElements.Clear();
                    e.Painter.DrawObject(e.Info);
                    DrawHeaderCheckBox(checkItem, e.Graphics, e.Bounds, getCheckedCount(_curView, _filedName) == _curView.DataRowCount);
                    e.Handled = true;
                }
            };
            view.MouseDown += (sender, e) =>
            {
                GridView _curView = sender as GridView;
                _curView.SyncCheckStatus(_filedName, e);
            };
        }

        /// <summary>
        ///绘制无数据行的时候提示信息
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// <param name="noRecordMsg">The no record MSG.</param>
        /// 创建时间:2015-05-26 14:59
        /// 备注说明:<c>null</c>
        public static void DrawNoRowCountMessage(this GridView gridView, string noRecordMsg)
        {
            gridView.CustomDrawEmptyForeground += (sender, e) =>
            {
                GridView _curView = sender as GridView;
                if (_curView.RowCount == 0)
                {
                    if (!string.IsNullOrEmpty(noRecordMsg))
                    {
                        Font _font = new Font("宋体", 10, FontStyle.Bold);
                        Rectangle _r = new Rectangle(e.Bounds.Left + 5, e.Bounds.Top + 5, e.Bounds.Width - 5, e.Bounds.Height - 5);
                        e.Graphics.DrawString(noRecordMsg, _font, Brushes.Black, _r);
                    }
                }
            };
        }

        /// <summary>
        /// 绘制行序号
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// 创建时间:2015-05-26 15:03
        /// 备注说明:<c>null</c>
        public static void DrawSequenceNumber(this GridView gridView)
        {
            if (gridView.IndicatorWidth != 40)
                gridView.IndicatorWidth = 40;
            gridView.CustomDrawRowIndicator += (sender, e) =>
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            };
        }

        /// <summary>
        /// 根据行，列索引来获取RepositoryItem
        /// </summary>
        /// <param name="view">GridView</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <returns>RepositoryItem</returns>
        public static RepositoryItem GetRepositoryItem(this GridView view, int rowIndex, int columnIndex)
        {
            GridViewInfo _viewInfo = view.GetViewInfo() as GridViewInfo;
            GridDataRowInfo _viewRowInfo = _viewInfo.RowsInfo.FindRow(rowIndex) as GridDataRowInfo;
            return _viewRowInfo.Cells[columnIndex].Editor;
        }

        /// <summary>
        /// 设置水平滚动条，并且自动根据内容调整列宽
        /// </summary>
        /// <param name="gridView">GridView</param>
        public static void HorzScroll(this GridView gridView)
        {
            gridView.BestFitColumns();
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.ScrollStyle = ScrollStyleFlags.LiveHorzScroll | ScrollStyleFlags.LiveVertScroll;
            gridView.HorzScrollVisibility = ScrollVisibility.Always;
        }

        /// <summary>
        /// 根据列来选中一行
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="colName">列名称</param>
        /// <param name="colValue">列值</param>
        public static void SelectedRow(this GridView gridView, string colName, object colValue)
        {
            gridView.ClearSelection();
            for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
            {
                object _cellValue = gridView.GetRowCellValue(rowHandle, colName);
                if (_cellValue != null)
                {
                    if (_cellValue == colValue)
                    {
                        gridView.SelectRow(rowHandle);
                        gridView.FocusedRowHandle = rowHandle;
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///设置GridControl绑定
        /// </summary>
        /// <param name="gridview">The gridview.</param>
        /// <param name="datasource">The datasource.</param>
        /// <param name="clearColumns">if set to <c>true</c> 清除之前绑定列.</param>
        /// 创建时间:2015-05-25 13:25
        /// 备注说明:<c>null</c>
        public static void SetDataSource(this GridView gridview, object datasource, bool clearColumns)
        {
            GridControl _gridControl = gridview.GridControl;
            try
            {
                _gridControl.BeginUpdate();
                if (clearColumns)
                    gridview.Columns.Clear();

                _gridControl.DataSource = null;
                _gridControl.DataSource = datasource;
            }
            catch (Exception)
            {
            }
            finally
            {
                _gridControl.EndUpdate();
            }
        }

        /// <summary>
        ///设置RepositoryItemButtonEdit
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// <param name="button">The button.</param>
        /// <param name="caption">The caption.</param>
        /// 创建时间:2015-05-26 17:34
        /// 备注说明:<c>null</c>
        public static void SetRepositoryItemButtonEdit(this GridView gridView, RepositoryItemButtonEdit button, string caption)
        {
            button.Buttons[0].Kind = ButtonPredefines.Glyph;
            button.Buttons[0].Caption = caption;
            button.TextEditStyle = TextEditStyles.HideTextEditor;
            if (!gridView.OptionsBehavior.Editable)
                gridView.OptionsBehavior.Editable = true;
        }

        /// <summary>
        /// 设定列的汇总信息
        /// <para>eg: gridView1.SetSummaryItem(DevExpress.Data.SummaryItemType.Sum, "合计={0:n2}", c => c.Age);</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="girdView">The gird view.</param>
        /// <param name="type">The type.</param>
        /// <param name="formatString">The format string.</param>
        /// <param name="keySelector">The key selector.</param>
        /// 创建时间:2015-05-26 15:13
        /// 备注说明:<c>null</c>
        public static void SetSummaryItem<T, TProperty>(this GridView girdView, SummaryItemType type, string formatString, Expression<Func<T, TProperty>> keySelector)
            where T : class
        {
            if (!girdView.OptionsView.ShowFooter)
                girdView.OptionsView.ShowFooter = true;

            string _fieldName = keySelector.GetTPropertyName<T, TProperty>();
            GridColumn _summaryColumn = girdView.Columns[_fieldName];
            if (_summaryColumn != null)
            {
                _summaryColumn.SummaryItem.SummaryType = type;
                if (!string.IsNullOrEmpty(formatString))
                    _summaryColumn.SummaryItem.DisplayFormat = formatString;
            }
        }

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
                grid.ExportToXls(_saveFileDialog.FileName, _options);
                DevMessageBoxHelper.ShowInfo("导出到Excel成功！");
            }
        }

        private static void CheckAll(GridView view, string fieldName)
        {
            for (int i = 0; i < view.DataRowCount; i++)
            {
                view.SetRowCellValue(i, view.Columns[fieldName], true);
            }
        }

        private static void CustomPrintFooter(GridControl gridControl, PrintableComponentLink print, PrintItem printSetting)
        {
            if (printSetting.PrintFooter)
            {
                print.CreateMarginalFooterArea += (sender, e) =>
                {
                    PageInfoBrick _rick = e.Graph.DrawPageInfo(PageInfo.None, printSetting.FooterText, printSetting.FooterColor,
                      new RectangleF(0, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
                    _rick.LineAlignment = BrickAlignment.Center;
                    _rick.Alignment = BrickAlignment.Center;
                    _rick.AutoWidth = true;
                    _rick.Font = printSetting.FooterFont;
                };
            }
        }

        private static void CustomPrintHeader(GridControl gridControl, PrintableComponentLink print, PrintItem printSetting)
        {
            if (printSetting.PrintHeader)
            {
                print.CreateMarginalHeaderArea += (sender, e) =>
                {
                    PageInfoBrick _rick = e.Graph.DrawPageInfo(PageInfo.None, printSetting.HeaderText, printSetting.HeaderColor,
                      new RectangleF(0, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
                    _rick.LineAlignment = BrickAlignment.Center;
                    _rick.Alignment = BrickAlignment.Center;
                    _rick.AutoWidth = true;
                    _rick.Font = printSetting.FooterFont;
                };
            }
        }

        private static void DrawHeaderCheckBox(RepositoryItemCheckEdit checkItem, Graphics g, Rectangle r, bool Checked)
        {
            CheckEditViewInfo _info;
            CheckEditPainter _painter;
            ControlGraphicsInfoArgs _args;
            _info = checkItem.CreateViewInfo() as CheckEditViewInfo;
            _painter = checkItem.CreatePainter() as CheckEditPainter;
            _info.EditValue = Checked;

            _info.Bounds = r;
            _info.PaintAppearance.ForeColor = Color.Black;
            _info.CalcViewInfo(g);
            _args = new ControlGraphicsInfoArgs(_info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            _painter.Draw(_args);
            _args.Cache.Dispose();
        }

        private static int getCheckedCount(GridView view, string filedName)
        {
            int count = 0;
            for (int i = 0; i < view.DataRowCount; i++)
            {
                object _cellValue = view.GetRowCellValue(i, view.Columns[filedName]);
                //if (_cellValue != null && !(_cellValue is DBNull))
                if (_cellValue == null) continue;
                if (string.IsNullOrEmpty(_cellValue.ToString().Trim())) continue;
                bool _checkStatus = false;
                if (bool.TryParse(_cellValue.ToString(), out _checkStatus))
                {
                    //if ((bool)_cellValue)
                    if (_checkStatus)
                        count++;
                }
            }
            return count;
        }

        private static void SyncCheckStatus(this GridView view, string fieldeName, MouseEventArgs e)
        {
            /*说明：
             *在MouseDown事件中使用
             *参考：https://www.devexpress.com/Support/Center/Question/Details/Q354489
             *eg:
             *private void gvLampConfig_MouseDown(object sender, MouseEventArgs e)
             *{
             *GridView _view = sender as GridView;
             *_view.SyncCheckStatus(gcCheckFieldName, e);
             *}
             */
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                view.ClearSorting();
                view.PostEditor();
                GridHitInfo _info;
                Point _pt = view.GridControl.PointToClient(Control.MousePosition);
                _info = view.CalcHitInfo(_pt);
                if (_info.InColumn && _info.Column.FieldName.Equals(fieldeName))
                {
                    if (getCheckedCount(view, fieldeName) == view.DataRowCount)
                        UnChekAll(view, fieldeName);
                    else
                        CheckAll(view, fieldeName);
                }
            }
        }

        private static void UnChekAll(GridView view, string fieldName)
        {
            for (int i = 0; i < view.DataRowCount; i++)
            {
                view.SetRowCellValue(i, view.Columns[fieldName], false);
            }
        }

        #endregion Methods
    }
}
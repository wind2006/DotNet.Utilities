namespace YanZhiwei.DotNet2.Utilities.WebForm
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Common;

    using Enums;

    /// <summary>
    ///  GridView 帮助类
    /// </summary>
    public static class GridViewHelper
    {
        #region Methods

        /// <summary>
        /// 点击列头排序初始设定
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="orderColumnName">排序依定的绑定数据列名</param>
        /// <param name="defaultOrderWay">排序方式，降序或升序</param>
        /// 时间：2015-11-02 17:21
        /// 备注：在Page_Load方法中使用；
        public static void InitializeColumnOrderBy(this GridView gridView, string orderColumnName, OrderWay defaultOrderWay)
        {
            if (!gridView.AllowSorting)
            {
                gridView.AllowSorting = true;
            }
            gridView.Attributes["SortOrder"] = orderColumnName;
            gridView.Attributes["OrderWay"] = defaultOrderWay.ToString();
        }

        /// <summary>
        /// 点击列头排序设定
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="finallyDataBindFactory">最后数据绑定委托</param>
        /// 时间：2015-11-02 17:21
        /// 备注：在OnInit事件中使用；
        public static void SetColumnOrderBy(this GridView gridView, Action<GridView, string, OrderWay> finallyDataBindFactory)
        {
            gridView.Sorting += (sender, e) =>
            {
                GridView _gridView = sender as GridView;
                string _sortExpression = e.SortExpression,
                       _orderWay = _gridView.Attributes["OrderWay"].ToString().Trim();
                OrderWay _curOrderby = OrderWay.Asc;
                if (_orderWay == OrderWay.Desc.ToString())
                {
                    _gridView.Attributes["OrderWay"] = OrderWay.Asc.ToString();
                    _curOrderby = OrderWay.Asc;
                }
                else if (_orderWay == OrderWay.Asc.ToString())
                {
                    _gridView.Attributes["OrderWay"] = OrderWay.Desc.ToString();
                    _curOrderby = OrderWay.Desc;
                }

                _gridView.Attributes["SortOrder"] = _sortExpression;
                finallyDataBindFactory(_gridView, _sortExpression, _curOrderby);
            };
        }

        /// <summary>
        /// GridView数据绑定
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="datasource">需要绑定数据源</param>
        /// 时间：2015-11-02 13:41
        /// 备注：
        public static void SetDataSource(this GridView gridView, object datasource)
        {
            gridView.DataSource = datasource;
            gridView.DataBind();
        }

        /// <summary>
        /// 设置数据自带分页
        /// </summary>
        /// <param name="gridview">GridView</param>
        /// <param name="pageSize">分页时每页大小</param>
        /// <param name="pagerStyle">自带分页样式</param>
        /// <param name="finallyDataBindFactory">最后数据绑定委托</param>
        /// 时间：2015-11-02 14:51
        /// 备注：在OnInit事件中使用；
        public static void SetOwnDataPager(this GridView gridview, int pageSize, PagerButtons pagerStyle, Action<GridView> finallyDataBindFactory)
        {
            if (!gridview.AllowPaging)
            {
                gridview.AllowPaging = true;
            }

            gridview.PagerSettings.Mode = pagerStyle;
            gridview.PageSize = pageSize;
            gridview.PageIndexChanging += (sender, e) =>
            {
                GridView _gridView = sender as GridView;
                _gridView.PageIndex = e.NewPageIndex;
                finallyDataBindFactory(_gridView);
            };
        }

        /// <summary>
        /// 定制化分页模板
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="finallyDataBindFactory">最后数据绑定委托</param>
        /// 时间：2015-11-02 15:57
        /// 备注：在OnInit事件中使用；
        public static void SetPagerTemplate(this GridView gridView, Action<GridView> finallyDataBindFactory)
        {
            /*
            * 分页模板
               <PagerTemplate>
                    <asp:LinkButton ID="lbtnFirst" runat="server" Font-Overline="false" CommandName="Page"
                        CommandArgument="1">首页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnPrev" runat="server" Font-Overline="false">上一页</asp:LinkButton>
                    <asp:PlaceHolder ID="phdPageNumber" runat="server"></asp:PlaceHolder>
                    <asp:LinkButton ID="lbtnNext" runat="server" Font-Overline="false">下一页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" Font-Overline="false" CommandName="Page"
                        CommandArgument='<%# gvCaptrueInfoRec.PageCount %>'>尾页</asp:LinkButton>
                    <asp:DropDownList ID="drpShowCount" CssClass="selectpicker" runat="server" AutoPostBack="True">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                </PagerTemplate>
            */
            gridView.RowCreated += (sender, e) =>
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    GridView _gridView = sender as GridView;
                    PlaceHolder _phdPageNumber = e.Row.FindControl("phdPageNumber") as PlaceHolder;
                    LinkButton _lbtnPrev = e.Row.FindControl("lbtnPrev") as LinkButton;
                    LinkButton _lbtnNext = e.Row.FindControl("lbtnNext") as LinkButton;
                    DropDownList _drpShowCount = e.Row.FindControl("drpShowCount") as DropDownList;
                    LinkButton _lbtnPage;
                    _drpShowCount.SelectedValue = _gridView.PageSize.ToString();
                    _drpShowCount.SelectedIndexChanged += delegate (object obj, EventArgs args)
                    {
                        _gridView.PageSize = Convert.ToInt16(_drpShowCount.SelectedValue);
                        finallyDataBindFactory(_gridView);
                    };
                    int _pageSize = _gridView.PageSize == 0 ? 10 : _gridView.PageSize;
                    int _pageCount = _gridView.PageCount;
                    int _pageIndex = _gridView.PageIndex;
                    int _startIndex = (_pageIndex + 1 < _pageSize) ?
                        0 : (_pageIndex + 1 + _pageSize / 2 >= _pageCount) ? _pageCount - _pageSize : _pageIndex - _pageSize / 2;
                    int _endIndex = (_startIndex >= _pageCount - _pageSize) ? _pageCount : _startIndex + _pageSize;

                    _phdPageNumber.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    for (int i = _startIndex; i < _endIndex; i++)
                    {
                        _lbtnPage = new LinkButton();
                        _lbtnPage.Text = (i + 1).ToString();
                        _lbtnPage.CommandName = "Page";
                        _lbtnPage.CommandArgument = (i + 1).ToString();
                        _lbtnPage.Font.Overline = false;
                        if (i == _pageIndex)
                            _lbtnPage.Font.Bold = true;
                        else
                            _lbtnPage.Font.Bold = false;
                        _phdPageNumber.Controls.Add(_lbtnPage);
                        _phdPageNumber.Controls.Add(new LiteralControl("&nbsp;"));
                    }
                    _lbtnPrev.Click += delegate (object obj, EventArgs args)
                    {
                        if (_gridView.PageIndex > 0)
                        {
                            _gridView.PageIndex = _gridView.PageIndex - 1;
                            finallyDataBindFactory(_gridView);
                        }
                    };
                    _lbtnNext.Click += delegate (object obj, EventArgs args)
                    {
                        if (_gridView.PageIndex < _gridView.PageCount)
                        {
                            _gridView.PageIndex = _gridView.PageIndex + 1;
                            finallyDataBindFactory(_gridView);
                        }
                    };
                    _phdPageNumber.Controls.Add(new LiteralControl(string.Format("&nbsp;&nbsp;{0} / {1}", _pageIndex + 1, _pageCount)));
                    _phdPageNumber.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                }
            };
        }

        /// <summary>
        /// 从GridView的数据生成DataTable
        /// </summary>
        /// <param name="gridview">GridView对象</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable(this GridView gridview)
        {
            DataTable _table = new DataTable();
            int _rowIndex = 0;
            List<string> cols = new List<string>();
            if (!gridview.ShowHeader && gridview.Columns.Count == 0)
            {
                return _table;
            }

            GridViewRow _headerRow = gridview.HeaderRow;
            int _columnCount = _headerRow.Cells.Count;
            for (int i = 0; i < _columnCount; i++)
            {
                string text = GetCellText(_headerRow.Cells[i]);
                cols.Add(text);
            }

            foreach (GridViewRow r in gridview.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    DataRow _row = _table.NewRow();
                    int j = 0;
                    for (int i = 0; i < _columnCount; i++)
                    {
                        string text = GetCellText(r.Cells[i]);
                        if (!string.IsNullOrEmpty(text))
                        {
                            if (_rowIndex == 0)
                            {
                                string columnName = cols[i];
                                if (string.IsNullOrEmpty(columnName))
                                {
                                    continue;
                                }

                                if (_table.Columns.Contains(columnName))
                                {
                                    continue;
                                }

                                DataColumn dc = _table.Columns.Add();
                                dc.ColumnName = columnName;
                                dc.DataType = typeof(string);
                            }

                            _row[j] = text;
                            j++;
                        }
                    }

                    _rowIndex++;
                    _table.Rows.Add(_row);
                }
            }

            return _table;
        }

        /// <summary>
        /// 将GridView导出到EXCEL
        /// </summary>
        /// <param name="gridview">GridView</param>
        /// <param name="fileName">导出到的文件名称</param>
        public static void ToExcel(this GridView gridview, string fileName)
        {
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF7;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString());
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            gridview.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            gridview.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 获取单元格内容
        /// </summary>
        /// <param name="cell">TableCell</param>
        /// <returns>内容</returns>
        private static string GetCellText(TableCell cell)
        {
            string text = cell.Text;
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }

            foreach (Control control in cell.Controls)
            {
                if (control != null && control is IButtonControl)
                {
                    IButtonControl btn = control as IButtonControl;
                    text = btn.Text.Replace("\r\n", string.Empty).Trim();
                    break;
                }

                if (control != null && control is ITextControl)
                {
                    LiteralControl lc = control as LiteralControl;
                    if (lc != null)
                    {
                        continue;
                    }

                    ITextControl l = control as ITextControl;
                    text = l.Text.Replace("\r\n", string.Empty).Trim();
                    break;
                }
            }

            return text;
        }

        /// <summary>
        /// 设置单元格内容
        /// </summary>
        /// <param name="cell">TableCell</param>
        /// <param name="maxLen">最大长度</param>
        private static void SetCellText(TableCell cell, int maxLen)
        {
            string text = cell.Text;
            if (!string.IsNullOrEmpty(text))
            {
                cell.Text = StringHelper.GetFriendly(text, maxLen);
            }

            foreach (Control control in cell.Controls)
            {
                if (control != null && control is IButtonControl)
                {
                    IButtonControl btn = control as IButtonControl;
                    text = btn.Text.Replace("\r\n", string.Empty).Trim();
                    btn.Text = StringHelper.GetFriendly(text, maxLen);
                    break;
                }

                if (control != null && control is ITextControl)
                {
                    LiteralControl _lc = control as LiteralControl;
                    if (_lc != null)
                    {
                        continue;
                    }

                    ITextControl _l = control as ITextControl;
                    text = _l.Text.Replace("\r\n", string.Empty).Trim();
                    if (_l is DataBoundLiteralControl)
                    {
                        cell.Text = StringHelper.GetFriendly(text, maxLen);
                        break;
                    }
                    else
                    {
                        _l.Text = StringHelper.GetFriendly(text, maxLen);
                        break;
                    }
                }
            }
        }

        #endregion Methods

        #region Other

        //    static readonly string sqlconnectionString = @"Server=YANZHIWEI-PC\SQLEXPRESS;DataBase=Test;uid=sa;pwd=sasa";
        //    MSSQLToolV2 SqlHelper = new MSSQLToolV2(sqlconnectionString);
        //    protected void Page_Load(object sender, EventArgs e)
        //    {
        //        if (!Page.IsPostBack)
        //        {
        //            ViewState["OrderType"] = "ASC";
        //            ViewState["SortColumn"] = "Date";
        //            BindLogToView();
        //        }
        //    }
        //    private void BindLogToView()
        //    {
        //        string _sql = "select  top 10 * from [Log]";
        //        DataTable _dtResource = SqlHelper.ExecuteDataTable(_sql, null);
        //        DataView _view = _dtResource.DefaultView;
        //        string _sort = (string)ViewState["SortColumn"] + " " + (string)ViewState["OrderType"];
        //        _view.Sort = _sort;
        //        this.GridView1.DataSource = _view;
        //        this.GridView1.DataBind();
        //    }
        //    /// <summary>
        //    /// 修改
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //    {
        //        try
        //        {
        //            string _id = GridView1.Rows[e.RowIndex].Cells[3].Text;
        //            if (!string.IsNullOrEmpty(_id))
        //            {
        //                SqlParameter[] _parameters = new SqlParameter[1];
        //                _parameters[0] = new SqlParameter("@ID", _id);
        //                string _sql = "delete [Log] where ID=@ID";
        //                if (SqlHelper.ExecuteNonQuery(_sql, _parameters) > 0)
        //                {
        //                    BuilderScriptAlert("删除成功");
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            BuilderScriptAlert(string.Format("删除失败,原因:{0}", ex.Message.Trim()));
        //        }
        //        finally
        //        {
        //            BindLogToView();
        //        }
        //    }
        //    private void BuilderScriptAlert(string message)
        //    {
        //        if (!string.IsNullOrEmpty(message))
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('" + message + "')", true);
        //        }
        //    }
        //    /// <summary>
        //    /// 进去编辑
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        //    {
        //        GridView1.EditIndex = e.NewEditIndex;
        //        BindLogToView();
        //    }
        //    /// <summary>
        //    /// 取消编辑
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //    {
        //        GridView1.EditIndex = -1;
        //        BindLogToView();
        //    }
        //    /// <summary>
        //    /// 更新
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //    {
        //        string _id = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(_id))
        //            {
        //                string _updateTime = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
        //                SqlParameter[] _parameters = new SqlParameter[2];
        //                _parameters[0] = new SqlParameter("@Date", _updateTime);
        //                _parameters[1] = new SqlParameter("@ID", _id);
        //                string _sql = "Update [Log] set Date=@Date where ID=@ID";
        //                if (SqlHelper.ExecuteNonQuery(_sql, _parameters) > 0)
        //                {
        //                    BuilderScriptAlert(string.Format("修改ID：{0}时间为{1}成功！", _id, DateTime.Parse(_updateTime)));
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            BuilderScriptAlert(string.Format("修改ID:{0}失败,原因：{1}", _id, ex.Message.Trim()));
        //        }
        //        finally
        //        {
        //            GridView1.EditIndex = -1;
        //            BindLogToView();
        //        }
        //    }
        //    /// <summary>
        //    /// 排序
        //    /// Remarks:1==>SortExpression="ID"
        //    /// 2==》AllowSorting="True"
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        //    {
        //        string _page = e.SortExpression;
        //        if (!string.IsNullOrEmpty(_page))
        //        {
        //            if (ViewState["SortColumn"].ToString() == _page)
        //            {
        //                if (ViewState["OrderType"].ToString() == "ASC")
        //                    ViewState["OrderType"] = "Desc";
        //                else
        //                    ViewState["OrderType"] = "ASC";
        //            }
        //            else
        //            {
        //                ViewState["SortColumn"] = e.SortExpression;
        //            }
        //            BindLogToView();
        //        }
        //    }
        //}

        #endregion Other
    }
}
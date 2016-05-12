using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.Enums;
using YanZhiwei.DotNet2.Utilities.WebForm;

namespace YanZhiwei.DotNet2.Utilities.WebFormExamples
{
    public partial class GridViewDemo : System.Web.UI.Page
    {
        private SqlServerHelper sqlHelper = new SqlServerHelper(@"server=YANZHIWEI-IT-PC\SQLEXPRESS;database=Northwind;uid=sa;pwd=sasa;");

        protected override void OnInit(EventArgs e)
        {
            SetDataPager(gvPage, btnPFirst, btnPNext, btnPPre, btnPLast, drpPShowCount, () => InitializeDataPager(gvPage, "Products", "ProductID,ProductName,QuantityPerUnit,UnitPrice,UnitsOnOrder,Discontinued", "ProductID", OrderWay.Asc, string.Empty, 1, drpPShowCount.Text.ToIntOrDefault(10)));

            gvDemo.SetOwnDataPager(10, PagerButtons.Numeric, gv => LoadProductListView(gv));
            gvDemo.SetPagerTemplate(gv => LoadProductListView(gv));
            gvDemo.SetColumnOrderBy((gv, orderByColumnName, orderWay) =>
            {
                LoadProductListView(gv, orderByColumnName, orderWay);
            });
            drpPShowCount.SelectedIndexChanged += DrpPShowCount_SelectedIndexChanged;
            gvPage.SetColumnOrderBy((gv, orderByColumnName, orderWay) =>
            {
                InitializeDataPager(gvPage, "Products", "ProductID,ProductName,QuantityPerUnit,UnitPrice,UnitsOnOrder,Discontinued", orderByColumnName, orderWay, string.Empty, 1, drpPShowCount.Text.ToIntOrDefault(10));
            });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvDemo.AllowPaging = true;
                gvDemo.InitializeColumnOrderBy("ProductID", OrderWay.Desc);
                gvPage.InitializeColumnOrderBy("ProductID", OrderWay.Desc);
                LoadProductListView(gvDemo);
                //--------------------------------------
                InitializeDataPager(gvPage, "Products", "ProductID,ProductName,QuantityPerUnit,UnitPrice,UnitsOnOrder,Discontinued", "ProductID", OrderWay.Asc, string.Empty, 1, 10);
                //LoadProductPageView(gvPage, 1, Convert.ToInt16(drpPShowCount.Text), "ProductID", OrderWay.Asc);
            }
        }

        private void btnDataPage_Click(object sender, EventArgs e)
        {
        }

        private void DataPagerAction(object sender, EventArgs e, GridView gridView)
        {
            Button _button = sender as Button;
            if (_button != null)
            {
                string _cmdName = _button.CommandName;
                int _pageIndex = Convert.ToInt32(gridView.Attributes["PageIndex"]),
                    _pageCount = Convert.ToInt32(gridView.Attributes["PageCount"]),
                    _pageSize = Convert.ToInt32(gridView.Attributes["PageSize"]);
                switch (_cmdName)
                {
                    case "next":
                        _pageIndex++;
                        break;

                    case "prev":
                        _pageIndex--;
                        break;

                    case "last":
                        _pageIndex = _pageCount;
                        break;

                    case "first":
                        _pageIndex = 1;
                        break;
                }
                InitializeDataPager(gvPage, "Products", "ProductID,ProductName,QuantityPerUnit,UnitPrice,UnitsOnOrder,Discontinued", "ProductID", OrderWay.Asc, string.Empty, _pageIndex, 10);
            }
        }

        private void DrpPShowCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductPageView(gvPage, 1, Convert.ToInt16(drpPShowCount.Text), "ProductID", OrderWay.Asc);
        }

        private void InitializeDataPager(GridView gridView, string tableName, string fields, string orderByColumn, OrderWay orderWay, string sqlWhere, int pageIndex, int pageSize)
        {
            var _pageResult = sqlHelper.StoreExecutePageQuery(tableName, fields, string.Format("{0} {1}", orderByColumn, orderWay), sqlWhere, pageSize, pageIndex);
            lblPCount.Text = _pageResult.Item2.ToString();
            lblPCurIndexValue.Text = pageIndex.ToString();
            lblPTotalCountValue.Text = _pageResult.Item3.ToString();
            if (pageIndex == 1)//当前页是否为首页
            {
                btnPFirst.Enabled = false;
                btnPPre.Enabled = false;
            }
            else
            {
                btnPFirst.Enabled = true;
                btnPPre.Enabled = true;
            }
            if (pageIndex == _pageResult.Item2)//当前页是否为尾页
            {
                btnPNext.Enabled = false;
                btnPLast.Enabled = false;
            }
            else
            {
                btnPNext.Enabled = true;
                btnPLast.Enabled = true;
            }
            gridView.Attributes["PageIndex"] = pageIndex.ToString();
            gridView.Attributes["PageCount"] = _pageResult.Item2.ToString();
            gridView.Attributes["PageSize"] = pageSize.ToString();
            gridView.SetDataSource(_pageResult.Item1);
        }

        private void LoadProductListView(GridView girdView, string orderByColumnName, OrderWay orderWay)
        {
            try
            {
                string _sql = @"select ProductID,ProductName,QuantityPerUnit,UnitPrice,UnitsOnOrder,Discontinued from dbo.Products";
                DataTable _table = sqlHelper.ExecuteDataTable(_sql, null);
                DataView _dataView = new DataView(_table);
                _dataView.Sort = string.Format("{0} {1}", orderByColumnName, orderWay);
                girdView.SetDataSource(_dataView);
            }
            catch (Exception ex)
            {
                ClientScriptHelper.Alert(ex.Message.Trim());
            }
        }

        private void LoadProductListView(GridView girdView)
        {
            try
            {
                string _sql = @"select ProductID,ProductName,QuantityPerUnit,UnitPrice,UnitsOnOrder,Discontinued from dbo.Products";
                DataTable _table = sqlHelper.ExecuteDataTable(_sql, null);
                girdView.SetDataSource(_table);
            }
            catch (Exception ex)
            {
                ClientScriptHelper.Alert(ex.Message.Trim());
            }
        }

        private void LoadProductPageView(GridView gvPage, int pageIndex, int pageSize, string orderByColumn, OrderWay orderWay)
        {
            var _pageResult = sqlHelper.StoreExecutePageQuery("Products", "ProductID,ProductName,QuantityPerUnit,UnitPrice,UnitsOnOrder,Discontinued", string.Format("{0} {1}", orderByColumn, orderWay), "", pageSize, pageIndex);
            lblPCount.Text = _pageResult.Item2.ToString();
            lblPCurIndexValue.Text = pageIndex.ToString();
            lblPTotalCountValue.Text = _pageResult.Item3.ToString();
            if (pageIndex == 1)//当前页是否为首页
            {
                btnPFirst.Enabled = false;
                btnPPre.Enabled = false;
            }
            else
            {
                btnPFirst.Enabled = true;
                btnPPre.Enabled = true;
            }
            if (pageIndex == _pageResult.Item2)//当前页是否为尾页
            {
                btnPNext.Enabled = false;
                btnPLast.Enabled = false;
            }
            else
            {
                btnPNext.Enabled = true;
                btnPLast.Enabled = true;
            }
            gvPage.Attributes["PageIndex"] = pageIndex.ToString();
            gvPage.Attributes["PageCount"] = _pageResult.Item2.ToString();
            gvPage.Attributes["PageSize"] = pageSize.ToString();
            gvPage.SetDataSource(_pageResult.Item1);
        }

        private void SetDataPager(GridView gridView, Button btnFirst, Button btnNext, Button btnPre, Button btnLast, DropDownList drpPageSize, Action finallyDataBindFactory)
        {
            btnFirst.CommandName = "first";
            btnNext.CommandName = "next";
            btnPre.CommandName = "prev";
            btnLast.CommandName = "last";
            if (!drpPageSize.AutoPostBack)
                drpPageSize.AutoPostBack = true;

            btnFirst.Click += (sender, e) => DataPagerAction(sender, e, gridView);
            btnNext.Click += (sender, e) => DataPagerAction(sender, e, gridView);
            btnPre.Click += (sender, e) => DataPagerAction(sender, e, gridView);
            btnLast.Click += (sender, e) => DataPagerAction(sender, e, gridView);

            drpPageSize.SelectedIndexChanged += (sender, e) =>
            {
                finallyDataBindFactory();
            };
        }
    }
}
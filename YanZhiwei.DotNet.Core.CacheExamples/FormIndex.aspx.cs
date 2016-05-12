using System;
using System.Diagnostics;
using YanZhiwei.DotNet.Core.Cache;
using YanZhiwei.DotNet4.Core.CacheProvider;
using YanZhiwei.DotNet.Core.Cache.Examples;
using YanZhiwei.DotNet.Core.Log;
using YanZhiwei.DotNet2.Utilities.Models;

namespace YanZhiwei.DotNet.Core.CacheExamples
{
    public partial class FormIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Core.Cache.CacheHelper.Set("LoginInfo_name", "YanZhiwei");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(Core.Cache.CacheHelper.Get("LoginInfo_name"));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            AppLogHelper.Instance.Info(LoggerType.WinExceptionLog, "测试");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            AppLogHelper.Instance.Info(LoggerType.ServiceExceptionLog, "测试");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            using (var edm = new NorthwindEntities1())
            {
                //List<Products> _rdp = edm.Products.ToList();
                // List<Products> _productList = QueryCacheHelper.ToCacheList(edm.Products.Where(c => c.ProductID > 10), 1);
                PageCondition _pageCondtion = new PageCondition();
                _pageCondtion.PageIndex = 1;
                _pageCondtion.PageSize = 10;
                _pageCondtion.SortConditions = new SortCondition[1];
                _pageCondtion.SortConditions[0] = new SortCondition("ProductName");

                PageResult<Products> _productPageResult = edm.Products.ToPageCache<Products, Products>(c => c.ProductID > 10
                                                                    , _pageCondtion
                                                                    , c => c
                                                                    );
                Products[] _productList = _productPageResult.Data;
            }
        }
    }
}
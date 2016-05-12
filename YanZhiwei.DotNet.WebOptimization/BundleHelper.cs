using System.Web.Optimization;
using YanZhiwei.DotNet.WebOptimization.Core;

namespace YanZhiwei.DotNet.WebOptimization
{
    /// <summary>
    /// Bundle 帮助类
    /// </summary>
    /// 创建时间:2015-06-11 14:32
    /// 备注说明:<c>null</c>
    public static class BundleHelper
    {
        /// <summary>
        /// 强制排序生成
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        /// <returns></returns>
        /// 创建时间:2015-06-11 14:32
        /// 备注说明:<c>null</c>
        public static Bundle ForceOrdered(this Bundle bundle)
        {
            /*
             *参考：
             *1. http://stackoverflow.com/questions/11979718/how-can-i-specify-an-explicit-scriptbundle-include-order
             *示例：
             *  BundleTable.Bundles.Add(new ScriptBundle("~/resource/baseScript").Include("~/assets/global/plugins/jquery.min.js",
                                                                                      "~/Scripts/jsUtils.js",
                                                                                      "~/Scripts/jqUtils.js",
                                                                                      "~/Scripts/json2.min.js",
                                                                                      "~/assets/global/plugins/jquery-ui/jquery-ui.min.js",
                                                                                      "~/assets/global/plugins/bootstrap/js/                                                                         bootstrap.min.js").ForceOrdered());
             */
            bundle.Orderer = new AsIsBundleOrderer();
            return bundle;
        }
    }
}
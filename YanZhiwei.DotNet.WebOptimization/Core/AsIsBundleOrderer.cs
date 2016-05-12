using System.Collections.Generic;
using System.Web.Optimization;

namespace YanZhiwei.DotNet.WebOptimization.Core
{
    internal class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
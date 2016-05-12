using System.Linq;
using YanZhiwei.Ninject.TS.Models;

namespace YanZhiwei.Ninject.TS
{
    public class LinqValueCalculator : IValueCalculator
    {
        public decimal ValueProducts(params Product[] products)
        {
            return products.Sum(p => p.Price);
        }
    }
}
using System.Linq;
using YanZhiwei.Ninject.TS.Models;

namespace YanZhiwei.Ninject.TS
{
    public class LinqValueCalculator2 : IValueCalculator
    {
        private IDiscountHelper discounter;

        public LinqValueCalculator2(IDiscountHelper discountParam)
        {
            discounter = discountParam;
        }

        public decimal ValueProducts(params Product[] products)
        {
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}
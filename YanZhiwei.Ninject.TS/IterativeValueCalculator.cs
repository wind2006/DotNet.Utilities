using YanZhiwei.Ninject.TS.Models;

namespace YanZhiwei.Ninject.TS
{
    public class IterativeValueCalculator : IValueCalculator
    {
        public decimal ValueProducts(params Product[] products)
        {
            decimal totalValue = 0;
            foreach (Product p in products)
            {
                totalValue += p.Price;
            }
            return totalValue;
        }
    }
}
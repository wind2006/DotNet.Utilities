using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YanZhiwei.Ninject.TS
{
    public class LimitShoppingCart : ShoppingCart
    {
        public LimitShoppingCart(IValueCalculator calcParam)
            : base(calcParam)
        {
        }

        public override decimal CalculateStockValue()
        {
            //过滤价格超过了上限的商品
            var filteredProducts = products.Where(e => e.Price < ItemLimit);

            return calculator.ValueProducts(filteredProducts.ToArray());
        }

        public decimal ItemLimit { get; set; }
    } 
}

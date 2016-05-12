using YanZhiwei.Ninject.TS.Models;
namespace YanZhiwei.Ninject.TS
{
    public interface IValueCalculator
    {
        decimal ValueProducts(params Product[] products);
    }
}
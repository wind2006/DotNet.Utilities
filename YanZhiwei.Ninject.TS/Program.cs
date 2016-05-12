using Ninject;
using System;

namespace YanZhiwei.Ninject.TS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //创建Ninject内核实例
            IKernel ninjectKernel = new StandardKernel();

            #region demo1 一般绑定

            ////绑定接口到实现了该接口的类
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator2>();
            //// 获得实现接口的对象实例
            //IValueCalculator calcImpl = ninjectKernel.Get<IValueCalculator>();
            //// 创建ShoppingCart实例并注入依赖
            //ShoppingCart cart = new ShoppingCart(calcImpl);
            //// 计算商品总价钱并输出结果
            // Console.WriteLine("Total: {0:c}", cart.CalculateStockValue());

            #endregion demo1 一般绑定

            #region demo2 指定值绑定

            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator2>();
            //ninjectKernel.Bind<IDiscountHelper>()
            //    .To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 5M);
            ////例化的动作是Ninject自动完成的，怎么告诉Ninject在实例化类的时候给某属性赋一个指定的值呢？这时就需要用到参数绑定，我们在绑定的时候可以通过给WithPropertyValue方法传参的方式指定DiscountSize属性的值，
            //IValueCalculator calcImpl = ninjectKernel.Get<IValueCalculator>();
            //ShoppingCart cart = new ShoppingCart(calcImpl);
            //Console.WriteLine("Total: {0:c}", cart.CalculateStockValue());

            #endregion demo2 指定值绑定

            #region demo3 自我绑定

            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator2>();
            //ninjectKernel.Bind<IDiscountHelper>()
            //    .To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 5M);
            //ShoppingCart _cart = ninjectKernel.Get<ShoppingCart>();
            //Console.WriteLine("Total: {0:c}", _cart.CalculateStockValue());
            //这种写法不需要关心ShoppingCart类依赖哪个接口，也不需要手动去获取该接口的实现(calcImpl)。当通过这句代码请求一个ShoppingCart类的实例的时候，Ninject会自动判断依赖关系，并为我们创建所需接口对应的实现。
            /*
             *ninjectKernel.Bind<ShoppingCart>().ToSelf();
             *ShoppingCart cart = ninjectKernel.Get<ShoppingCart>();
             *这里有自我绑定用的是ToSelf方法，在本示例中可以省略该句。但用ToSelf方法自我绑定的好处是可以在其后面用WithXXX方法指定构造函数参数、属性等等的值。
             */

            #endregion demo3 自我绑定

            #region demo4 派生类绑定

            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator2>();
            //ninjectKernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 5M);
            ////派生类绑定
            //ninjectKernel.Bind<ShoppingCart>().To<LimitShoppingCart>().WithPropertyValue("ItemLimit", 3M);

            //ShoppingCart cart = ninjectKernel.Get<ShoppingCart>();
            //Console.WriteLine("Total: {0:c}", cart.CalculateStockValue());

            /*
             * cart对象调用的是子类的CalculateStockValue方法，证明了可以把父类绑定到一个继承自该父类的子类。通过派生类绑定，当我们请求父类的时候，Ninject自动帮我们创建一个对应的子类的实例，并将其返回。由于抽象类不能被实例化，所以派生类绑定在使用抽象类的时候非常有用。
             */

            #endregion demo4 派生类绑定

            #region demo5 条件绑定

            ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator2>();
            ninjectKernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 5M);
            //派生类绑定
            ninjectKernel.Bind<ShoppingCart>().To<LimitShoppingCart>().WithPropertyValue("ItemLimit", 3M);
            //条件绑定
            ninjectKernel.Bind<IValueCalculator>().To<IterativeValueCalculator>().WhenInjectedInto<LimitShoppingCart>();

            ShoppingCart cart = ninjectKernel.Get<ShoppingCart>();
            Console.WriteLine("Total: {0:c}", cart.CalculateStockValue());
            /*
             * IValueCalculator接口现在有两个实现：IterativeValueCalculator和LinqValueCalculator。我们可以指定，如果是把该接口的实现注入到LimitShoppingCart类，那么就用IterativeValueCalculator，其他情况都用LinqValueCalculator。
             * 
             * 即调用的是计算方法是IterativeValueCalculator的ValueProducts方法。可见，Ninject会查找最匹配的绑定，如果没有找到条件绑定，则使用默认绑定。在条件绑定中，除了WhenInjectedInto方法，还有When和WhenClassHas等方法
             */

            #endregion

            Console.ReadLine();
        }
    }
}
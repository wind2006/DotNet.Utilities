using Microsoft.Practices.Unity;
using System;

namespace YanZhiwei.DotNet.EntLib4.TS
{
    /*
     知识：
     * 1.RegisterType：这个方法可以往container中注册一种类型或映射关系，当我们需要调用该类型的实例时，container会自动实例化该类型的对象，无需通过new someName方法实例化对象（例如：使用Resolve或ResolveAll方法获取注册类型的实例），当没有指定实例化对象的生命周期，将使用默认的TransientLifetimeManager（每次调用Resolve或ResolveAll方法时都会实例化一个新的对象）
     * 
     * 2.RegisterInstance：这个方法是往container中注册一个已存在实例，通过Resolve或ResolveAll方法获取该类型的实例，默认使用ContainerControlledLifetimeManager管理对象生命周期，而且container中会保持对象的引用（简而言之每次调用Resolve或ResolveAll方法都会调用同一个对象的引用）。
     * 
     * 3.使用Register方法可以往容器中注册一种类型或映射关系
     *     往容器中注册一种类型RegisterType<Type>
     *     往容器中注册一种映射关系RegisterType< RegisteredType, TargetType >
     
     */
    #region 1. 构造注入

    /*
      知识：
     *1. 如果注册的类型构造方法参数中需要依赖其他自定义类型，那么 Unity 会在你调用 Resolve 时自动生成，并传递给目标构造参数。
     *2. 如果注册类型有多个构造方法时，我们可以用 InjectionConstructorAttribute 特性告知容器如何选择。
     *3. 如果不使用 InjectionConstructorAttribute，那么 Unity 默认会选择参数最多的那个构造方法。
     *4. 如果有多个 "参数最多" 的构造方法时，会抛出异常；
     */

    //public class Data
    //{
    //    public Data()
    //    {
    //        Console.WriteLine("Data constructor");
    //    }
    //}

    //public interface ICache
    //{
    //    Data Data { get; set; }
    //}

    //public class MemoryCache : ICache
    //{
    //    #region 1

    //    //public MemoryCache(Data data)
    //    //{
    //    //    this.Data = data;
    //    //}

    //    #endregion 1

    //    #region 2

    //    //public MemoryCache()
    //    //{
    //    //    Console.WriteLine("Constructor1");
    //    //}
    //    //[InjectionConstructor]
    //    //public MemoryCache(Data data)
    //    //{
    //    //    Console.WriteLine("Constructor2");
    //    //}

    //    #endregion 2

    //    #region 3

    //    public MemoryCache()
    //    {
    //        Console.WriteLine("Constructor1");
    //    }

    //    public MemoryCache(Data data)
    //    {
    //        Console.WriteLine("Constructor2");
    //    }

    //    public MemoryCache(Data data, Data data2)
    //    {
    //        Console.WriteLine("Constructor3");
    //    }


    //    #endregion 3

    //    public Data Data { get; set; }
    //}

    //internal class Program
    //{
    //    private static void Main(string[] args)
    //    {
    //        var container = new UnityContainer();
    //        container.RegisterType<ICache, MemoryCache>();

    //        var cache = container.Resolve<ICache>();
    //        Console.WriteLine(cache.Data != null);
    //    }
    //}

    #endregion 1. 构造注入

    #region 2. 属性注入

    /*
      知识：
     *1. 同样使用一个特性 DependencyAttribute，我们可以让容器自动替我们完成属性赋值操作
     *
     */
    //public class Data
    //{
    //    public Data()
    //    {
    //        Console.WriteLine("Data constructor");
    //    }
    //}

    //public interface ICache
    //{
    //    Data Data { get; set; }
    //}

    //public class MemoryCache : ICache
    //{
    //    [Dependency]
    //    public Data Data { get; set; }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var container = new UnityContainer();
    //        container.RegisterType<ICache, MemoryCache>();

    //        var cache = container.Resolve<ICache>();
    //        Console.WriteLine(cache.Data != null);
    //    }
    //}

    #endregion 2. 属性注入

    #region 3. 方法注入

    /*
      知识：
     *1.方法注入更像一个被自动调用的初始化操作，当然这个方法也可以像构造方法那样拥有依赖类型参数。
     *2.我们还可以利用 DependencyAttribute 特性指定方法(含构造方法)参数的注入类型。
     */
    public class Data
    {
        public Data()
        {
            Console.WriteLine("Data constructor");
        }
    }

    public interface ICache
    {
        Data Data { get; set; }
        void Initialize(Data data);
    }

    public class MemoryCache : ICache
    {
        public Data Data { get; set; }

        [InjectionMethod]
        public void Initialize(Data data)
        {
            this.Data = data;
            Console.WriteLine("Initialize");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer().
              RegisterType<ICache, MemoryCache>();

            var cache = container.Resolve<ICache>();
            Console.WriteLine(cache.Data != null);
        }
    }

    #endregion 3. 方法注入


}
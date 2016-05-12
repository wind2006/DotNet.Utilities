namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Singleton泛型类
    /// </summary>
    /// <typeparam name="T">带默认构造函数的泛型</typeparam>
    public sealed class Singleton<T>
        where T : new()
    {
        #region Fields

        private static T instance = new T();
        private static object lockHelper = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        private Singleton()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 获取实例
        /// </summary>
        public static T GetInstance()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// 设置实例
        /// </summary>
        /// <param name="value">泛型实例</param>
        public void SetInstance(T value)
        {
            instance = value;
        }

        #endregion Methods
    }

    /// <summary>
    /// 提供一个字典容器，按类型装载所有<see cref="Singleton&lt;T&gt;"/>的单例实例
    /// </summary>
    public class Singleton
    {
        #region Constructors

        static Singleton()
        {
            if (AllSingletons == null)
            {
                AllSingletons = new Dictionary<Type, object>();
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 获取 单例对象字典
        /// </summary>
        public static IDictionary<Type, object> AllSingletons
        {
            get; private set;
        }

        #endregion Properties
    }
}
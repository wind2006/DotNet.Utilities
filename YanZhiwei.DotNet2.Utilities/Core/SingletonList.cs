namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// 创建一个类型列表的单例，该实例的生命周期将跟随整个应用程序
    /// </summary>
    /// <typeparam name="T">要创建的列表元素的类型</typeparam>
    public class SingletonList<T> : SingletonType<IList<T>>
    {
        #region Constructors

        static SingletonList()
        {
            SingletonType<IList<T>>.Instance = new List<T>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 获取指定类型的列表的单例实例
        /// </summary>
        public static new IList<T> Instance
        {
            get { return SingletonType<IList<T>>.Instance; }
        }

        #endregion Properties
    }
}
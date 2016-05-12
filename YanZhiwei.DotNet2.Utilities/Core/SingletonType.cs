namespace YanZhiwei.DotNet2.Utilities.Core
{
    /// <summary>
    /// 定义一个指定类型的单例，该实例的生命周期将跟随整个应用程序。
    /// </summary>
    /// <typeparam name="T">要创建单例的类型。</typeparam>
    public class SingletonType<T> : Singleton
    {
        private static T instance;

        /// <summary>
        /// 获取指定类型的单例实例
        /// </summary>
        public static T Instance
        {
            get { return instance; }
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}
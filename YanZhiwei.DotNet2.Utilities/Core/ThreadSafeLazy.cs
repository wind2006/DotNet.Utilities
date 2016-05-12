namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;

    /// <summary>
    /// Lazy 基于.NET 2.0的实现帮助类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public sealed class ThreadSafeLazy<T>
    {
        #region Fields

        /*
         * 参考：
         * 1. http://stackoverflow.com/questions/3207580/implementation-of-lazyt-for-net-3-5
         */
        /// <summary>
        /// 委托
        /// </summary>
        private readonly Func<T> createValue;

        /// <summary>
        /// The synchronize root
        /// </summary>
        private readonly object syncRoot = new object();

        /// <summary>
        /// The is value created
        /// </summary>
        private bool isValueCreated;

        /// <summary>
        /// The value
        /// </summary>
        private T value;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="createValue">委托</param>
        public ThreadSafeLazy(Func<T> createValue)
        {
            this.createValue = createValue;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 是否初始化
        /// </summary>
        public bool IsValueCreated
        {
            get
            {
                lock (syncRoot)
                {
                    return isValueCreated;
                }
            }
        }

        /// <summary>
        /// 获取初始化值
        /// </summary>
        public T Value
        {
            get
            {
                if (!isValueCreated)
                {
                    lock (syncRoot)
                    {
                        if (!isValueCreated)
                        {
                            value = createValue();
                            isValueCreated = true;
                        }
                    }
                }

                return value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion Methods
    }
}
namespace YanZhiwei.DotNet3._5.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// http 存储抽象对象
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public abstract class IHttpStorageObject<T>
    {
        #region Fields

        /// <summary>
        /// HttpContext 对象
        /// </summary>
        public HttpContext context = HttpContext.Current;

        /// <summary>
        /// 天
        /// </summary>
        public int Day = 60 * 60 * 24;

        /// <summary>
        /// 小时
        /// </summary>
        public int Hour = 60 * 60;

        /// <summary>
        /// 分钟
        /// </summary>
        public int Minutes = 60;

        #endregion Fields

        #region Indexers

        /// <summary>
        /// 索引
        /// </summary>
        public abstract T this[string key]
        {
            get;
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public abstract void Add(string key, T value);

        /// <summary>
        /// 是否包含某个键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否包含</returns>
        public abstract bool ContainsKey(string key);

        /// <summary>
        /// 按键取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public abstract T Get(string key);

        /// <summary>
        /// 获取所有键
        /// </summary>
        /// <returns>集合</returns>
        public abstract IEnumerable<string> GetAllKey();

        /// <summary>
        /// 按键移除
        /// </summary>
        /// <param name="key">键</param>
        public abstract void Remove(string key);

        /// <summary>
        /// 移除所有
        /// </summary>
        public abstract void RemoveAll();

        /// <summary>
        /// 移除所有
        /// </summary>
        /// <param name="keySelector">选择器</param>
        public abstract void RemoveAll(Func<string, bool> keySelector);

        #endregion Methods
    }
}
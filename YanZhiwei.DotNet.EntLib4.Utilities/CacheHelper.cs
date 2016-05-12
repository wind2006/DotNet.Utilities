using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using System;
using System.IO;

namespace YanZhiwei.DotNet.EntLib4.Utilities
{
    /// <summary>
    /// 企业库 缓存帮助类
    /// </summary>
    public class CacheHelper
    {
        /*
         *在Caching Application Block中，主要提供以下四种保存缓存数据的途径，
         *分别是：内存存储（默认）、独立存储（Isolated Storage）、
         *数据库存储（DataBase Cache Storage）和自定义存储（Custom Cache Storage）。
         *In-Memory：保存在内存中。
         *Isolated Storage Cache Store：系统将缓存的信息保存在独立文件中（C:\Users\<<user name>>\AppData\Local\IsolatedStorage）。
         *Data Cache Storage：将缓存数据保存在数据库中。（需要运行CreateCachingDatabase.sql脚本）
         *Custom Cache Storage：自己扩展的处理器。我们可以将数据保存在注册表中或文本文件中。

         *
         * 缓存等级，在企业库的缓存模块中已经提供了4个缓存等级：Low，Normal，High和NotRemovable，在超出最大缓存数量后会自动根据缓存等级来移除对象。
        *  过期方式，企业库默认提供4种过期方式
        * AbsoluteTime：绝对是时间过期，传递一个时间对象指定到时过期
        * SlidingTime：缓存在最后一次访问之后多少时间后过期，默认为2分钟，有2个构造函数可以指定一个过期时间或指定一个过期时间和一个最后使用时
        * ExtendedFormatTime ：指定过期格式，以特定的格式来过期，通过ExtendedFormat.cs类来包装过期方式，具体可参照ExtendedFormat.cs，源代码中已经给出了很多方式
        * FileDependency：依赖于文件过期，当所依赖的文件被修改则过期，这个我觉得很有用，因为在许多网站，如论坛、新闻系统等都需要大量的配置，可以将配置文件信息进行缓存，将依赖项设为配置文件，这样当用户更改了配置文件后通过ICacheItemRefreshAction.Refresh可以自动重新缓存。
         *
         * new ExtendedFormatTime("0 0 * * *") stands for Minutes, Hours, Days, Months, DaysOfWeeks.  Spaces between 0 and * are there for parsing purpose.
         * Extended format syntax :
         * Minute - 0-59
         * Hour - 0-23
         * Day of month - 1-31
         * Month - 1-12
         * Day of week - 0-6 (Sunday is 0)
         * Wildcards - * means run every

         * Examples:
         * * * * * * - expires every minute
         * 5 * * * * - expire 5th minute of every hour
         * * 21 * * * - expire every minute of the 21st hour of every day
         * 31 15 * * * - expire 3:31 PM every day
         * 7 4 * * 6 - expire Saturday 4:07 AM
         * 15 21 4 7 * - expire 9:15 PM on 4 July
         * Therefore 6 6 6 6 1 means:
         * have we crossed/entered the 6th minute AND
         * have we crossed/entered the 6th hour AND
         * have we crossed/entered the 6th day AND
         * have we crossed/entered the 6th month AND
         * have we crossed/entered A MONDAY?
         */

        #region 变量

        private static readonly object syncRoot = new object();
        private ICacheManager cacheMgr = null;

        #endregion 变量

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheHelper"/> class.
        /// </summary>
        public CacheHelper()
        {
            cacheMgr = CacheFactory.GetCacheManager();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheHelper"/> class.
        /// </summary>
        /// <param name="cacheMangeName">Name of the cache mange.</param>
        public CacheHelper(string cacheMangeName)
        {
            cacheMgr = CacheFactory.GetCacheManager(cacheMangeName);
        }

        /// <summary>
        /// Gets the cache manager.
        /// </summary>
        /// <value>
        /// The cache manager.
        /// </value>
        public ICacheManager CacheManager
        {
            get
            {
                lock (syncRoot)
                {
                    return cacheMgr;
                }
            }
        }

        /// <summary>
        /// Adds the absolute time.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteTime">The absolute time.</param>
        /// <param name="itemRefreshAction">The item refresh action.</param>
        public void AddAbsoluteTime(string key, object value, DateTime absoluteTime, ICacheItemRefreshAction itemRefreshAction)
        {
            CacheManager.Add(key, value, CacheItemPriority.Normal, itemRefreshAction, new AbsoluteTime(absoluteTime) { });
        }

        /// <summary>
        /// Adds the absolute time.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteTime">The absolute time.</param>
        public void AddAbsoluteTime(string key, object value, DateTime absoluteTime)
        {
            AddAbsoluteTime(key, value, absoluteTime, null);
        }

        /// <summary>
        /// Adds the absolute time.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteTime">The absolute time.</param>
        public void AddAbsoluteTime(string key, object value, TimeSpan absoluteTime)
        {
            AddAbsoluteTime(key, value, absoluteTime, null);
        }

        /// <summary>
        /// Adds the absolute time.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteTime">The absolute time.</param>
        /// <param name="itemRefreshAction">The item refresh action.</param>
        public void AddAbsoluteTime(string key, object value, TimeSpan absoluteTime, ICacheItemRefreshAction itemRefreshAction)
        {
            CacheManager.Add(key, value, CacheItemPriority.Normal, itemRefreshAction, new AbsoluteTime(absoluteTime) { });
        }

        /// <summary>
        /// Adds the file dependency.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="filePath">The file path.</param>
        public void AddFileDependency(string key, object value, string filePath)
        {
            AddFileDependency(key, value, filePath, null);
        }

        /// <summary>
        /// Adds the file dependency.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="itemRefreshAction">The item refresh action.</param>
        public void AddFileDependency(string key, object value, string filePath, ICacheItemRefreshAction itemRefreshAction)
        {
            if (File.Exists(filePath))
            {
                FileDependency _fileDependency = new FileDependency(filePath);
                CacheManager.Add(key, value, CacheItemPriority.Normal, itemRefreshAction, _fileDependency);
            }
        }

        /// <summary>
        /// Adds the sliding time.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="slidingTime">The sliding time.</param>
        public void AddSlidingTime(string key, object value, TimeSpan slidingTime)
        {
            AddSlidingTime(key, value, slidingTime, null);
        }

        /// <summary>
        /// Adds the sliding time.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="slidingTime">The sliding time.</param>
        /// <param name="itemRefreshAction">The item refresh action.</param>
        public void AddSlidingTime(string key, object value, TimeSpan slidingTime, ICacheItemRefreshAction itemRefreshAction)
        {
            CacheManager.Add(key, value, CacheItemPriority.Normal, itemRefreshAction, new SlidingTime(slidingTime) { });
        }

        /// <summary>
        /// Adds the extended format time.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="ExtendedFormatString">The extended format string.</param>
        /// <param name="itemRefreshAction">The item refresh action.</param>
        public void AddExtendedFormatTime(string key, object value, string ExtendedFormatString, ICacheItemRefreshAction itemRefreshAction)
        {
            cacheMgr.Add(key, value, CacheItemPriority.Normal, itemRefreshAction, new ExtendedFormatTime(ExtendedFormatString) { });
        }

        /// <summary>
        /// Adds the extended format time.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="ExtendedFormatString">The extended format string.</param>
        public void AddExtendedFormatTime(string key, object value, string ExtendedFormatString)
        {
            AddExtendedFormatTime(key, value, ExtendedFormatString);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetData<T>(string key)
        {
            T _result = default(T);
            if (CacheManager.Contains(key))
                _result = (T)CacheManager.GetData(key);
            return _result;
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            if (CacheManager.Contains(key))
                CacheManager.Remove(key);
        }

        /// <summary>
        /// Removes all.
        /// </summary>
        public void RemoveAll()
        {
            if (CacheManager != null)
            {
                CacheManager.Flush();
            }
        }
    }
}
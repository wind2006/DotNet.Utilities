using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using System;

namespace YanZhiwei.DotNet.EntLib2.Utilities
{
    /// <summary>
    /// Cache 帮助类
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
 * 过期方式，企业库默认提供4种过期方式
 * AbsoluteTime：绝对是时间过期，传递一个时间对象指定到时过期
 * SlidingTime：缓存在最后一次访问之后多少时间后过期，默认为2分钟，有2个构造函数可以指定一个过期时间或指定一个过期时间和一个最后使用时
 * ExtendedFormatTime ：指定过期格式，以特定的格式来过期，通过ExtendedFormat.cs类来包装过期方式，具体可参照ExtendedFormat.cs，源代码中已经给出了很多方式
 * FileDependency：依赖于文件过期，当所依赖的文件被修改则过期，这个我觉得很有用，因为在许多网站，如论坛、新闻系统等都需要大量的配置，可以将配置文件信息进行缓存，将依赖项设为配置文件，这样当用户更改了配置文件后通过ICacheItemRefreshAction.Refresh可以自动重新缓存。
 */

        ///// <summary>
        ///// 自定义缓存刷新操作
        ///// </summary>
        //[Serializable]
        //public class CacheItemRefreshAction : ICacheItemRefreshAction
        //{
        //    #region ICacheItemRefreshAction 成员
        //    /// <summary>
        //    /// 自定义刷新操作
        //    /// </summary>
        //    /// <param name="removedKey">移除的键</param>
        //    /// <param name="expiredValue">过期的值</param>
        //    /// <param name="removalReason">移除理由</param>
        //    void ICacheItemRefreshAction.Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason)
        //    {
        //        if (removalReason == CacheItemRemovedReason.Expired)
        //        {
        //            CacheManager cache = CacheFactory.GetCacheManager();
        //            cache.Add(removedKey, expiredValue);
        //        }
        //    }
        //    #endregion
        //}

        private static CacheManager CacheMgr = null;

        static CacheHelper()
        {
            CacheMgr = CacheFactory.GetCacheManager();
        }

        #region 获取CacheManager实例

        /// <summary>
        /// 获取CacheManager实例
        /// </summary>
        /// <returns>CacheManager</returns>
        public static CacheManager Instance()
        {
            return CacheMgr;
        }

        #endregion 获取CacheManager实例

        #region 添加缓存

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Add(string key, object value)
        {
            CacheMgr.Add(key, value);
        }

        #endregion 添加缓存

        #region 添加缓存_滑动过期_小时

        /// <summary>
        /// 添加缓存_滑动过期_小时
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="hour">小时</param>
        public static void AddWithHour(string key, object value, int hour)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromHours(hour)));
        }

        #endregion 添加缓存_滑动过期_小时

        #region 添加缓存_滑动过期_天

        /// <summary>
        ///  添加缓存_滑动过期_天
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="days">天</param>
        public static void AddWithDay(string key, object value, int days)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromDays(days)));
        }

        #endregion 添加缓存_滑动过期_天

        #region 添加缓存_滑动过期_毫秒

        /// <summary>
        ///  添加缓存_滑动过期_毫秒
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="millisecond">毫秒</param>
        public static void AddWithMillisecond(string key, object value, int millisecond)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromMilliseconds(millisecond)));
        }

        #endregion 添加缓存_滑动过期_毫秒

        #region 添加缓存_滑动过期_分钟

        /// <summary>
        ///添加缓存_滑动过期_分钟
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">分钟</param>
        public static void AddWithMinutes(string key, object value, int minutes)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromMinutes(minutes)));
        }

        #endregion 添加缓存_滑动过期_分钟

        #region 添加缓存_滑动过期_秒

        /// <summary>
        ///添加缓存_滑动过期_秒
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="seconds">秒</param>
        public static void AddWithSeconds(string key, object value, int seconds)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromSeconds(seconds)));
        }

        #endregion 添加缓存_滑动过期_秒

        #region 添加缓存_滑动过期_文件依赖

        /// <summary>
        /// 添加缓存_滑动过期_文件依赖
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="filePath">文件路径</param>
        public static void AddFileDependency(string key, object value, string filePath)
        {
            FileDependency _fileDependency = new FileDependency(filePath);
            CacheMgr.Add(key, value, CacheItemPriority.Normal, null, _fileDependency);
        }

        #endregion 添加缓存_滑动过期_文件依赖

        #region 添加缓存_滑动过期_小时

        /// <summary>
        /// 添加缓存_滑动过期_小时
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="hour">小时</param>
        /// <param name="refreshAction">ICacheItemRefreshAction</param>
        public static void AddWithHour(string key, object value, int hour, ICacheItemRefreshAction refreshAction)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, refreshAction, new SlidingTime(TimeSpan.FromHours(hour)));
        }

        #endregion 添加缓存_滑动过期_小时

        #region 添加缓存_滑动过期_天

        /// <summary>
        /// 添加缓存_滑动过期_天
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="days">天</param>
        /// <param name="refreshAction">ICacheItemRefreshAction</param>
        public static void AddWithDay(string key, object value, int days, ICacheItemRefreshAction refreshAction)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, refreshAction, new SlidingTime(TimeSpan.FromDays(days)));
        }

        #endregion 添加缓存_滑动过期_天

        #region 添加缓存_滑动过期_毫秒

        /// <summary>
        /// 添加缓存_滑动过期_毫秒
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="millisecond">毫秒</param>
        /// <param name="refreshAction">ICacheItemRefreshAction</param>
        public static void AddWithMillisecond(string key, object value, int millisecond, ICacheItemRefreshAction refreshAction)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, refreshAction, new SlidingTime(TimeSpan.FromMilliseconds(millisecond)));
        }

        #endregion 添加缓存_滑动过期_毫秒

        #region 添加缓存_滑动过期_分钟

        /// <summary>
        /// 添加缓存_滑动过期_分钟
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">分钟</param>
        /// <param name="refreshAction">ICacheItemRefreshAction</param>
        public static void AddWithMinutes(string key, object value, int minutes, ICacheItemRefreshAction refreshAction)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, refreshAction, new SlidingTime(TimeSpan.FromMinutes(minutes)));
        }

        #endregion 添加缓存_滑动过期_分钟

        #region 添加缓存_滑动过期_秒

        /// <summary>
        /// 添加缓存_滑动过期_秒
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="seconds">秒</param>
        /// <param name="refreshAction">ICacheItemRefreshAction</param>
        public static void AddWithSeconds(string key, object value, int seconds, ICacheItemRefreshAction refreshAction)
        {
            CacheMgr.Add(key, value, CacheItemPriority.Normal, refreshAction, new SlidingTime(TimeSpan.FromSeconds(seconds)));
        }

        #endregion 添加缓存_滑动过期_秒

        #region 添加缓存_滑动过期_文件依赖

        /// <summary>
        /// 添加缓存_滑动过期_文件依赖
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="refreshAction">ICacheItemRefreshAction</param>
        public static void AddFileDependency(string key, object value, string filePath, ICacheItemRefreshAction refreshAction)
        {
            FileDependency _fileDependency = new FileDependency(filePath);
            CacheMgr.Add(key, value, CacheItemPriority.Normal, refreshAction, _fileDependency);
        }

        #endregion 添加缓存_滑动过期_文件依赖

        #region 清空缓存

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void Flush()
        {
            CacheMgr.Flush();
        }

        #endregion 清空缓存

        #region 移出缓存

        /// <summary>
        /// 移出缓存
        /// </summary>
        /// <param name="key">键</param>
        public static void Remove(string key)
        {
            if (CacheMgr.Contains(key))
                CacheMgr.Remove(key);
        }

        #endregion 移出缓存

        #region 获取缓存

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static object GetData(string key)
        {
            if (CacheMgr.Contains(key))
                return CacheMgr.GetData(key);
            return null;
        }

        #endregion 获取缓存

        #region 获取缓存

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static T GetData<T>(string key)
        {
            if (CacheMgr.Contains(key))
                return (T)CacheMgr.GetData(key);
            return default(T);
        }

        #endregion 获取缓存
    }
}
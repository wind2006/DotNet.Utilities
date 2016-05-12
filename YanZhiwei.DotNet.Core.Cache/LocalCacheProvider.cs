namespace YanZhiwei.DotNet.Core.Cache
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Web;

    using YanZhiwei.DotNet2.Utilities.WebForm.Core;

    /// <summary>
    /// 本地缓存实现
    /// </summary>
    /// 时间：2015-12-31 13:31
    /// 备注：
    public class LocalCacheProvider : ICacheProvider
    {
        #region Methods

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="keyRegex">正则表达式</param>
        /// 时间：2015-12-31 13:32
        /// 备注：
        public virtual void Clear(string keyRegex)
        {
            List<string> _keys = new List<string>();
            IDictionaryEnumerator _enumerator = HttpRuntime.Cache.GetEnumerator();
            while (_enumerator.MoveNext())
            {
                var _key = _enumerator.Key.ToString();
                if (Regex.IsMatch(_key, keyRegex, RegexOptions.IgnoreCase))
                    _keys.Add(_key);
            }

            for (int i = 0; i < _keys.Count; i++)
            {
                HttpRuntime.Cache.Remove(_keys[i]);
            }
        }

        /// <summary>
        /// 以键取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>
        /// 值
        /// </returns>
        public virtual object Get(string key)
        {
            return CacheManger.Get(key);
        }

        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        public virtual T Get<T>(string key)
        {
            return (T)Get(key);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        /// 时间：2015-12-31 13:32
        /// 备注：
        public virtual void Remove(string key)
        {
            CacheManger.Remove(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">分钟</param>
        /// <param name="isAbsoluteExpiration">是否绝对时间</param>
        /// <param name="onRemoveFacotry">委托</param>
        /// 时间：2015-12-31 13:12
        /// 备注：
        public virtual void Set(string key, object value, int minutes, bool isAbsoluteExpiration, Action<string, object, string> onRemoveFacotry)
        {
            CacheManger.Set(key, value, minutes, isAbsoluteExpiration, (k, v, reason) =>
            {
                if (onRemoveFacotry != null)
                    onRemoveFacotry(k, v, reason.ToString());
            });
        }

        #endregion Methods
    }
}
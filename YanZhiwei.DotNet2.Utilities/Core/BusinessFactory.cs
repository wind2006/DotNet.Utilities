using System.Collections;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet2.Utilities.Core
{
    /// <summary>
    /// 业务工厂
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// 时间：2016-01-07 13:43
    /// 备注：
    public class BusinessFactory<T>
        where T : class, new()
    {
        #region Fields

        private static Hashtable businessCache = new Hashtable();
        private static object lockObj = new object();

        #endregion Fields

        #region Properties

        /// <summary>
        /// 实例化
        /// </summary>
        /// 时间：2016-01-07 13:45
        /// 备注：
        public static T Instance
        {
            get
            {
                string _fullName = typeof(T).FullName;
                T _business = (T)businessCache[_fullName];
                if (_business == null)
                {
                    lock (lockObj)
                    {
                        if (_business == null)
                        {
                            _business = ReflectHelper.CreateInstance<T>(typeof(T).FullName, typeof(T).Assembly.FullName);
                            businessCache.Add(typeof(T).FullName, _business);
                        }
                    }
                }
                return _business;
            }
        }

        #endregion Properties
    }
}
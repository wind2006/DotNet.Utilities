namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// BindingSource 帮助类
    /// </summary>
    public static class BindingSourceHelper
    {
        #region Methods

        /// <summary>
        ///  从BindingSource中条件查找
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbSource">BindingSource</param>
        /// <param name="match">委托</param>
        /// <returns>没有查找到则返回NULL</returns>
        public static T Find<T>(this BindingSource dbSource, Predicate<T> match)
            where T : class
        {
            T _finded = null;
            if (dbSource != null)
            {
                foreach (T t in dbSource.List)
                {
                    if (match(t))
                    {
                        _finded = t;
                        break;
                    }
                }
            }
            return _finded;
        }

        /// <summary>
        ///  从BindingSource中条件查找集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbSource">BindingSource</param>
        /// <param name="match">委托</param>
        /// <returns>没有查找到则返回NULL</returns>
        public static IList<T> FindAll<T>(this BindingSource dbSource, Predicate<T> match)
            where T : class
        {
            IList<T> _findedList = null;
            if (dbSource != null)
            {
                _findedList = new List<T>();
                foreach (T t in dbSource.List)
                {
                    if (match(t))
                    {
                        _findedList.Add(t);
                    }
                }
            }
            return _findedList;
        }

        /// <summary>
        /// 获取Control的BindingSource
        /// </summary>
        /// <param name="control">Control</param>
        /// <returns>BindingSource</returns>
        public static BindingSource GetBindingSource(this Control control)
        {
            if (control != null)
            {
                PropertyInfo _finded = control.GetType().GetProperty("DataSource");
                if (_finded != null)
                {
                    object _dbsource = _finded.GetValue(control, null);
                    if (_dbsource != null && _dbsource is BindingSource)
                    {
                        return _dbsource as BindingSource;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 从BindingSource中条件移出
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbSource">BindingSource</param>
        /// <param name="match">委托</param>
        /// <returns>条件移出个数</returns>
        public static int Remove<T>(this BindingSource dbSource, Predicate<T> match)
            where T : class
        {
            int _count = 0;
            if (dbSource != null)
            {
                for (int i = 0; i < dbSource.List.Count; i++)
                {
                    object _cur = dbSource.List[i];
                    if (match((T)_cur))
                    {
                        dbSource.List.Remove(_cur);
                        _count++;
                        i--;
                    }
                }
            }
            return _count;
        }

        #endregion Methods
    }
}
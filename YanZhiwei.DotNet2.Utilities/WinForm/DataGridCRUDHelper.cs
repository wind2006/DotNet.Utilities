namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// DataGrid的添加，删除，修改，查询帮助类
    /// </summary>
    public static class DataGridCRUDHelper
    {
        #region Methods

        /// <summary>
        /// 为DataGridView添加行
        /// 仅仅适用DataGridCRUDHelper类下操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">DataGridView</param>
        /// <param name="model">是否添加成功</param>
        /// <returns>操作是否成功</returns>
        public static bool AddObject<T>(this DataGridView gridView, T model)
            where T : class
        {
            bool _result = true;
            try
            {
                BindList<T> _bindList = gridView.ToBindList<T>();
                if (_bindList == null)
                {
                    _bindList = new BindList<T>();
                    _bindList.Add(model);
                    SetDataSource<T>(gridView, _bindList);
                }
                else
                {
                    _bindList.Add(model);
                }
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        /// <summary>
        ///  为DataGridView添加行
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">DataGridView</param>
        /// <param name="list">集合</param>
        /// <returns>操作是否成功</returns>
        public static bool AddObject<T>(this DataGridView gridView, List<T> list)
        {
            bool _result = true;
            try
            {
                BindList<T> _bindList = gridView.ToBindList<T>();
                if (_bindList == null)
                {
                    _bindList = new BindList<T>(list);
                    SetDataSource<T>(gridView, _bindList);
                }
                else
                {
                    _bindList = gridView.ToBindList<T>();
                    foreach (T t in list)
                    {
                        _bindList.Add(t);
                    }
                }
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        /// <summary>
        /// 判断某列等于某个值是否存在
        /// </summary>
        /// <param name="gridView">DataGridView</param>
        /// <param name="columnName">列名称</param>
        /// <param name="key">列值</param>
        /// <returns>存在返回True;不存在返回FASLE</returns>
        public static bool Exist<T>(this DataGridView gridView, string columnName, Object key)
        {
            bool _result = false;
            BindList<T> _source = gridView.ToBindList<T>();
            if (_source != null)
            {
                int _index = _source.Find(-1, columnName, key);
                _result = _index > 0;
            }
            return _result;
        }

        /// <summary>
        /// 数据筛选
        ///<para>eg:dataGridView1.Filter("Age = 18");</para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">DataGridView</param>
        /// <param name="filter">筛选条件</param>
        public static void Filter<T>(this DataGridView gridView, string filter)
            where T : class
        {
            if (!string.IsNullOrEmpty(filter))
            {
                BindingSource _bindSource = gridView.ToBindingSource();
                if (_bindSource != null)
                {
                    _bindSource.RemoveFilter();
                    _bindSource.Filter = filter;
                }
            }
        }

        /// <summary>
        /// 根据字段名称以及值查找对应的实体类
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">DataGridView</param>
        /// <param name="columnName">列名称</param>
        /// <param name="key">列值</param>
        /// <returns>没有查到返回NULL</returns>
        public static T Find<T>(this DataGridView gridView, string columnName, object key)
            where T : class
        {
            if (!string.IsNullOrEmpty(columnName))
            {
                BindList<T> _source = gridView.ToBindList<T>();
                if (_source != null)
                {
                    int _index = _source.Find(-1, columnName, key);
                    if (_index != -1)
                    {
                        return _source[_index];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 条件查找实体类
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">DataGridView</param>
        /// <param name="match">Predicate</param>
        /// <returns>没有查到返回NULL</returns>
        public static T Find<T>(this DataGridView gridView, Predicate<T> match)
            where T : class
        {
            BindList<T> _source = gridView.ToBindList<T>();
            if (_source != null)
                return _source.Find(match);
            return null;
        }

        /// <summary>
        /// 查找所有合法条件的数据集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">DataGridView</param>
        /// <param name="match">Predicate为何</param>
        /// <returns>返回符合数据;没有合法的返回NULL</returns>
        public static IList<T> FindAll<T>(this DataGridView gridView, Predicate<T> match)
        {
            BindList<T> _source = gridView.ToBindList<T>();
            if (_source != null)
                return _source.FindAll(match);
            return null;
        }

        /// <summary>
        /// 清除DataGridView所有的行
        /// 仅仅适用DataGridCRUDHelper类下操作
        /// </summary>
        /// <param name="gridView">DataGridView</param>
        /// <returns>操作是否成功</returns>
        public static bool RemoveObject<T>(this DataGridView gridView)
        {
            bool _result = true;
            try
            {
                SetDataSource<DataGridView>(gridView, null);
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        /// <summary>
        /// 移出数据项
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">gridView</param>
        /// <param name="t">实体类</param>
        /// <returns>移出是否成功</returns>
        public static bool RemoveObject<T>(this DataGridView gridView, T t)
        {
            bool _result = false;
            try
            {
                BindList<T> _bindList = gridView.ToBindList<T>();
                if (_bindList != null)
                {
                    _result = _bindList.Remove(t);
                }
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        /// <summary>
        /// 移出数据项
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">gridView</param>
        /// <param name="match">条件委托</param>
        /// <returns>移出行数</returns>
        public static int RemoveObject<T>(this DataGridView gridView, Predicate<T> match)
        {
            int _result = 0;
            try
            {
                BindList<T> _bindList = gridView.ToBindList<T>();
                if (_bindList != null)
                {
                    _result = _bindList.Remove(match);
                }
            }
            catch (Exception)
            {
                _result = 0;
            }
            return _result;
        }

        /// <summary>
        /// 将绑定的数据源转换成IList
        /// 仅仅适用DataGridCRUDToolV2Plus类下操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">DataGridView</param>
        /// <returns>IList</returns>
        public static BindList<T> ToBindList<T>(this DataGridView gridView)
        {
            BindList<T> _source = null;
            BindingSource _bindSource = (BindingSource)gridView.DataSource;
            if (_bindSource != null)
                _source = _bindSource.DataSource as BindList<T>;
            return _source;
        }

        /// <summary>
        /// 获取符合条件的数据条数
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="gridView">DataGridView</param>
        /// <param name="match">Predicate委托</param>
        /// <returns>返回符合项目;</returns>
        public static int Where<T>(this DataGridView gridView, Predicate<T> match)
        {
            int _rowsCount = 0;
            IList<T> _finded = gridView.FindAll<T>(match);
            if (_finded != null)
                _rowsCount = _finded.Count;
            return _rowsCount;
        }

        private static void SetDataSource<T>(DataGridView dataGrid, BindList<T> list)
        {
            dataGrid.UIThread<DataGridView>(gv =>
            {
                BindingSource _source = new BindingSource(list, null);
                gv.DataSource = _source;
            });
        }

        private static BindingSource ToBindingSource(this DataGridView gridView)
        {
            BindingSource _bindSource = (BindingSource)gridView.DataSource;
            return _bindSource;
        }

        #endregion Methods
    }
}
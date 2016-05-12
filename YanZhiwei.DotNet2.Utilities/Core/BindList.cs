namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    ///  双向绑定帮助类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class BindList<T> : BindingList<T>, IBindingListView
    {
        #region Fields

        /*
         * 参考：
         * 1. http://blogs.msdn.com/b/winformsue/archive/2007/12/06/filtering-code.aspx
         * 2. http://www.codeproject.com/Articles/19867/How-To-Allow-To-Sort-By-Multiple-Columns-in-Custom
         * 3. http://msdn.microsoft.com/zh-cn/library/ms993236.aspx
         * 4. http://www.codeproject.com/Articles/37671/Databinding-BindingList-BindingSource-and-Busine
         * 5. http://www.dotblogs.com.tw/yc421206/archive/2013/09/03/116162.aspx
         */
        /// <summary>
        /// The comparer list
        /// </summary>
        private readonly Dictionary<string, PropertyComparer<T>> comparerList = new Dictionary<string, PropertyComparer<T>>();

        /// <summary>
        /// The filter compare value
        /// </summary>
        private object filterCompareValue;

        /// <summary>
        /// The filter property name value
        /// </summary>
        private string filterPropertyNameValue;

        /// <summary>
        /// The filter value
        /// </summary>
        private string filterValue = null;

        /// <summary>
        /// The list sort description
        /// </summary>
        private ListSortDescriptionCollection listSortDescription;

        /// <summary>
        /// The property comparer
        /// </summary>
        private List<PropertyComparer<T>> propertyComparer;

        /// <summary>
        /// The property desc
        /// </summary>
        private PropertyDescriptor propertyDesc;

        /// <summary>
        /// The sort direction
        /// </summary>
        private ListSortDirection sortDirection;

        /// <summary>
        /// The unfiltered list value
        /// </summary>
        private List<T> unfilteredListValue = new List<T>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public BindList()
            : base(new List<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindList{T}"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public BindList(IList<T> list)
            : base(list)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindList{T}"/> class.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        public BindList(IEnumerable<T> enumerable)
            : base(new List<T>(enumerable))
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 获取或设置筛选器，以用于从数据源返回的项的集合中排除项。
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Multi-column + filtering is not implemented.
        /// or
        /// Filter is not + in the format: propName = 'value'.
        /// </exception>
        public string Filter
        {
            get
            {
                return filterValue;
            }

            set
            {
                if (filterValue != value)
                {
                    RaiseListChangedEvents = false;
                    if (value == null)
                    {
                        this.ClearItems();
                        foreach (T t in unfilteredListValue)
                        {
                            this.Items.Add(t);
                        }

                        filterValue = value;
                    }
                    else if (Regex.Matches(value, "[?[\\w ]+]? ?[=] ?'?[\\w|/: ]+'?", RegexOptions.Singleline).Count == 1)
                    {
                        unfilteredListValue.Clear();
                        unfilteredListValue.AddRange(this.Items);
                        filterValue = value;
                        GetFilterParts();
                        ApplyFilter();
                    }
                    else if (Regex.Matches(value, "[?[\\w ]+]? ?[=] ?'?[\\w|/: ]+'?", RegexOptions.Singleline).Count > 1)
                    {
                        throw new ArgumentException("Multi-column" + "filtering is not implemented.");
                    }
                    else
                    {
                        throw new ArgumentException("Filter is not" + "in the format: propName = 'value'.");
                    }

                    RaiseListChangedEvents = true;
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
                }
            }
        }

        /// <summary>
        /// FilterCompare
        /// </summary>
        public object FilterCompare
        {
            get { return filterCompareValue; }
        }

        /// <summary>
        /// 属性筛选
        /// </summary>
        public string FilterPropertyName
        {
            get { return filterPropertyNameValue; }
        }

        /// <summary>
        /// 获取当前应用于数据源的排序说明的集合。
        /// </summary>
        public ListSortDescriptionCollection SortDescriptions
        {
            get { return listSortDescription; }
        }

        /// <summary>
        /// 获取一个值，指示数据源是否支持高级排序。
        /// </summary>
        public bool SupportsAdvancedSorting
        {
            get { return true; }
        }

        /// <summary>
        /// 获取一个值，该值指示数据源是否支持筛选。
        /// </summary>
        public bool SupportsFiltering
        {
            get { return true; }
        }

        /// <summary>
        /// 未筛选集合
        /// </summary>
        public List<T> UnfilteredList
        {
            get { return unfilteredListValue; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is sorted core.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is sorted core; otherwise, <c>false</c>.
        /// </value>
        protected override bool IsSortedCore
        {
            get
            {
                return true;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 根据给定的 <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> 对数据源进行排序。
        /// </summary>
        /// <param name="sorts">包含要应用于数据源的顺序的 <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />。</param>
        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            //获取未排序集合
            List<T> _items = this.Items as List<T>;

            //若集合不等于NULL，则应用排序
            if (_items != null)
            {
                listSortDescription = sorts;
                propertyComparer = new List<PropertyComparer<T>>();
                foreach (ListSortDescription sort in sorts)
                {
                    propertyComparer.Add(new PropertyComparer<T>(sort.PropertyDescriptor, sort.SortDirection));
                }

                _items.Sort(CompareValuesByProperties);
            }

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="match">委托</param>
        /// <returns>泛型</returns>
        public T Find(Predicate<T> match)
        {
            T _finded = default(T);

            for (int i = 0; i < Count; i++)
            {
                T t = Items[i];
                if (match(t))
                {
                    _finded = t;
                    break;
                }
            }

            return _finded;
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="startIndex">索引开始数</param>
        /// <param name="property">属性名称</param>
        /// <param name="key">属性值</param>
        /// <returns>查找到符合记录</returns>
        public int Find(int startIndex, string property, object key)
        {
            PropertyDescriptorCollection _properties = TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor _prop = _properties.Find(property, true);
            if (_prop == null)
            {
                return -1;
            }
            else
                if (startIndex <= 0)
                {
                    return FindCore(_prop, key);
                }
                else
                {
                    return FindCore(startIndex, _prop, key);
                }
        }

        /// <summary>
        /// 查找符合记录集合
        /// </summary>
        /// <param name="match">委托</param>
        /// <returns>符合条件的集合</returns>
        public IList<T> FindAll(Predicate<T> match)
        {
            IList<T> _findedList = null;
            if (Count > 0)
            {
                _findedList = new List<T>();
            }

            for (int i = 0; i < Count; i++)
            {
                T t = Items[i];
                if (match(t))
                {
                    _findedList.Add(t);
                }
            }

            return _findedList;
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>成功移除个数</returns>
        public int Remove(Predicate<T> match)
        {
            int _result = 0;
            for (int i = 0; i < Count; i++)
            {
                T t = Items[i];
                if (match(t))
                {
                    RemoveItem(i);
                    i--;
                    _result++;
                }
            }

            return _result;
        }

        /// <summary>
        /// 移除应用于数据源的当前筛选器。
        /// </summary>
        public void RemoveFilter()
        {
            if (Filter != null)
            {
                Filter = null;
            }
        }

        /// <summary>
        /// Applies the sort core.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="sortDirection">The sort direction.</param>
        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection sortDirection)
        {
            List<T> _list = (List<T>)this.Items;
            string _name = property.Name;
            PropertyComparer<T> _comparer;

            if (!this.comparerList.TryGetValue(_name, out _comparer))
            {
                _comparer = new PropertyComparer<T>(property, sortDirection);
                this.comparerList.Add(_name, _comparer);
            }

            _comparer.SetDirection(sortDirection);
            _list.Sort(_comparer);
            this.propertyDesc = property;
            this.sortDirection = sortDirection;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// Finds the core.
        /// </summary>
        /// <param name="prop">The property.</param>
        /// <param name="key">The key.</param>
        /// <returns>查找个数</returns>
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            PropertyInfo _propInfo = typeof(T).GetProperty(prop.Name);
            T _item;
            if (key != null)
            {
                for (int i = 0; i < Count; ++i)
                {
                    _item = (T)Items[i];
                    if (_propInfo.GetValue(_item, null).Equals(key))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        ///  重写FindCore方法
        /// </summary>
        /// <param name="startIndex">开始索引</param>
        /// <param name="prop">PropertyDescriptor</param>
        /// <param name="key">object</param>
        /// <returns>符合条数</returns>
        protected int FindCore(int startIndex, PropertyDescriptor prop, object key)
        {
            // Get the property info for the specified property.
            PropertyInfo propInfo = typeof(T).GetProperty(prop.Name);
            T item;
            if (key != null)
            {
                for (int i = startIndex; i < Count; ++i)
                {
                    item = (T)Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Applies the filter.
        /// </summary>
        private void ApplyFilter()
        {
            unfilteredListValue.Clear();
            unfilteredListValue.AddRange(this.Items);
            List<T> results = new List<T>();
            PropertyDescriptor propDesc = TypeDescriptor.GetProperties(typeof(T))[FilterPropertyName];
            if (propDesc != null)
            {
                int tempResults = -1;
                do
                {
                    tempResults = FindCore(tempResults + 1, propDesc, FilterCompare);
                    if (tempResults != -1)
                    {
                        results.Add(this[tempResults]);
                    }
                }
                while (tempResults != -1);
            }

            this.ClearItems();
            if (results != null && results.Count > 0)
            {
                foreach (T itemFound in results)
                {
                    this.Add(itemFound);
                }
            }
        }

        /// <summary>
        /// Compares the values by properties.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>比较数值</returns>
        private int CompareValuesByProperties(T x, T y)
        {
            if (x == null)
            {
                return (y == null) ? 0 : -1;
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    foreach (PropertyComparer<T> comparer in propertyComparer)
                    {
                        int retval = comparer.Compare(x, y);
                        if (retval != 0)
                        {
                            return retval;
                        }
                    }

                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the filter parts.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Specified filter value  + FilterCompare +  can not be converted from string. + ..Implement a type converter for  + propDesc.PropertyType.ToString()
        /// or
        /// Specified property ' + FilterPropertyName + ' is not found on type  + typeof(T).Name + .
        /// </exception>
        private void GetFilterParts()
        {
            string[] _filterParts = Filter.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            filterPropertyNameValue = _filterParts[0].Replace("[", string.Empty).Replace("]", string.Empty).Trim();
            PropertyDescriptor propDesc = TypeDescriptor.GetProperties(typeof(T))[filterPropertyNameValue.ToString()];
            if (propDesc != null)
            {
                try
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(propDesc.PropertyType);
                    filterCompareValue = converter.ConvertFromString(_filterParts[1].Replace("'", string.Empty).Trim());
                }
                catch (NotSupportedException)
                {
                    throw new ArgumentException("Specified filter value " + FilterCompare + " can not be converted from string." + "..Implement a type converter for " + propDesc.PropertyType.ToString());
                }
            }
            else
            {
                throw new ArgumentException("Specified property '" + FilterPropertyName + "' is not found on type " + typeof(T).Name + ".");
            }
        }

        #endregion Methods
    }
}
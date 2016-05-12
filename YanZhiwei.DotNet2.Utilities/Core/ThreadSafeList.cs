namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// 线程安全集合
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class ThreadSafeList<T> : IList<T>
    {
        #region Fields

        /*
         * 参考：
         * 1. http://www.codeproject.com/KB/cs/safe_enumerable.aspx
         */
        /// <summary>
        /// 集合
        /// </summary>
        private readonly List<T> innerList;

        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object syncRoot = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// ThreadSafeList构造函数
        /// </summary>
        public ThreadSafeList()
        {
            innerList = new List<T>();
        }

        /// <summary>
        /// ThreadSafeList构造函数
        /// </summary>
        /// <param name="data">IEnumerable</param>
        public ThreadSafeList(IEnumerable<T> data)
        {
            innerList = IListHelper.ToList(data);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                lock (syncRoot)
                {
                    return innerList.Count;
                }
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>T</returns>
        public T this[int index]
        {
            get
            {
                lock (syncRoot)
                {
                    return innerList[index];
                }
            }

            set
            {
                lock (syncRoot)
                {
                    innerList[index] = value;
                }
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="item">数据项</param>
        public void Add(T item)
        {
            lock (syncRoot)
            {
                innerList.Add(item);
            }
        }

        /// <summary>
        /// 先判断是否存在集合里面，若存在则移出，然后重新添加
        /// <para>eg:personList.Add(_person, p => p.Age == 19);</para>
        /// </summary>
        /// <param name="t">泛型</param>
        /// <param name="match">委托</param>
        public void Add(T t, Predicate<T> match)
        {
            if (match != null)
            {
                T _finded = Find(match);
                if (_finded != null)
                {
                    Remove(_finded);
                }

                Add(t);
            }
        }

        /// <summary>
        /// 去重复集合添加
        /// </summary>
        /// <param name="items">添加集合</param>
        /// <param name="comparaer">IComparer</param>
        public void AddUniqueTF(IEnumerable<T> items, IComparer<T> comparaer)
        {
            lock (syncRoot)
            {
                innerList.Sort(comparaer);
                foreach (T item in items)
                {
                    int _result = innerList.BinarySearch(item, comparaer);
                    if (_result < 0)
                    {
                        innerList.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// 作为只读
        /// </summary>
        /// <returns>ReadOnlyCollection</returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            lock (syncRoot)
            {
                return new ReadOnlyCollection<T>(this);
            }
        }

        /// <summary>
        /// 移出所有元素
        /// </summary>
        public void Clear()
        {
            lock (syncRoot)
            {
                innerList.Clear();
            }
        }

        /// <summary>
        /// 是否包含某项元素
        /// </summary>
        /// <param name="item">数据项</param>
        /// <returns>是否包含</returns>
        public bool Contains(T item)
        {
            lock (syncRoot)
            {
                return innerList.Contains(item);
            }
        }

        /// <summary>
        /// 复制到某个类型数组
        /// </summary>
        /// <param name="array">复制到苏族</param>
        /// <param name="arrayIndex">开始位置</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (syncRoot)
            {
                innerList.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="match">委托</param>
        /// <returns>是否存在</returns>
        public bool Exists(Predicate<T> match)
        {
            if (match != null)
            {
                lock (syncRoot)
                {
                    foreach (var item in innerList)
                    {
                        if (match(item))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="match">委托</param>
        /// <returns>查找到项</returns>
        public T Find(Predicate<T> match)
        {
            if (match != null)
            {
                lock (syncRoot)
                {
                    return innerList.Find(match);
                }
            }

            return default(T);
        }

        /// <summary>
        /// 查找群不
        /// </summary>
        /// <param name="match">委托</param>
        /// <returns>查找到的集合</returns>
        public List<T> FindAll(Predicate<T> match)
        {
            if (match != null)
            {
                lock (syncRoot)
                {
                    return innerList.FindAll(match);
                }
            }

            return null;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action">委托</param>
        public void ForEach(Action<T> action)
        {
            if (action != null)
            {
                lock (syncRoot)
                {
                    foreach (var item in innerList)
                    {
                        action(item);
                    }
                }
            }
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举数。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            lock (syncRoot)
            {
                return new ThreadSafeEnumerator<T>(innerList.GetEnumerator(), syncRoot);
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。
        /// </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (syncRoot)
            {
                return new ThreadSafeEnumerator<T>(innerList.GetEnumerator(), syncRoot);
            }
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>索引位置</returns>
        public int IndexOf(T item)
        {
            lock (syncRoot)
            {
                return innerList.IndexOf(item);
            }
        }

        /// <summary>
        /// 插入一项
        /// </summary>
        /// <param name="index">插入位置</param>
        /// <param name="item">插入项</param>
        public void Insert(int index, T item)
        {
            lock (syncRoot)
            {
                innerList.Insert(index, item);
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="item">需要移除项</param>
        /// <returns>是否移除成功</returns>
        public bool Remove(T item)
        {
            lock (syncRoot)
            {
                return innerList.Remove(item);
            }
        }

        /// <summary>
        /// RemoveAll
        /// </summary>
        /// <param name="match">Predicate委托</param>
        public void RemoveAll(Predicate<T> match)
        {
            if (match != null)
            {
                lock (syncRoot)
                {
                    innerList.RemoveAll(match);
                }
            }
        }

        /// <summary>
        /// RemoveAt
        /// </summary>
        /// <param name="index">index</param>
        public void RemoveAt(int index)
        {
            lock (syncRoot)
            {
                innerList.RemoveAt(index);
            }
        }

        /// <summary>
        /// Trims the excess.
        /// </summary>
        public void TrimExcess()
        {
            lock (syncRoot)
            {
                innerList.TrimExcess();
            }
        }

        #endregion Methods
    }
}
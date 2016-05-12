using System;
using System.Collections.Generic;
using System.Linq;

namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    /// <summary>
    /// 递归连接
    /// </summary>
    public static class RecursiveJoinHelper
    {
        /// <summary>
        /// Recursives the join.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="parentKeySelector">The parent key selector.</param>
        /// <param name="childKeySelector">The child key selector.</param>
        /// <param name="resultSelector">The result selector.</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, resultSelector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Recursives the join.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="parentKeySelector">The parent key selector.</param>
        /// <param name="childKeySelector">The child key selector.</param>
        /// <param name="resultSelector">The result selector.</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, int, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector,
                (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, index, children));
        }

        /// <summary>
        /// Recursives the join.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="parentKeySelector">The parent key selector.</param>
        /// <param name="childKeySelector">The child key selector.</param>
        /// <param name="resultSelector">The result selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, IEnumerable<TResult>, TResult> resultSelector,
            IComparer<TKey> comparer)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector,
                (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, children), comparer);
        }

        /// <summary>
        /// Recursives the join.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="parentKeySelector">The parent key selector.</param>
        /// <param name="childKeySelector">The child key selector.</param>
        /// <param name="resultSelector">The result selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, int, IEnumerable<TResult>, TResult> resultSelector,
            IComparer<TKey> comparer)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector,
                (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, index, children), comparer);
        }

        /// <summary>
        /// Recursives the join.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="parentKeySelector">The parent key selector.</param>
        /// <param name="childKeySelector">The child key selector.</param>
        /// <param name="resultSelector">The result selector.</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, int, int, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, resultSelector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Recursives the join.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="parentKeySelector">The parent key selector.</param>
        /// <param name="childKeySelector">The child key selector.</param>
        /// <param name="resultSelector">The result selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, int, int, IEnumerable<TResult>, TResult> resultSelector,
            IComparer<TKey> comparer)
        {
            source = new LinkedList<TSource>(source);
            SortedDictionary<TKey, TSource> _parents = new SortedDictionary<TKey, TSource>(comparer);
            SortedDictionary<TKey, LinkedList<TSource>> _children = new SortedDictionary<TKey, LinkedList<TSource>>(comparer);

            foreach (TSource element in source)
            {
                _parents[parentKeySelector(element)] = element;

                LinkedList<TSource> _list;

                TKey _childKey = childKeySelector(element);

                if (!_children.TryGetValue(_childKey, out _list))
                {
                    _children[_childKey] = _list = new LinkedList<TSource>();
                }

                _list.AddLast(element);
            }

            Func<TSource, int, IEnumerable<TResult>> _childSelector = null;

            _childSelector = (TSource parent, int depth) =>
            {
                LinkedList<TSource> _innerChildren = null;

                if (_children.TryGetValue(parentKeySelector(parent), out _innerChildren))
                {
                    return _innerChildren.Select((child, index) => resultSelector(child, index, depth, _childSelector(child, depth + 1)));
                }

                return Enumerable.Empty<TResult>();
            };

            return source.Where(element => !_parents.ContainsKey(childKeySelector(element)))
                .Select((element, index) => resultSelector(element, index, 0, _childSelector(element, 1)));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YanZhiwei.DotNet.Core.Cache;
using YanZhiwei.DotNet.Core.Cache.Models;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.Models;
using YanZhiwei.DotNet4.Utilities.Common;
using YanZhiwei.DotNet4.Utilities.Core;

namespace YanZhiwei.DotNet4.Core.CacheProvider
{
    public static class CacheHelper
    {
        /// <summary>
        /// 将结果转换为缓存的数组，如缓存存在，直接返回，否则从数据源查询，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <returns>查询结果</returns>
        public static TSource[] ToCacheArray<TSource>(this IQueryable<TSource> source)
        {
            string _key = GetKey(source.Expression);
            WrapCacheConfigItem _cacheConfig = CacheConfigContext.GetCurrentWrapCacheConfigItem(_key);
            TSource[] result = _cacheConfig.CacheProvider.Get<TSource[]>(_key);
            if (result != null)
            {
                return result;
            }
            result = source.ToArray();
            _cacheConfig.CacheProvider.Set(_key, result, _cacheConfig.CacheConfigItem.Minitus, _cacheConfig.CacheConfigItem.IsAbsoluteExpiration, null);
            return result;
        }

        /// <summary>
        /// 将结果转换为缓存的列表，如缓存存在，直接返回，否则从数据源查询，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <param name="source">查询数据源</param>
        /// <returns>查询结果</returns>
        public static List<TSource> ToCacheList<TSource>(this IQueryable<TSource> source)
        {
            string _key = GetKey(source.Expression);
            WrapCacheConfigItem _cacheConfig = CacheConfigContext.GetCurrentWrapCacheConfigItem(_key);
            List<TSource> _result = _cacheConfig.CacheProvider.Get<List<TSource>>(_key);
            if (_result != null)
            {
                return _result;
            }
            _result = source.ToList();
            _cacheConfig.CacheProvider.Set(_key, _result, _cacheConfig.CacheConfigItem.Minitus, _cacheConfig.CacheConfigItem.IsAbsoluteExpiration, null);
            return _result;
        }

        /// <summary>
        /// 查询分页数据结果，如缓存存在，直接返回，否则从数据源查找分页结果，并存入缓存中再返回
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>查询的分页结果</returns>
        public static PageResult<TResult> ToPageCache<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector)
        {
            string key = GetKey(source, predicate, pageCondition, selector);
            WrapCacheConfigItem _cacheConfig = CacheConfigContext.GetCurrentWrapCacheConfigItem(key);
            PageResult<TResult> result = _cacheConfig.CacheProvider.Get<PageResult<TResult>>(key);
            if (result != null)
            {
                return result;
            }
            result = source.ToPage(predicate, pageCondition, selector);
            _cacheConfig.CacheProvider.Set(key, result, _cacheConfig.CacheConfigItem.Minitus, _cacheConfig.CacheConfigItem.IsAbsoluteExpiration, null);
            return result;
        }

        private static string GetKey<TEntity, TResult>(IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector)
        {
            if ((pageCondition.SortConditions == null || pageCondition.SortConditions.Length == 0) && string.IsNullOrWhiteSpace(pageCondition.PrimaryKeyField))
            {
                throw new ArgumentException("排序条件集合为空的话，请设置主键字段数值！");
            }

            source = source.Where(predicate);
            SortCondition[] _sortConditions = pageCondition.SortConditions;
            if (_sortConditions == null || _sortConditions.Length == 0)
            {
                source = source.OrderBy(pageCondition.PrimaryKeyField);
            }
            else
            {
                int _count = 0;
                IOrderedQueryable<TEntity> orderSource = null;
                foreach (SortCondition sortCondition in _sortConditions)
                {
                    orderSource = _count == 0
                        ? CollectionPropertySorter<TEntity>.OrderBy(source, sortCondition.SortField, sortCondition.ListSortDirection)
                        : CollectionPropertySorter<TEntity>.ThenBy(orderSource, sortCondition.SortField, sortCondition.ListSortDirection);
                    _count++;
                }
                source = orderSource;
            }
            int _pageIndex = pageCondition.PageIndex, pageSize = pageCondition.PageSize;
            source = source != null
                ? source.Skip((_pageIndex - 1) * pageSize).Take(pageSize)
                : Enumerable.Empty<TEntity>().AsQueryable();
            IQueryable<TResult> _query = source.Select(selector);
            return GetKey(_query.Expression);
        }

        private static string GetKey(Expression expression)
        {
            return expression.ToString().ToMD5String();
        }
    }
}
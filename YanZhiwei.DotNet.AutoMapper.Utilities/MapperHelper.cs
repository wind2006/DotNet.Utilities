namespace YanZhiwei.DotNet.AutoMapper.Utilities
{
    using global::AutoMapper;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// AutoMapper扩展帮助类
    /// </summary>
    /// 时间：2015-12-07 15:49
    /// 备注：
    public static class MapperHelper
    {
        #region Methods

        /// <summary>
        /// DataTable 映射
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> GetEntitys<T>(this DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
                return null;
            IDataReader _reader = table.CreateDataReader();
            Mapper.Reset();
            Mapper.CreateMap<IDataReader, IEnumerable<T>>();
            return Mapper.Map<IDataReader, IEnumerable<T>>(_reader);
        }

        /// <summary>
        /// DataReader映射
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="reader">IDataReader</param>
        /// <returns>IEnumerable</returns>
        /// 时间：2015-12-07 15:45
        /// 备注：
        public static IEnumerable<T> GetEntitys<T>(this IDataReader reader)
        {
            Mapper.Reset();
            Mapper.CreateMap<IDataReader, IEnumerable<T>>();
            return Mapper.Map<IDataReader, IEnumerable<T>>(reader);
        }

        /// <summary>
        /// 类型映射
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">映射类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="destination">The destination.</param>
        /// <returns>IEnumerable</returns>
        /// 时间：2015-12-07 15:45
        /// 备注：
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) return destination;
            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map(source, destination);
        }

        #endregion Methods
    }
}
namespace YanZhiwei.DotNet4.Framework.Data
{
    using DotNet.Framework.Contract;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// 仓储接口
    /// </summary>
    /// 时间：2016-01-06 17:09
    /// 备注：
    public interface IDataRepository
    {
        #region Methods

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// 时间：2016-01-13 13:31
        /// 备注：
        void Delete<T>(T entity)
            where T : ModelBase;

        /// <summary>
        /// 查找
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="keyValues">删除依据的键</param>
        /// <returns>实体类</returns>
        /// 时间：2016-01-13 13:32
        /// 备注：
        T Find<T>(params object[] keyValues)
            where T : ModelBase;

        /// <summary>
        /// 查找全部
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="conditions">委托.</param>
        /// <returns>集合</returns>
        /// 时间：2016-01-13 13:32
        /// 备注：
        List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null)
            where T : ModelBase;

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <typeparam name="S">泛型</typeparam>
        /// <param name="conditions">查找条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">分页集合</param>
        /// <returns>PagedList</returns>
        /// 时间：2016-01-13 13:33
        /// 备注：
        PagedList<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex)
            where T : ModelBase;

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>实体类</returns>
        /// 时间：2016-01-13 13:31
        /// 备注：
        T Insert<T>(T entity)
            where T : ModelBase;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>实体类</returns>
        /// 时间：2016-01-13 13:31
        /// 备注：
        T Update<T>(T entity)
            where T : ModelBase;

        #endregion Methods
    }
}
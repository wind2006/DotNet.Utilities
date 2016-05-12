using System.Collections.Generic;
using System.Data.Entity;
using YanZhiwei.DotNet.Framework.Contract;

namespace YanZhiwei.DotNet4.Framework.Data
{
    /// <summary>
    /// IDbContext接口
    /// </summary>
    /// 时间：2016-01-29 13:26
    /// 备注：
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : ModelBase;

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>影响行数</returns>
        int SaveChanges();

        /// <summary>
        /// 执行存储过程，并返回对象列表
        /// </summary>
        /// <typeparam name="TEntity">泛型</typeparam>
        /// <param name="commandText">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>集合</returns>
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : ModelBase, new();

        /// <summary>
        /// 查询Sql语句
        /// </summary>
        /// <typeparam name="TEntity">泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>集合</returns>
        IEnumerable<TEntity> SqlQuery<TEntity>(string sql, params object[] parameters);

        /// <summary>
        /// 执行sql 是否启用事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="doNotEnsureTransaction">是否启用事物</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null,
            params object[] parameters);
    }
}
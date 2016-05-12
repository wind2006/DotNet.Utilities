using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using YanZhiwei.DotNet.Framework.Contract;

namespace YanZhiwei.DotNet4.Framework.Data
{
    /// <summary>
    /// DAL基类，通用泛型数据访问模式
    /// </summary>
    /// 时间：2016-01-29 14:07
    /// 备注：
    public class EfContextBase : DbContext, IDbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// 时间：2016-01-29 13:58
        /// 备注：
        public EfContextBase(string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// 隐藏基类Set方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>IDbSet</returns>
        /// 时间：2016-01-29 11:07
        /// 备注：
        public new IDbSet<T> Set<T>()
            where T : ModelBase
        {
            return base.Set<T>();
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">Sql书</param>
        /// <param name="doNotEnsureTransaction">是否启用事务，默认不启用</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        /// 时间：2016-01-29 11:11
        /// 备注：
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? _previousTimeout = null;
            if (timeout.HasValue)
            {
                _previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            TransactionalBehavior _transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            int _result = this.Database.ExecuteSqlCommand(_transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = _previousTimeout;
            }
            return _result;
        }

        /// <summary>
        /// 执行存储过程返回集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="commandText">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>集合</returns>
        /// 时间：2016-01-29 11:08
        /// 备注：
        /// <exception cref="System.ArgumentException">不支持的参数类型！</exception>
        public IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters)
            where T : ModelBase, new()
        {
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new ArgumentException("不支持的参数类型！");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        commandText += " output";
                    }
                }
            }

            List<T> _result = this.Database.SqlQuery<T>(commandText, parameters).ToList();

            //performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
            bool _acd = this.Configuration.AutoDetectChangesEnabled;
            try
            {
                this.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < _result.Count; i++)
                    _result[i] = AttachEntityToContext(_result[i]);
            }
            finally
            {
                this.Configuration.AutoDetectChangesEnabled = _acd;
            }
            return _result;
        }

        /// <summary>
        /// Sql语句查询
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>IEnumerable</returns>
        /// 时间：2016-01-29 11:09
        /// 备注：
        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<T>(sql, parameters);
        }

        protected virtual T AttachEntityToContext<T>(T entity)
            where T : ModelBase, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var alreadyAttached = Set<T>().Local.FirstOrDefault(x => x.ID == entity.ID);
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<T>().Attach(entity);
                return entity;
            }

            //entity is already loaded
            return alreadyAttached;
        }
    }
}
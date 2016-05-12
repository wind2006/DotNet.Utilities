namespace YanZhiwei.DotNet4.Framework.Data
{
    using DotNet.Framework.Contract;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using YanZhiwei.DotNet2.Utilities.Core;
    using YanZhiwei.DotNet3._5.Utilities.Common;

    /// <summary>
    /// DAL基类，实现Repository通用泛型数据访问模式
    /// </summary>
    public class DbContextBase : DbContext, IDataRepository, IDisposable
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// 时间：2016-01-14 10:57
        /// 备注：
        public DbContextBase(string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="auditLogger">IAuditable</param>
        /// 时间：2016-01-14 10:58
        /// 备注：
        public DbContextBase(string connectionString, IAuditable auditLogger)
            : this(connectionString)
        {
            this.AuditLogger = auditLogger;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 日志接口
        /// </summary>
        public IAuditable AuditLogger
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        public void Delete<T>(T entity)
            where T : ModelBase
        {
            this.Entry<T>(entity).State = EntityState.Deleted;
            this.SaveChanges();
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
                //store previous timeout
                _previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            TransactionalBehavior _transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            int _result = this.Database.ExecuteSqlCommand(_transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = _previousTimeout;
            }

            //return result
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
        /// 查找
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="keyValues">查询依据的键</param>
        /// <returns>
        /// 实体类
        /// </returns>
        public T Find<T>(params object[] keyValues)
            where T : ModelBase
        {
            return this.Set<T>().Find(keyValues);
        }

        /// <summary>
        /// 查找全部
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="conditions">委托.</param>
        /// <returns>
        /// 集合
        /// </returns>
        public List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null)
            where T : ModelBase
        {
            if (conditions == null)
                return this.Set<T>().ToList();
            else
                return this.Set<T>().Where(conditions).ToList();
        }

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
        public PagedList<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex)
            where T : ModelBase
        {
            var queryList = conditions == null ? this.Set<T>() : this.Set<T>().Where(conditions) as IQueryable<T>;

            return queryList.OrderByDescending(orderBy).ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>
        /// 实体类
        /// </returns>
        public T Insert<T>(T entity)
            where T : ModelBase
        {
            this.Set<T>().Add(entity);
            this.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 将在此上下文中所做的所有更改保存到基础数据库。
        /// </summary>
        /// <returns>
        /// 已写入基础数据库的对象的数目。
        /// </returns>
        /// 时间：2016-01-14 10:59
        /// 备注：
        public override int SaveChanges()
        {
            this.WriteAuditLog();

            var result = base.SaveChanges();
            return result;
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>
        /// 实体类
        /// </returns>
        public T Update<T>(T entity)
            where T : ModelBase
        {
            var set = this.Set<T>();
            set.Attach(entity);
            this.Entry<T>(entity).State = EntityState.Modified;
            this.SaveChanges();

            return entity;
        }

        /// <summary>
        /// 日志拦截写入
        /// </summary>
        /// 时间：2016-01-29 11:07
        /// 备注：
        internal void WriteAuditLog()
        {
            if (this.AuditLogger == null)
                return;

            foreach (var dbEntry in this.ChangeTracker.Entries<ModelBase>().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                AuditableAttribute _auditableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(AuditableAttribute), false).SingleOrDefault() as AuditableAttribute;
                if (_auditableAttr == null)
                    continue;

                string _operaterName = ServiceCallContext.Current.Operater.Name;

                Task.Factory.StartNew(() =>
                {
                    TableAttribute _tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
                    string _tableName = _tableAttr != null ? _tableAttr.Name : dbEntry.Entity.GetType().Name;
                    string _moduleName = dbEntry.Entity.GetType().FullName.Split('.').Skip(1).FirstOrDefault();

                    this.AuditLogger.WriteLog(dbEntry.Entity.ID, _operaterName, _moduleName, _tableName, dbEntry.State.ToString(), dbEntry.Entity);
                });
            }
        }

        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity)
            where TEntity : ModelBase, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.ID == entity.ID);
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }

            //entity is already loaded
            return alreadyAttached;
        }

        #endregion Methods
    }
}
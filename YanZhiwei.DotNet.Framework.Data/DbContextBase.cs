namespace YanZhiwei.DotNet.Framework.Data
{
    using PetaPoco;
    using System;
    using System.Data.Common;
    using System.Linq;
    using System.Threading;
    using YanZhiwei.DotNet.Framework.Contract;

    /// <summary>
    ///  DAL基类，通用数据访问模式
    /// </summary>
    /// 时间：2016-03-30 11:15
    /// 备注：
    public class DbContextBase : Database, IDisposable
    {
        #region Constructors

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dbProviderFactory">数据源提供实例</param>
        /// <param name="auditLogger">数据添加，删除，修改拦截接口</param>
        /// 时间：2016-03-28 10:13
        /// 备注：
        public DbContextBase(string connectionString, DbProviderFactory dbProviderFactory, ISqlAuditable auditLogger)
            : base(connectionString, dbProviderFactory)
        {
            this.AuditLogger = auditLogger;
            this.DataChangedEvent += DbContextBase_DataChangedEvent; ;
        }

        /// <summary>
        /// 初始化构造函数，默认Sql Server数据，不数据拦截
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// 时间：2016-03-28 10:14
        /// 备注：
        public DbContextBase(string connectionString)
            : this(connectionString, DbProviderFactories.GetFactory("System.Data.SqlClient"), null)
        {
        }

        /// <summary>
        /// 初始化构造函数，默认Sql Server数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="auditLogger">数据添加，删除，修改拦截接口</param>
        /// 时间：2016-03-28 10:15
        /// 备注：
        public DbContextBase(string connectionString, ISqlAuditable auditLogger)
            : this(connectionString, DbProviderFactories.GetFactory("System.Data.SqlClient"), auditLogger)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 数据拦截接口
        /// </summary>
        public ISqlAuditable AuditLogger
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        private void DbContextBase_DataChangedEvent(Type type, string sql)
        {
            if (type != null)
            {
                Type _entityType = type;
                AuditableAttribute _auditableAttr = _entityType.GetCustomAttributes(typeof(AuditableAttribute), false).SingleOrDefault() as AuditableAttribute;
                if (_auditableAttr == null) return;
                int _userId = ServiceCallContext.Current.Operater.UserId;
                Thread _task = new Thread(() =>
                {
                    var _tableAttr = _entityType.GetCustomAttributes(typeof(TableNameAttribute), true);
                    string _tableName = _tableAttr.Length == 0 ? _entityType.Name : (_tableAttr[0] as TableNameAttribute).Value;

                    this.AuditLogger.WriteLog(_userId, _tableName, sql, DateTime.UtcNow);
                });
                _task.Start();
            }
        }

        #endregion Methods
    }
}
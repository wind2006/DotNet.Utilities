namespace YanZhiwei.DotNet4.Framework.Data
{
    using DotNet.Framework.Contract;
    using DotNet2.Utilities.Common;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// EF存储仓实现
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// 时间：2016-01-28 14:18
    /// 备注：
    public class EfRepository<T> : IRepository<T>
        where T : ModelBase
    {
        #region Fields

        private readonly IDbContext database;
        private IDbSet<T> dbSet;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">IDbContext</param>
        /// 时间：2016-01-29 13:43
        /// 备注：
        public EfRepository(IDbContext context)
        {
            ValidateHelper.Begin().NotNull(context, "context");
            database = context;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 获取实体类没有跟踪
        /// </summary>
        /// <value>IQueryable</value>
        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        /// <summary>
        /// 获取实体类没有跟踪
        /// <para>
        /// AsNoTracking是无跟踪查询, 有时我们的实体只需要显示，无需更新，所以为了提高性能，我们不需要实体被EF追踪。此时可以使用AsNoTracking的查询来得到实体，这样实体的状态是Detached状态。这样可以提高性能，但是如果取到数据后，要对数据做修改并保存，则无法反映到数据库里。另外如果对通过AsNoTracking，得到的数据做删除处理，还会报错。
        /// </para>
        /// </summary>
        /// <value>
        /// IQueryable
        /// </value>
        public IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// 获取IDbSet
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (dbSet == null)
                {
                    dbSet = database.Set<T>();
                }
                return dbSet ?? (dbSet = database.Set<T>());
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体类</param>
        /// 时间：2016-01-29 13:42
        /// 备注：
        public virtual void Delete(T entity)
        {
            try
            {
                ValidateHelper.Begin().NotNull(entity, "entity");

                Entities.Remove(entity);

                database.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                GetException(dbEx);
            }
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="entities">集合</param>
        /// 时间：2016-01-29 13:42
        /// 备注：
        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                ValidateHelper.Begin().NotNull(entities, "entities");
                foreach (var entity in entities)
                    Entities.Remove(entity);
                database.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                GetException(dbEx);
            }
        }

        /// <summary>
        /// 根据主键查找
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        /// 时间：2016-01-29 13:41
        /// 备注：
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">实体类</param>
        /// 时间：2016-01-29 13:41
        /// 备注：
        public void Insert(T entity)
        {
            try
            {
                ValidateHelper.Begin().NotNull(entity, "entities");
                Entities.Add(entity);
                database.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                GetException(dbEx);
            }
        }

        /// <summary>
        /// 插入集合
        /// </summary>
        /// <param name="entities">集合/param>
        /// 时间：2016-01-29 13:41
        /// 备注：
        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                ValidateHelper.Begin().NotNull(entities, "entities");

                foreach (var entity in entities)
                    Entities.Add(entity);

                database.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                GetException(dbEx);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体类</param>
        /// 时间：2016-01-29 13:40
        /// 备注：
        public virtual void Update(T entity)
        {
            try
            {
                ValidateHelper.Begin().NotNull(entity, "entity");
                database.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                GetException(dbEx);
            }
        }

        /// <summary>
        /// 重新构建异常对象
        /// </summary>
        /// <param name="dbEx">DbEntityValidationException</param>
        /// 时间：2016-01-29 13:37
        /// 备注：
        /// <exception cref="System.Exception">DbEntityValidationException</exception>
        private static void GetException(DbEntityValidationException dbEx)
        {
            StringBuilder _builder = new StringBuilder();

            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    _builder.AppendFormat("属性: {0} 错误: {1}{2}", validationError.PropertyName, validationError.ErrorMessage, Environment.NewLine);
                }
            }
            throw new Exception(_builder.ToString(), dbEx);
        }

        #endregion Methods
    }
}
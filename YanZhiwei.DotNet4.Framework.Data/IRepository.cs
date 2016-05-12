namespace YanZhiwei.DotNet4.Framework.Data
{
    using DotNet.Framework.Contract;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// 时间：2016-01-28 14:06
    /// 备注：
    public interface IRepository<T>
        where T : ModelBase
    {
        #region Properties

        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <value>IQueryable</value>
        IQueryable<T> Table
        {
            get;
        }

        /// <summary>
        /// 获取实体类没有跟踪
        /// <para>
        /// AsNoTracking是无跟踪查询, 有时我们的实体只需要显示，无需更新，所以为了提高性能，我们不需要实体被EF追踪。此时可以使用AsNoTracking的查询来得到实体，这样实体的状态是Detached状态。这样可以提高性能，但是如果取到数据后，要对数据做修改并保存，则无法反映到数据库里。另外如果对通过AsNoTracking，得到的数据做删除处理，还会报错。
        /// </para>
        /// </summary>
        /// <value>IQueryable</value>
        IQueryable<T> TableNoTracking
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体类</param>
        void Delete(T entity);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>泛型</returns>
        /// 时间：2016-01-28 14:05
        /// 备注：
        T GetById(object id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体类</param>
        void Insert(T entity);

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities">集合</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体类</param>
        void Update(T entity);

        #endregion Methods
    }
}
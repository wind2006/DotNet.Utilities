using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YanZhiwei.DotNet.EF.Utilities.Core
{
    public static class EntityBaseQueries
    {
        public static TEntity GetById<TEntity>(this IQueryable<TEntity> set, int id) where TEntity : EntityBase
        {
            return set.SingleOrDefault(x => x.Id == id);
        }

        public static IQueryable<TEntity> GetByIds<TEntity>(this IQueryable<TEntity> set, List<TEntity> ids) where TEntity : EntityBase
        {
            return set.Where(x => ids.Contains(x));
        }

        //public static TEntity Remove<TEntity>(this IDbSet<TEntity> set, int id) where TEntity : EntityBase
        //{
        //    var entity = set.GetById(id);
        //    DBEntities.Current.Remove(entity);
        //    return entity;
        //}

        //public static IQueryable<TEntity> OrderByRecentlyCreated<TEntity>(this IDbSet<TEntity> set) where TEntity : EntityBase
        //{
        //    return set.OrderByDescending(x => x.CreatedDate);
        //}
    }
}

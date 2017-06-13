using BCoreDal.Contracts;
using BCoreDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BCoreDal.SqlServer
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private DbContext _db;

        public Repository(DbContext context)
        {
            _db = context;
        }

        public async Task<Guid> CreateAsync(T item)
        {
            var entity = item as Entity;

            if (entity == null)
                throw new Exception(String.Format("{0} is not Entity", item.GetType()));

            _db.Set<T>().Add(item);

            await _db.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<int> DeleteAsync(T item)
        {
            _db.Set<T>().Remove(item);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            T item = default(T);
            item = Activator.CreateInstance<T>();
            item.Id = id;

            _db.Set<T>().Attach(item);
            _db.Set<T>().Remove(item);

            return await _db.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> q = _db.Set<T>();
            foreach (var include in includes)
            {
                q = q.Include(include);
            }

            return await q.Where(where).SingleOrDefaultAsync();
        }

        public async Task<T> GetAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> q = _db.Set<T>();
            foreach (var include in includes)
            {
                q = q.Include(include);
            }

            return await q.Where(f => f.Id == id).SingleOrDefaultAsync();
        }

        public async Task<object> GetValueAsync(Guid id, Expression<Func<T, object>> selector)
        {
            IQueryable<T> q = _db.Set<T>();

            return await q.Where(f => f.Id == id).Select(selector).SingleOrDefaultAsync();
        }
        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> where = null
            , params Expression<Func<T, object>>[] includes)
        {
            return await _getAllQuery(where: where, includes: includes).ToListAsync();
        }


        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> where = null
            , int? skip = default(int?)
            , int? take = default(int?)
            , params Expression<Func<T, object>>[] includes)
        {
            return await _getAllQuery(where: where, skip: skip, take: take, includes: includes).ToListAsync();
        }

        public async Task<ICollection<T>> GetAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy = null,
            SortOrder sort = SortOrder.Unspecified,
            Expression<Func<T, bool>> where = null,
            int? skip = default(int?),
            int? take = default(int?),
            params Expression<Func<T, object>>[] includes)
        {
            return await _getAllQuery<TOrderKey>(orderBy, sort, where, skip, take, includes).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> where = null
            , int? skip = default(int?)
            , int? take = default(int?))
        {
            ICollection<T> items = await _getAllQuery(where: where, skip: skip, take: take).ToListAsync();

            return items.Count();
        }

        public async Task<int> CountAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy = null,
            SortOrder sort = SortOrder.Unspecified,
            Expression<Func<T, bool>> where = null,
            int? skip = default(int?),
            int? take = default(int?))

        {
            ICollection<T> items = await _getAllQuery<TOrderKey>(orderBy, sort, where, skip, take).ToListAsync();

            return items.Count();
        }

        public async Task<int> UpdateAsync(T item)
        {
            _db.Entry(item).State = EntityState.Modified;

            return await _db.SaveChangesAsync();
        }
        
        private IQueryable<T> _getAllQuery(IQueryable<T> items = null,
            Expression<Func<T, bool>> where = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> res = items ?? _db.Set<T>();

            if (where != null)
                res = res.Where(where);

            if (skip != null)
                res = res.Skip((int)skip);

            if (take != null)
                res = res.Take((int)take);

            foreach (var include in includes)
            {
                res = res.Include(include);
            }

            return res;
        }

        private IQueryable<T> _getAllQuery<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy = null,
            SortOrder sort = SortOrder.Unspecified,
            Expression<Func<T, bool>> where = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> items = _db.Set<T>();

            if (orderBy != null)
                items = sort == SortOrder.Descending ? items.OrderByDescending(orderBy) : items.OrderBy(orderBy);

            return _getAllQuery(items, where, skip, take, includes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BCoreDal.Contracts
{
    public interface IRepository<T>
    {
        Task<Guid> CreateAsync(T item);
        Task<int> DeleteAsync(T item);
        Task<int> DeleteAsync(Guid id);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> where = null
            , int? skip = default(int?)
            , int? take = default(int?)
            , params Expression<Func<T, object>>[] includes);

        Task<ICollection<T>> GetAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy = null,
            SortOrder sort = SortOrder.Unspecified,
            Expression<Func<T, bool>> where = null,
            int? skip = default(int?),
            int? take = default(int?),
            params Expression<Func<T, object>>[] includes);

        Task<int> CountAsync(Expression<Func<T, bool>> where = null
            , int? skip = default(int?)
            , int? take = default(int?));

        Task<int> CountAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy = null,
            SortOrder sort = SortOrder.Unspecified,
            Expression<Func<T, bool>> where = null,
            int? skip = default(int?),
            int? take = default(int?));

        Task<T> GetAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task<int> UpdateAsync(T item);        
        Task<object> GetValueAsync(Guid id, Expression<Func<T, object>> selector);
    }
}

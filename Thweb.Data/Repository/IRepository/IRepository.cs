using System.Linq.Expressions;
using Thweb.Model.Model.Pager;

namespace Thweb.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        Task<PagedList<T>> GetPagedListAsync<U>(int page, int pageSize, Expression<Func<T, bool>>? filter = null, Expression<Func<T, U>>? orderBy = null, bool descending = false, string? includeProperties = null);
        

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
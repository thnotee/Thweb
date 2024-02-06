using System.Linq.Expressions;
using Thweb.Model.Model.Pager;

namespace Thweb.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        /*
         
        Entity Framework Core(EF Core)에서 AddAsync 메소드가 존재하는 이유는 데이터베이스에 새로운 데이터를 추가하는 작업이 I/O 작업이기 때문입니다. 
        이는 비동기적으로 처리될 수 있어야 하며, 따라서 AddAsync 메소드가 필요합니다.
        그러나 Update나 Remove와 같은 메소드들은 실제로 I/O 작업을 수행하지 않습니다. 이들은 단지 추적 상태(tracking state)를 변경하며, 이는 메모리 내에서 일어나는 작업이므로 비동기적으로 처리될 필요가 없습니다.
        실제 I/O 작업은 SaveChanges 또는 SaveChangesAsync 메소드를 호출할 때 일어나게 됩니다.
        따라서 UpdateAsync나 RemoveAsync 메소드가 없는 것은 EF Core의 설계 철학과 일치하며, 
        이는 비동기 처리가 필요한 I/O 작업과 그렇지 않은 메모리 내 작업을 명확하게 구분하기 위함입니다.
         */

        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        Task<PagedList<T>> GetPagedListAsync<U>(int page, int pageSize, Expression<Func<T, bool>>? filter = null, Expression<Func<T, U>>? orderBy = null, bool descending = false, string? includeProperties = null);
        

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
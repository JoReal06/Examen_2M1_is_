using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Examen_2M1_is_.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsycn(T entity);
        Task<List<T>> GetAllAsyn();
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task<T> GetByIdAsyn(int id);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(Expression<Func<T, bool>>? filter = null);



    }
}

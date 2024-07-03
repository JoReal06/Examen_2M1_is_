using Examen_2M1_is_.DAta;
using Examen_2M1_is_.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Examen_2M1_is_.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HotelContext  _context;
        private readonly DbSet<T> _dbSet;

        public Repository(HotelContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task CreateAsycn(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                await Console.Out.WriteLineAsync(ex.InnerException?.Message);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.AnyAsync();
        }

        public async Task<List<T>> GetAllAsyn()
        {

            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsyn(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

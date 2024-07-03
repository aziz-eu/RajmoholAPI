using Microsoft.EntityFrameworkCore;
using Rajmohol.Data;
using Rajmohol.Repository.IRepository;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Rajmohol.Repository
{
    public class Repository<T> : IRepository <T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
           await dbSet.AddAsync(entity);
           await SaveAsync();
           

        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) query = dbSet.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked) query = query.AsNoTracking();
            if (filter!= null) query = dbSet.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }
    }
}

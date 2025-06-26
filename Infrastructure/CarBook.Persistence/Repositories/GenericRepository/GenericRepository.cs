using CarBook.Application.RepositoryInterfaces.GenericRepository;
using CarBook.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CarBookContext _context;

        public GenericRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CancellationToken cancellationToken, T entity)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            CancellationToken cancellationToken, 
            Expression<Func<T, bool>>? filter = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T> GetAsync(
            CancellationToken cancellationToken, 
            Expression<Func<T, bool>> filter, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _context.Set<T>().Where(filter);
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(CancellationToken cancellationToken, int id)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

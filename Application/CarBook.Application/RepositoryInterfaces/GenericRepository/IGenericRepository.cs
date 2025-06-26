using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.RepositoryInterfaces.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            CancellationToken cancellationToken, 
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T> GetAsync(
            CancellationToken cancellationToken,
            Expression<Func<T, bool>> filter, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T> GetByIdAsync(CancellationToken cancellationToken, int id);
        Task CreateAsync(CancellationToken cancellationToken, T entity);
        Task UpdateAsync(CancellationToken cancellationToken, T entity);
        Task RemoveAsync(CancellationToken cancellationToken, T entity);
    }
}

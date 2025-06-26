using CarBook.Application.RepositoryInterfaces.GenericRepository;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.RepositoryInterfaces
{
    public interface ICommentRepository: IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByBlogIdAsync(int blogId, CancellationToken cancellationToken);
        Task<int> GetCommentsCountByBlogIdAsync(int blogId, CancellationToken cancellationToken);
    }
}

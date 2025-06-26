using CarBook.Application.RepositoryInterfaces;
using CarBook.Domain.Entities;
using CarBook.Persistence.Context;
using CarBook.Persistence.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Repositories
{
    public class CommentRepository: GenericRepository<Comment>, ICommentRepository
    {
        private readonly CarBookContext _context;
        public CommentRepository(CarBookContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByBlogIdAsync(int blogId, CancellationToken cancellationToken)
        {
            IEnumerable<Comment> comments = await _context.Comments.Where(comment => comment.BlogId ==blogId).ToListAsync(cancellationToken);
            return comments;
        }

        public async Task<int> GetCommentsCountByBlogIdAsync(int blogId, CancellationToken cancellationToken)
        {
            return await _context.Comments.Where(comment => comment.BlogId == blogId).CountAsync(cancellationToken);
        }
    }
}

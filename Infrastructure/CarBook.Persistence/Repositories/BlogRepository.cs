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
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly CarBookContext _context;

        public BlogRepository(CarBookContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetLatest3BlogsWithAuthor(CancellationToken cancellationToken)
        {
            IEnumerable<Blog> blogs = await _context.Blogs.Include(blog => blog.Author).OrderByDescending(blog => blog.BlogId).Take(3).ToListAsync(cancellationToken);
            return blogs;
        }
    }   
}

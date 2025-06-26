using CarBook.Application.RepositoryInterfaces;
using CarBook.Domain.Entities;
using CarBook.Persistence.Context;
using CarBook.Persistence.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Repositories
{
    public class TagCloudRepository : GenericRepository<TagCloud>, ITagCloudRepository
    {
        public TagCloudRepository(CarBookContext context): base(context)
        {
        }
    }
}

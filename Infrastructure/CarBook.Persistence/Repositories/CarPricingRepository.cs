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
    public class CarPricingRepository : GenericRepository<CarPricing>, ICarPricingRepository
    {
        public CarPricingRepository(CarBookContext context) : base(context)
        {
        }
    }
}

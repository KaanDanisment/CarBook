using CarBook.Application.Dtos.CarDtos;
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
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        private readonly CarBookContext _context;

        public CarRepository(CarBookContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AvailableCarByLocationDto>> GetAvailableCarsByLocation(CancellationToken cancellationToken, int locationId)
        {
            IEnumerable<AvailableCarByLocationDto> cars = await _context.Cars
                .Where(car => car.LocationId == locationId && car.Available)
                .Select(c => new AvailableCarByLocationDto
                {
                    CarId = c.CarId,
                    Brand = c.Brand.Name,
                    Model = c.Model,
                    Transmission = c.Transmission,
                    CoverImageUrl = c.CoverImageUrl,
                    BigImageUrl = c.BigImageUrl,
                    HourlyPrice = c.CarPricings.Where(cp => cp.PricingId == 2).Select(cp => cp.Amount).FirstOrDefault(),
                    DailyPrice = c.CarPricings.Where(cp => cp.PricingId == 3).Select(cp => cp.Amount).FirstOrDefault(),
                    MonthlyPrice = c.CarPricings.Where(cp => cp.PricingId == 4).Select(cp => cp.Amount).FirstOrDefault()
                }).ToListAsync(cancellationToken);
            return cars;
        }

        public async Task<IEnumerable<Car>> GetLatest5Cars(CancellationToken cancellationToken)
        {
            IEnumerable<Car> cars = await _context.Cars
                .Include(car => car.Brand)
                .Include(car => car.CarPricings)
                    .ThenInclude(carPrcing => carPrcing.Pricing)
                .OrderByDescending(car => car.CarId)
                .Take(5)
                .ToListAsync(cancellationToken);
            return cars;
        }
    }
}

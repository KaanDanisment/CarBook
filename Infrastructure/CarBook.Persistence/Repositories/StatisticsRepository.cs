using CarBook.Application.Dtos.StatisticsDtos;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly CarBookContext _context;

        public StatisticsRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task<CarCountDto> GetCarsCount(CancellationToken cancellationToken)
        {
            return new CarCountDto
            {
                CarCount = await _context.Cars.CountAsync(cancellationToken)
            };
        }

        public async Task<AuthorCountDto> GetAuthorCount(CancellationToken cancellationToken)
        {
            return new AuthorCountDto
            {
                AuthorCount = await _context.Authors.CountAsync(cancellationToken)
            };
        }

        public async Task<AutomaticTransmissionCarsCountDto> GetAutomaticTransmissionCarsCount(CancellationToken cancellationToken)
        {
            int value = await _context.Cars.CountAsync(car => car.Transmission == "Automatic", cancellationToken);

            return new AutomaticTransmissionCarsCountDto { AutomaticTransmissionCarsCount = value };
        }

        public async Task<AvgDailyRentalPriceDto> GetAvgDailyRentalPrice(CancellationToken cancellationToken)
        {
            var dailPricingId = await _context.Pricings
                .Where(p => p.Name == "Günlük")
                .Select(p => p.PricingId)
                .FirstOrDefaultAsync(cancellationToken);

            decimal value = await _context.CarPricings
                .Where(cp => cp.PricingId == dailPricingId)
                .Select(cp => (decimal?)cp.Amount)
                .AverageAsync(cancellationToken) ?? 0;

            return new AvgDailyRentalPriceDto { AvgDailyRentalPrice = value };
        }

        public async Task<AvgMonthlyRentalPriceDto> GetAvgMonthlyRentalPrice(CancellationToken cancellationToken)
        {
            var monthlyPricingId = await _context.Pricings
                .Where(p => p.Name == "Aylık")
                .Select(p => p.PricingId)
                .FirstOrDefaultAsync(cancellationToken);

            decimal value = await _context.CarPricings
                    .Where(cp => cp.PricingId == monthlyPricingId)
                    .Select(cp => (decimal?)cp.Amount)
                    .AverageAsync(cancellationToken) ?? 0;

            return new AvgMonthlyRentalPriceDto { AvgMonthlyRentalPrice = value };
        }

        public async Task<AvgWeeklyRentalPriceDto> GetAvgWeeklyRentalPrice(CancellationToken cancellationToken)
        {
            var weeklyPricingId = await _context.Pricings
                .Where(p => p.Name == "Haftalık")
                .Select(p => p.PricingId)
                .FirstOrDefaultAsync(cancellationToken);

            decimal value = await _context.CarPricings
                    .Where(cp => cp.PricingId == weeklyPricingId)
                    .Select(cp => (decimal?)cp.Amount)
                    .AverageAsync(cancellationToken) ?? 0;

            return new AvgWeeklyRentalPriceDto { AvgWeeklyRentalPrice = value };
        }

        public async Task<BlogCountDto> GetBlogCount(CancellationToken cancellationToken)
        {
            return new BlogCountDto { BlogCount = await _context.Blogs.CountAsync(cancellationToken) };
        }

        public async Task<BrandCountDto> GetBrandCount(CancellationToken cancellationToken)
        {
            return new BrandCountDto { BrandCount = await _context.Blogs.CountAsync(cancellationToken) };
        }

        public async Task<CarsCountWithLessThan1000KmDto> GetCarsCountWithLessThan1000Km(CancellationToken cancellationToken)
        {
            return new CarsCountWithLessThan1000KmDto
            {
                CarsCountWithLessThan1000Km = await _context.Cars.CountAsync(car => car.Km < 1000, cancellationToken)
            };
        }

        public async Task<ElectricCarsCountDto> GetElectricCarsCount(CancellationToken cancellationToken)
        {
            return new ElectricCarsCountDto
            {
                ElectricCarsCount = await _context.Cars.CountAsync(car => car.Fuel == "Electric", cancellationToken)
            };
        }

        public async Task<GasolineOrDieselCarsCountDto> GetGasolineOrDieselCarsCount(CancellationToken cancellationToken)
        {
            return new GasolineOrDieselCarsCountDto
            {
                GasolineOrDieselCarsCount = await _context.Cars.CountAsync(car => car.Fuel == "Gasoline" || car.Fuel == "Diesel", cancellationToken)
            };
        }

        public async Task<LocationCountDto> GetLocationCount(CancellationToken cancellationToken)
        {
            return new LocationCountDto { LocationCount = await _context.Locations.CountAsync(cancellationToken) };
        }

        public async Task<TheBlogWithTheMostCommentsDto> GetTheBlogWithTheMostComments(CancellationToken cancellationToken)
        {
            string? blogTitle = await _context.Blogs
                .Include(blog => blog.Comments)
                .OrderByDescending(blog => blog.Comments.Count)
                .Select(blog => blog.Title)
                .FirstOrDefaultAsync(cancellationToken);

            return blogTitle != null
                ? new TheBlogWithTheMostCommentsDto { BlogTitle = blogTitle }
                : new TheBlogWithTheMostCommentsDto { BlogTitle = "Yorum bulunamadı" };
        }

        public async Task<TheBrandWithMostCarsDto> GetTheBrandWithMostCars(CancellationToken cancellationToken)
        {
            var brandCounts = await _context.Cars
                .GroupBy(c => c.Brand.Name)
                .Select(g => new
                {
                    BrandName = g.Key,
                    CarCount = g.Count()
                })
                .OrderByDescending(g => g.CarCount)
                .FirstOrDefaultAsync(cancellationToken);

            return brandCounts != null
                ? new TheBrandWithMostCarsDto { BrandName = brandCounts.BrandName, CarsCount = brandCounts.CarCount }
                : new TheBrandWithMostCarsDto { BrandName = "Araç bilgisi bulunamadı", CarsCount = 0 };
        }

        public async Task<TheHighestPricedCarForDailyRentalDto?> GetTheHighestPricedCarForDailyRental(CancellationToken cancellationToken)
        {
            TheHighestPricedCarForDailyRentalDto highestPricedCar = new TheHighestPricedCarForDailyRentalDto();

            var value = await _context.CarPricings
                .Where(cp => cp.Pricing.Name == "Günlük")
                .OrderByDescending(cp => cp.Amount)
                .Select(cp => new
                {
                    BrandName = cp.Car.Brand.Name,
                    ModelName = cp.Car.Model,
                    Amount = cp.Amount
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (value != null)
            {
                highestPricedCar.BrandName = value.BrandName;
                highestPricedCar.CarModel = value.ModelName;
                highestPricedCar.Amount = value.Amount;
            }

            return highestPricedCar;
        }

        public async Task<TheLowestPricedCarForDailyRentalDto?> GetTheLowestPricedCarForDailyRental(CancellationToken cancellationToken)
        {
            TheLowestPricedCarForDailyRentalDto lowestPricedCar = new TheLowestPricedCarForDailyRentalDto();

            var value = await _context.CarPricings
                .Where(cp => cp.Pricing.Name == "Günlük")
                .OrderBy(cp => cp.Amount)
                .Select(cp => new
                {
                    BrandName = cp.Car.Brand.Name,
                    ModelName = cp.Car.Model,
                    Amount = cp.Amount
                })
                .FirstOrDefaultAsync();

            if (value != null)
            {
                lowestPricedCar.BrandName = value.BrandName;
                lowestPricedCar.CarModel = value.ModelName;
                lowestPricedCar.Amount = value.Amount;
            }

            return lowestPricedCar;
        }
    }
}

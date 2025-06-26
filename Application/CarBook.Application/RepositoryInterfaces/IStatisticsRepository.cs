using CarBook.Application.Dtos.StatisticsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.RepositoryInterfaces
{
    public interface IStatisticsRepository
    {
        Task<CarCountDto> GetCarsCount(CancellationToken cancellationToken);
        Task<LocationCountDto> GetLocationCount(CancellationToken cancellationToken);
        Task<AuthorCountDto> GetAuthorCount(CancellationToken cancellationToken);
        Task<BlogCountDto> GetBlogCount(CancellationToken cancellationToken);
        Task<BrandCountDto> GetBrandCount(CancellationToken cancellationToken);
        Task<AvgDailyRentalPriceDto> GetAvgDailyRentalPrice(CancellationToken cancellationToken);
        Task<AvgWeeklyRentalPriceDto> GetAvgWeeklyRentalPrice(CancellationToken cancellationToken);
        Task<AvgMonthlyRentalPriceDto> GetAvgMonthlyRentalPrice(CancellationToken cancellationToken);
        Task<AutomaticTransmissionCarsCountDto> GetAutomaticTransmissionCarsCount(CancellationToken cancellationToken);
        Task<TheBrandWithMostCarsDto> GetTheBrandWithMostCars(CancellationToken cancellationToken);
        Task<TheBlogWithTheMostCommentsDto> GetTheBlogWithTheMostComments(CancellationToken cancellationToken);
        Task<CarsCountWithLessThan1000KmDto> GetCarsCountWithLessThan1000Km(CancellationToken cancellationToken);
        Task<GasolineOrDieselCarsCountDto> GetGasolineOrDieselCarsCount(CancellationToken cancellationToken);
        Task<ElectricCarsCountDto> GetElectricCarsCount(CancellationToken cancellationToken);
        Task<TheHighestPricedCarForDailyRentalDto> GetTheHighestPricedCarForDailyRental(CancellationToken cancellationToken);
        Task<TheLowestPricedCarForDailyRentalDto> GetTheLowestPricedCarForDailyRental(CancellationToken cancellationToken);
    }
}

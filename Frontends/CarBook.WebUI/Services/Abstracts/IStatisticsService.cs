using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IStatisticsService
    {
        Task<IDataResult<AuthorCountDto>> GetAuthorCount();
        Task<IDataResult<AutomaticTransmissionCarsCountDto>> GetAutomaticTransmissionCarsCount();
        Task<IDataResult<AvgDailyRentalPriceDto>> GetAvgDailyRentalPrice();
        Task<IDataResult<AvgMonthlyRentalPriceDto>> GetAvgMonthlyRentalPrice();
        Task<IDataResult<AvgWeeklyRentalPriceDto>> GetAvgWeeklyRentalPrice();
        Task<IDataResult<BlogCountDto>> GetBlogCount();
        Task<IDataResult<BrandCountDto>> GetBrandCount();
        Task<IDataResult<CarCountDto>> GetCarCount();
        Task<IDataResult<CarsCountWithLessThan1000KmDto>> GetCarsCountWithLessThan1000Km();
        Task<IDataResult<ElectricCarsCountDto>> GetElectricCarsCount();
        Task<IDataResult<GasolineOrDieselCarsCountDto>> GetGasolineOrDieselCarsCount();
        Task<IDataResult<LocationCountDto>> GetLocationCount();
        Task<IDataResult<TheBlogWithTheMostCommentsDto>> GetTheBlogWithTheMostComments();
        Task<IDataResult<TheBrandWithMostCarsDto>> GetTheBrandWithMostCars();
        Task<IDataResult<TheHighestPricedCarForDailyRentalDto>> GetTheHighestPricedCarForDailyRental();
        Task<IDataResult<TheLowestPricedCarForDailyRentalDto>> GetTheLowestPricedCarForDailyRental();
    }
}

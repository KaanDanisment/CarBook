using CarBook.Dto;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class StatisticsViewModel
    {
        public AuthorCountDto? AuthorCount { get; set; }
        public string? AuthorCountErroMessage { get; set; }

        public AutomaticTransmissionCarsCountDto? AutomaticTransmissionCarsCount { get; set; }
        public string? AutomaticTransmissionCarsCountErroMessage { get; set; }

        public AvgDailyRentalPriceDto? AvgDailyRentalPrice { get; set; }
        public string? AvgDailyRentalPriceErroMessage { get; set; }

        public AvgMonthlyRentalPriceDto? AvgMonthlyRentalPrice { get; set; }
        public string? AvgMonthlyRentalPriceErrorMessage { get; set; }

        public AvgWeeklyRentalPriceDto? AvgWeeklyRentalPrice { get; set; }
        public string? AvgWeeklyRentalPriceErrorMessage { get; set; }

        public BlogCountDto? BlogCount { get; set; }
        public string? BlogCountErrorMessage { get; set; }

        public BrandCountDto? BrandCount { get; set; }
        public string? BrandCountErrorMessage { get; set; }

        public CarCountDto? CarCount { get; set; }
        public string? CarCountErrroMessage { get; set; }

        public CarsCountWithLessThan1000KmDto? CarsCountWithLessThan1000Km { get; set; }
        public string? CarsCountWithLessThan1000KmErrorMessage { get; set; }


        public ElectricCarsCountDto? ElectricCarsCount { get; set; }
        public string? ElectricCarsCountErrorMessage { get; set; }

        public GasolineOrDieselCarsCountDto? GasolineOrDieselCarsCount { get; set; }
        public string? GasolineOrDieselCarsCountErrorMessage { get; set; }

        public LocationCountDto? LocationCount { get; set; }
        public string? LocationCountErrorMessage { get; set; }

        public TheBlogWithTheMostCommentsDto? TheBlogWithTheMostComments { get; set; }
        public string? TheBlogWithTheMostCommentsErrorMessage { get; set; }

        public TheBrandWithMostCarsDto? TheBrandWithMostCars { get; set; }
        public string? TheBrandWithMostCarsErrorMessage { get; set; }

        public TheHighestPricedCarForDailyRentalDto? TheHighestPricedCarForDailyRental { get; set; }
        public string TheHighestPricedCarForDailyRentalErrorMessage { get; set; }

        public TheLowestPricedCarForDailyRentalDto TheLowestPricedCarForDailyRental { get; set; }
        public string TheLowestPricedCarForDailyRentalErrorMessage { get; set; }
    }
}

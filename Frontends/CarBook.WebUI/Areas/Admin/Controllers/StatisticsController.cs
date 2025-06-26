using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new StatisticsViewModel();

            var authorCount = await _statisticsService.GetAuthorCount();
            if (!authorCount.Success)
            {
                viewModel.AuthorCountErroMessage = authorCount.Message;
            }
            viewModel.AuthorCount = authorCount.Data;

            var automaticTransmissionCarsCount = await _statisticsService.GetAutomaticTransmissionCarsCount();
            if (!automaticTransmissionCarsCount.Success)
            {
                viewModel.AutomaticTransmissionCarsCountErroMessage = automaticTransmissionCarsCount.Message;
            }
            viewModel.AutomaticTransmissionCarsCount = automaticTransmissionCarsCount.Data;

            var avgDailyRentalPrice = await _statisticsService.GetAvgDailyRentalPrice();
            if (!avgDailyRentalPrice.Success)
            {
                viewModel.AvgDailyRentalPriceErroMessage = avgDailyRentalPrice.Message;
            }
            viewModel.AvgDailyRentalPrice = avgDailyRentalPrice.Data;

            var avgMonthlyRentalPrice = await _statisticsService.GetAvgMonthlyRentalPrice();
            if (!avgMonthlyRentalPrice.Success)
            {
                viewModel.AvgMonthlyRentalPriceErrorMessage = avgMonthlyRentalPrice.Message;
            }
            viewModel.AvgMonthlyRentalPrice = avgMonthlyRentalPrice.Data;

            var avgWeeklyRentalPrice = await _statisticsService.GetAvgWeeklyRentalPrice();
            if (!avgWeeklyRentalPrice.Success)
            {
                viewModel.AvgWeeklyRentalPriceErrorMessage = avgWeeklyRentalPrice.Message;
            }
            viewModel.AvgWeeklyRentalPrice = avgWeeklyRentalPrice.Data;

            var blogCount = await _statisticsService.GetBlogCount();
            if (!blogCount.Success)
            {
                viewModel.BlogCountErrorMessage = blogCount.Message;
            }
            viewModel.BlogCount = blogCount.Data;

            var brandCount = await _statisticsService.GetBrandCount();
            if (!brandCount.Success)
            {
                viewModel.BrandCountErrorMessage = brandCount.Message;
            }
            viewModel.BrandCount = brandCount.Data;

            var carCount = await _statisticsService.GetCarCount();
            if (!carCount.Success)
            {
                viewModel.CarCountErrroMessage = carCount.Message;
            }
            viewModel.CarCount = carCount.Data;

            var carsCountWithLessThan1000Km = await _statisticsService.GetCarsCountWithLessThan1000Km();
            if (!carsCountWithLessThan1000Km.Success)
            {
                viewModel.CarsCountWithLessThan1000KmErrorMessage = carsCountWithLessThan1000Km.Message;
            }
            viewModel.CarsCountWithLessThan1000Km = carsCountWithLessThan1000Km.Data;

            var electricCarsCount = await _statisticsService.GetElectricCarsCount();
            if (!electricCarsCount.Success)
            {
                viewModel.ElectricCarsCountErrorMessage = electricCarsCount.Message;
            }
            viewModel.ElectricCarsCount = electricCarsCount.Data;

            var gasolineOrDieselCarsCount = await _statisticsService.GetGasolineOrDieselCarsCount();
            if (!gasolineOrDieselCarsCount.Success)
            {
                viewModel.GasolineOrDieselCarsCountErrorMessage = gasolineOrDieselCarsCount.Message;
            }
            viewModel.GasolineOrDieselCarsCount = gasolineOrDieselCarsCount.Data;

            var locationCount = await _statisticsService.GetLocationCount();
            if (!locationCount.Success)
            {
                viewModel.LocationCountErrorMessage = locationCount.Message;
            }
            viewModel.LocationCount = locationCount.Data;

            var theBlogWithTheMostComments = await _statisticsService.GetTheBlogWithTheMostComments();
            if(!theBlogWithTheMostComments.Success)
            {
                viewModel.TheBlogWithTheMostCommentsErrorMessage = theBlogWithTheMostComments.Message;
            }
            viewModel.TheBlogWithTheMostComments = theBlogWithTheMostComments.Data;

            var theBrandWithMostCars = await _statisticsService.GetTheBrandWithMostCars();
            if (!theBrandWithMostCars.Success)
            {
                viewModel.TheBrandWithMostCarsErrorMessage = theBrandWithMostCars.Message;
            }
            viewModel.TheBrandWithMostCars = theBrandWithMostCars.Data;

            var theHighestPricedCarForDailyRental = await _statisticsService.GetTheHighestPricedCarForDailyRental();
            if (!theHighestPricedCarForDailyRental.Success)
            {
                viewModel.TheHighestPricedCarForDailyRentalErrorMessage = theHighestPricedCarForDailyRental.Message;
            }
            viewModel.TheHighestPricedCarForDailyRental = theHighestPricedCarForDailyRental.Data;

            var theLowestPricedCarForDailyRental = await _statisticsService.GetTheLowestPricedCarForDailyRental();
            if (!theLowestPricedCarForDailyRental.Success)
            {
                viewModel.TheLowestPricedCarForDailyRentalErrorMessage = theLowestPricedCarForDailyRental.Message;
            }
            viewModel.TheLowestPricedCarForDailyRental = theLowestPricedCarForDailyRental.Data;

            return View(viewModel);
        }
    }
}

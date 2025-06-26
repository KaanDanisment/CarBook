using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.RentalDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class RentalService : IRentalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<RentalService> _logger;

        public RentalService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<RentalService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public void CalculateTotalPrice(RentalCarsViewModel createRentalViewModel)
        {
            string rentalPeriod = RentalPeriod(createRentalViewModel.RentalToCreate);
            if (rentalPeriod == "Hourly")
            {
                TimeSpan timeSpan = createRentalViewModel.RentalToCreate.ReturnTime.ToTimeSpan() - createRentalViewModel.RentalToCreate.RentTime.ToTimeSpan();
                decimal hours = (decimal)timeSpan.TotalMinutes / 60;
                foreach (var car in createRentalViewModel.AvailableRentalCars)
                {
                    car.CalculatedPrice = car.HourlyPrice * hours;
                }
            }
            else
            {
                int days = (createRentalViewModel.RentalToCreate.ReturnDate.DayNumber - createRentalViewModel.RentalToCreate.RentDate.DayNumber);
                foreach (var car in createRentalViewModel.AvailableRentalCars)
                {
                    car.CalculatedPrice = car.DailyPrice * days;
                }
            }
        }

        private string RentalPeriod(CreateRentalDto createRentalDto)
        {
            var rentalPeriod = createRentalDto.ReturnDate.DayNumber - createRentalDto.RentDate.DayNumber;

            if (rentalPeriod == 0)
            {
                return "Hourly";
            }
            else
            {
                return "Daily";
            }
        }

        public async Task<IResult> CreateRental(CreateRentalDto createRentalDto)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Api/Rentals", createRentalDto);
                var response = await _apiResponseHandler.HandleApiResponse(result);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateRental");
                return new ErrorResult("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}

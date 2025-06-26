using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto;
using CarBook.WebUI.Services.Abstracts;

namespace CarBook.WebUI.Services.Concrete
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<StatisticsService> _logger;

        public StatisticsService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<StatisticsService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IDataResult<AuthorCountDto>> GetAuthorCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetAuthorsCount");
                var result = await _apiResponseHandler.HandleApiResponse<AuthorCountDto>(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAuthorCount");
                return new ErrorDataResult<AuthorCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<AutomaticTransmissionCarsCountDto>> GetAutomaticTransmissionCarsCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetAutomaticTransmissionCarsCount");
                var result = await _apiResponseHandler.HandleApiResponse<AutomaticTransmissionCarsCountDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAutomaticTransmissionCarsCount");
                return new ErrorDataResult<AutomaticTransmissionCarsCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }


        public async Task<IDataResult<AvgDailyRentalPriceDto>> GetAvgDailyRentalPrice()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetAvgDailyRentalPrice");
                var result = await _apiResponseHandler.HandleApiResponse<AvgDailyRentalPriceDto>(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAvgDailyRentalPrice");
                return new ErrorDataResult<AvgDailyRentalPriceDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<AvgMonthlyRentalPriceDto>> GetAvgMonthlyRentalPrice()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetAvgMonthlyRentalPrice");
                var result = await _apiResponseHandler.HandleApiResponse<AvgMonthlyRentalPriceDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAvgMonthlyRentalPrice");
                return new ErrorDataResult<AvgMonthlyRentalPriceDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<AvgWeeklyRentalPriceDto>> GetAvgWeeklyRentalPrice()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetAvgWeeklyRentalPrice");
                var result = await _apiResponseHandler.HandleApiResponse<AvgWeeklyRentalPriceDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAvgWeeklyRentalPrice");
                return new ErrorDataResult<AvgWeeklyRentalPriceDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<BlogCountDto>> GetBlogCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetBlogsCount");
                var result = await _apiResponseHandler.HandleApiResponse<BlogCountDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetBlogCount");
                return new ErrorDataResult<BlogCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<BrandCountDto>> GetBrandCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetBrandsCount");
                var result = await _apiResponseHandler.HandleApiResponse<BrandCountDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetBrandCount");
                return new ErrorDataResult<BrandCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<CarCountDto>> GetCarCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetCarsCount");
                var result = await _apiResponseHandler.HandleApiResponse<CarCountDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetCarCount");
                return new ErrorDataResult<CarCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<CarsCountWithLessThan1000KmDto>> GetCarsCountWithLessThan1000Km()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetCarsCountWithLessThan1000Km");
                var result = await _apiResponseHandler.HandleApiResponse<CarsCountWithLessThan1000KmDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetCarsCountWithLessThan1000Km");
                return new ErrorDataResult<CarsCountWithLessThan1000KmDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<ElectricCarsCountDto>> GetElectricCarsCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetElectricCarsCount");
                var result = await _apiResponseHandler.HandleApiResponse<ElectricCarsCountDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetElectricCarsCount");
                return new ErrorDataResult<ElectricCarsCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<GasolineOrDieselCarsCountDto>> GetGasolineOrDieselCarsCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetGasolineOrDieselCarsCount");
                var result = await _apiResponseHandler.HandleApiResponse<GasolineOrDieselCarsCountDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetGasolineOrDieselCarsCount");
                return new ErrorDataResult<GasolineOrDieselCarsCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<LocationCountDto>> GetLocationCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetLocationsCount");
                var result = await _apiResponseHandler.HandleApiResponse<LocationCountDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetLocationCount");
                return new ErrorDataResult<LocationCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<TheBlogWithTheMostCommentsDto>> GetTheBlogWithTheMostComments()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetTheBlogWithTheMostComments");
                var result = await _apiResponseHandler.HandleApiResponse<TheBlogWithTheMostCommentsDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetTheBlogWithTheMostComments");
                return new ErrorDataResult<TheBlogWithTheMostCommentsDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<TheBrandWithMostCarsDto>> GetTheBrandWithMostCars()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetTheBrandWithMostCars");
                var result = await _apiResponseHandler.HandleApiResponse<TheBrandWithMostCarsDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetTheBrandWithMostCars");
                return new ErrorDataResult<TheBrandWithMostCarsDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<TheHighestPricedCarForDailyRentalDto>> GetTheHighestPricedCarForDailyRental()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetTheHighestPricedCarForDailyRental");
                var result = await _apiResponseHandler.HandleApiResponse<TheHighestPricedCarForDailyRentalDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetTheHighestPricedCarForDailyRental");
                return new ErrorDataResult<TheHighestPricedCarForDailyRentalDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<TheLowestPricedCarForDailyRentalDto>> GetTheLowestPricedCarForDailyRental()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Statistics/GetTheLowestPricedCarForDailyRental");
                var result = await _apiResponseHandler.HandleApiResponse<TheLowestPricedCarForDailyRentalDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetTheLowestPricedCarForDailyRental");
                return new ErrorDataResult<TheLowestPricedCarForDailyRentalDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}

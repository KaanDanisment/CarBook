using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.CarDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class CarService : ICarService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<CarService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public CarService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<CarService> logger, IApiResponseHandler apiResponseHandler)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IDataResult<IEnumerable<CarDto>>> GetAllCars()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Cars");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<CarDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllCars");
                return new ErrorDataResult<IEnumerable<CarDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<CarDto>>> GetLatest5Cars()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Cars/GetLatest5Cars");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<CarDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetLatest5Cars");
                return new ErrorDataResult<IEnumerable<CarDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> CreateCar(CreateCarDto createCarDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Cars", createCarDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateCar");
                return new ErrorResult("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteCar(int carId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Cars/{carId}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteCar");
                return new ErrorResult("Araç silinirken bir hata oluştu, daha sonra tekrar deneyin", "InternalServerError");
            }
        }
        public async Task<IDataResult<CarDto>> GetCarById(int carId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Cars/{carId}");
                var result = await _apiResponseHandler.HandleApiResponse<CarDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetCarById");
                return new ErrorDataResult<CarDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
        public async Task<IResult> UpdateCar(CarDto carToUpdate)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Cars", carToUpdate);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: UpdateCar");
                return new ErrorResult("Araç güncellenirken bir hata oluştu, daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<AvailableRentalCarDto>>> GetAvailableRentalCars(int locationId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Api/Cars/GetAvailableCarsByLocation/{locationId}");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<AvailableRentalCarDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAvailableRentalCars");
                return new ErrorDataResult<IEnumerable<AvailableRentalCarDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<CarToRentalDto>> GetCarToRental(int carId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Api/Cars/GetCarToRentByCarId/{carId}");
                var result = await _apiResponseHandler.HandleApiResponse<CarToRentalDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetCarToRental");
                return new ErrorDataResult<CarToRentalDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}

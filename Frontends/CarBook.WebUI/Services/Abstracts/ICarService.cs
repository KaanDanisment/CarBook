using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.CarDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface ICarService
    {
        Task<IDataResult<IEnumerable<CarDto>>> GetAllCars();
        Task<IDataResult<IEnumerable<CarDto>>> GetLatest5Cars();
        Task<IResult> CreateCar(CreateCarDto createCarDto);
        Task<IResult> DeleteCar(int id);
        Task<IDataResult<CarDto>> GetCarById(int id);
        Task<IResult> UpdateCar(CarDto carToUpdate);
        Task<IDataResult<IEnumerable<AvailableRentalCarDto>>> GetAvailableRentalCars(int locationId);
        Task<IDataResult<CarToRentalDto>> GetCarToRental(int carId);
    }
}

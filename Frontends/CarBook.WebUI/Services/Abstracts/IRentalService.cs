using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.CarDtos;
using CarBook.Dto.RentalDtos;
using CarBook.WebUI.Models;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IRentalService
    {
        void CalculateTotalPrice(RentalCarsViewModel createRentalViewModel);
        Task<IResult> CreateRental(CreateRentalDto createRentalDto);
    }
}

using CarBook.Application.Services.Abstract;
using Hangfire;

namespace CarBook.WebApi.Hangfire.Managers
{
    public class CarAvailabilityJobManager
    {
        private readonly ICarService _carService;

        public CarAvailabilityJobManager(ICarService carService)
        {
            _carService = carService;
        }

        public async Task Process()
        {
            await _carService.UpdateCarAvailability();
        }
    }
}

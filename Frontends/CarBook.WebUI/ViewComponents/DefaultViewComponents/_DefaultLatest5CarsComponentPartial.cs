using CarBook.Dto.CarDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultLatest5CarsComponentPartial : ViewComponent
    {
        private readonly ICarService _carService;

        public _DefaultLatest5CarsComponentPartial(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new CarViewModel();
            var result = await _carService.GetLatest5Cars();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            viewModel.Cars = result.Data;
            return View(viewModel);
        }
    }
}

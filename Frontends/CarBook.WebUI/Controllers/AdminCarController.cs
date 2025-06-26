    using CarBook.Domain.Entities;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarBook.WebUI.Controllers
{
    public class AdminCarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBrandService _brandService;

        public AdminCarController(ICarService carService, IBrandService brandService)
        {
            _carService = carService;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new CarViewModel();
            var cars = await _carService.GetAllCars();
            if (!cars.Success)
            {
                viewModel.ErrorMessage = cars.Message;
                return View(viewModel);
            }
            viewModel.Cars = cars.Data;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            var viewModel = new CreateCarViewModel();
            var brands = await _brandService.GetAllBrands();
            if (!brands.Success)
            {
                viewModel.ErrorMessage = brands.Message;
                return View(viewModel);
            }
            viewModel.Brands = brands.Data.Select(brand => new SelectListItem
            {
                Value = brand.BrandId.ToString(),
                Text = brand.Name
            }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarViewModel createCarViewModel)
        {
            var result = await _carService.CreateCar(createCarViewModel.CarToCreate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                var brands = await _brandService.GetAllBrands();
                if (!brands.Success)
                {
                    createCarViewModel.ErrorMessage = brands.Message;
                    return View(createCarViewModel);
                }
                createCarViewModel.Brands = brands.Data.Select(brand => new SelectListItem
                {
                    Value = brand.BrandId.ToString(),
                    Text = brand.Name
                }).ToList();
                return View(createCarViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
            var result = await _carService.DeleteCar(id);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                var viewModel = new CarViewModel();
                var cars = await _carService.GetAllCars();
                if (!cars.Success)
                {
                    viewModel.ErrorMessage = cars.Message;
                    return View("Index",viewModel);
                }
                viewModel.Cars = cars.Data;
                return View("Index", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var viewModel = new UpdateCarViewModel();

            var car = await _carService.GetCarById(id);
            if (!car.Success)
            {
                viewModel.ErrorMessage = car.Message;
                return View(viewModel);
            }
            viewModel.CarToUpdate = car.Data;

            var brands = await _brandService.GetAllBrands();
            if (!brands.Success)
            {
                viewModel.ErrorMessage = brands.Message;
                return View(viewModel);
            }
            viewModel.Brands = brands.Data.Select(brand => new SelectListItem
            {
                Value = brand.BrandId.ToString(),
                Text = brand.Name
            }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarViewModel updateCarViewModel)
        {
            var result = await _carService.UpdateCar(updateCarViewModel.CarToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                var brands = await _brandService.GetAllBrands();
                if (!brands.Success)
                {
                    updateCarViewModel.ErrorMessage = brands.Message;
                    return View(updateCarViewModel);
                }
                updateCarViewModel.Brands = brands.Data.Select(brand => new SelectListItem
                {
                    Value = brand.BrandId.ToString(),
                    Text = brand.Name
                }).ToList();

                var car = await _carService.GetCarById(updateCarViewModel.CarToUpdate.CarId);
                if (!car.Success)
                {
                    updateCarViewModel.ErrorMessage = car.Message;
                    return View(updateCarViewModel);
                }
                updateCarViewModel.CarToUpdate = car.Data;
                return View(updateCarViewModel);
            }
            return RedirectToAction("Index");
        }
    }
}

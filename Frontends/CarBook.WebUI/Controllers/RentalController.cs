using CarBook.Dto.CarDtos;
using CarBook.Dto.RentalDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CarBook.WebUI.Controllers
{
    public class RentalController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IRentalService _rentalService;
        private readonly ICarService _carService;

        public RentalController(ILocationService locationService, IRentalService rentalService, ICarService carService)
        {
            _locationService = locationService;
            _rentalService = rentalService;
            _carService = carService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.v1 = "Araçlar";
            ViewBag.v2 = "Uygun Araçlar";

            return View(new RentalCarsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(RentalCarsViewModel model)
        {

            var validationContext = new ValidationContext(model.RentalToCreate);
            var validationResults = new List<ValidationResult>();

            bool isValid = true;

            string[] fieldsToValidate = new[]
            {
                "RentDate",
                "ReturnDate",
                "RentTime",
                "ReturnTime",
                "PickupLocationId",
                "DropoffLocationId"
            };

            foreach (var field in fieldsToValidate)
            {
                validationContext.MemberName = field;
                var propertyValue = model.RentalToCreate.GetType()
                                       .GetProperty(field)
                                       ?.GetValue(model.RentalToCreate);

                if (!Validator.TryValidateProperty(propertyValue, validationContext, validationResults))
                {
                    isValid = false;
                }
            }

            if (!isValid)
            {
                return RedirectToAction("Index", "Default");
            }

            var cars = await _carService.GetAvailableRentalCars(model.RentalToCreate.PickupLocationId);
            if (!cars.Success)
            {
                model.ErrorMessage = cars.Message;
                return View(model);
            }

            model.AvailableRentalCars = cars.Data;
            _rentalService.CalculateTotalPrice(model);

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetLocations(string query)
        {
            var locations = await _locationService.GetAllLocations();
            if (!locations.Success)
            {
                return Json(new[]
                        {
                            new { label = "Konumlar getirilirken bir hata oluştu", value = 0 }
                        });
            }
            var results = locations.Data
                .Where(l => l.Name.StartsWith(query, StringComparison.OrdinalIgnoreCase))
                .Select(location => new
                {
                    label = location.Name,
                    value = location.LocationId,
                }).ToList();

            return Json(results);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRental()
        {
            ViewBag.v1 = "Araç Kirala";
            ViewBag.v2 = "Araç Kirala";

            CreateRentalViewModel model = new CreateRentalViewModel();
            if (!TempData.ContainsKey("RentalDto"))
            {
                return RedirectToAction("Index", "Default");
            }

            var json = TempData["RentalDto"] as string;
            var dto = JsonConvert.DeserializeObject<CreateRentalDto>(json);

            model.RentalToCreate = dto;

            var carToRental = await _carService.GetCarToRental(dto.CarId);
            if (!carToRental.Success)
            {
                model.ErrorMessage = carToRental.Message;
                return View(model);
            }

            model.CarToRental = carToRental.Data;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental(CreateRentalViewModel model)
        {
            var result = await _rentalService.CreateRental(model.RentalToCreate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                var carToRental = await _carService.GetCarToRental(model.RentalToCreate.CarId);
                if (!carToRental.Success)
                {
                    model.ErrorMessage = carToRental.Message;
                    return View(model);
                }
                model.CarToRental = carToRental.Data;
                return View(model);
            }
            return RedirectToAction("Index", "Default");
        }

        [HttpPost]
        public IActionResult RedirectAction(CreateRentalDto dto)
        {
            TempData["RentalDto"] = JsonConvert.SerializeObject(dto);
            return RedirectToAction("CreateRental");
        }
    }
}

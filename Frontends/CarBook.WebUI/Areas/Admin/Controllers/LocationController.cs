using CarBook.Dto.LocationDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }


        public async Task<IActionResult> Index()
        {
            var viewModel = new LocationViewModel();
            var result = await _locationService.GetAllLocations();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError("", errorMessage);
            }
            viewModel.Locations = result.Data;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateLocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationDto createLocationDto)
        {
            var result = await _locationService.CreateLocation(createLocationDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createLocationDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateLocation(int id)
        {
            var viewModel = new UpdateLocationViewModel();
            var location = await _locationService.GetLocationById(id);
            if (!location.Success)
            {
                viewModel.ErrorMessage = location.Message;
                return View(viewModel);
            }
            viewModel.LocationToUpdate = location.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLocation(UpdateLocationViewModel updateLocationViewModel)
        {
            var result = await _locationService.UpdateLocation(updateLocationViewModel.LocationToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateLocationViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteLocation(int id)
        {
            var result = await _locationService.DeleteLocation(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

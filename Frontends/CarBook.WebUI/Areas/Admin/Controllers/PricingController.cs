using CarBook.Dto.PricingDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class PricingController : Controller
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new PricingViewModel();
            var pricings = await _pricingService.GetAllPricing();
            if (!pricings.Success)
            {
                viewModel.ErrorMessage = pricings.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError("", errorMessage);
            }
            viewModel.Pricings = pricings.Data;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreatePricing()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePricing(CreatePricingDto createPricingDto)
        {
            var result = await _pricingService.CreatePricing(createPricingDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createPricingDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePricing(int id)
        {
            var viewModel = new UpdatePricingViewModel();
            var pricing = await _pricingService.GetPricingById(id);
            if (!pricing.Success)
            {
                viewModel.ErrorMessage = pricing.Message;
                return View(viewModel);
            }
            viewModel.PricingToUpdate = pricing.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePricing(UpdatePricingViewModel updatePricingViewModel)
        {
            var result = await _pricingService.UpdadetePricing(updatePricingViewModel.PricingToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updatePricingViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePricing(int id)
        {
            var result = await _pricingService.DeletePricing(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

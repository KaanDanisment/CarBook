using CarBook.Dto.FeatureDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Controllers
{
    public class AdminFeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public AdminFeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new FeatureViewModel();
            var features = await _featureService.GetAllFeatures();
            if (!features.Success)
            {
                viewModel.ErrorMessage = features.Message;
                return View(viewModel);
            }

            viewModel.Features = features.Data;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateFeature()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var result = await _featureService.CreateFeature(createFeatureDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createFeatureDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeature(int id)
        {
            var viewModel = new UpdateFeatureViewModel();
            var feature = await _featureService.GetFeatureById(id);
            if (!feature.Success)
            {
                viewModel.ErrorMessage = feature.Message;
                return View(viewModel);
            }
            viewModel.Feature = feature.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureViewModel updateFeatureViewModel)
        {
            var result = await _featureService.UpdateFeature(updateFeatureViewModel.Feature);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateFeatureViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFeature(int id)
        {
            var result = await _featureService.DeleteFeature(id);
            if (!result.Success)
            {
                var viewModel = new FeatureViewModel();
                var features = await _featureService.GetAllFeatures();
                if (!features.Success)
                {
                    viewModel.ErrorMessage = features.Message;
                    return View("Index",viewModel);
                }
                viewModel.Features = features.Data;
                return View("Index",viewModel);
            }
            return RedirectToAction("Index");
        }
    }
}

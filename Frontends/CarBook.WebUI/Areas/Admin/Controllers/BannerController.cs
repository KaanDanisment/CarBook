using CarBook.Dto.BannerDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new BannerViewModel();
            var result = await _bannerService.GetAllBanners();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            if(TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            viewModel.Banners = result.Data;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateBanner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto)
        {
            var result = await _bannerService.CreateBanner(createBannerDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createBannerDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBanner(int id)
        {
            var viewModel = new UpdateBannerViewModel();
            var banner = await _bannerService.GetBannerById(id);
            if (!banner.Success)
            {
                viewModel.ErrorMessage = banner.Message;
                return View();
            }
            viewModel.BannerToUpdate = banner.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBanner(UpdateBannerViewModel updateBannerViewModel)
        {
            var result = await _bannerService.UpdateBanner(updateBannerViewModel.BannerToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateBannerViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBanner(int id)
        {
            var result = await _bannerService.DeleteBanner(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

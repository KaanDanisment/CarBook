using CarBook.Dto.BannerDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultCoverUILayoutComponentPartial: ViewComponent
    {
        private readonly IBannerService _bannerService;

        public _DefaultCoverUILayoutComponentPartial(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new BannerViewModel();
            var result = await _bannerService.GetAllBanners();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            viewModel.Banners = result.Data;
            return View(viewModel);
        }
    }
}

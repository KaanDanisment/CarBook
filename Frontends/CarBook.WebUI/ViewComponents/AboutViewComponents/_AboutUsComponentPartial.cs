using CarBook.Dto.AboutDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.AboutViewComponents
{
    public class _AboutUsComponentPartial: ViewComponent
    {
        private readonly IAboutService _aboutService;

        public _AboutUsComponentPartial(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new AboutViewModel();
            var abouts = await _aboutService.GetAllAbouts();
            if (!abouts.Success)
            {
                viewModel.ErrorMessage = abouts.Message;
                return View(viewModel);
            }
            viewModel.Abouts = abouts.Data;
            return View(viewModel);
        }
    }
}

using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.ServiceViewComponents
{
    public class _ServiceComponentPartial: ViewComponent
    {
        private readonly IServiceService _serviceService;

        public _ServiceComponentPartial(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new ServiceViewModel();
            var result = await _serviceService.GetAllService();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            viewModel.Services = result.Data;
            return View(viewModel);
        }
    }
}

using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }


        public async Task<IActionResult> Index()
        {
            var viewModel = new ServiceViewModel();
            var result = await _serviceService.GetAllService();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMEssage"] is string errorMessage)
            {
                ModelState.AddModelError("", errorMessage);
            }
            viewModel.Services = result.Data;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            var result = await _serviceService.CreateService(createServiceDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createServiceDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var viewModel = new UpdateServiceViewModel();
            var service = await _serviceService.GetServiceById(id);
            if (!service.Success)
            {
                viewModel.ErrorMessage = service.Message;
                return View(viewModel);
            }
            viewModel.ServiceToUpdate = service.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceViewModel updateServiceViewModel)
        {
            var result = await _serviceService.UpdateService(updateServiceViewModel.ServiceToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateServiceViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _serviceService.DeleteService(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

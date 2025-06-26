using CarBook.Dto.FooterAddressDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class FooterAddressController : Controller
    {
        private readonly IFooterAddressService _footerAddressService;

        public FooterAddressController(IFooterAddressService footerAddressService)
        {
            _footerAddressService = footerAddressService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new FooterAddressViewModel();
            var result = await _footerAddressService.GetAllFooterAddresses();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError("", errorMessage);
            }
            viewModel.FooterAddresses = result.Data;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateFooterAddress()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFooterAddress(CreateFooterAddressDto createFooterAddressDto)
        {
            var result = await _footerAddressService.CreateFooterAddress(createFooterAddressDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createFooterAddressDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFooterAddress(int id)
        {
            var viewModel = new UpdateFooterAddressViewModel();
            var result = await _footerAddressService.GetFooterAddressById(id);
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            viewModel.FooterAddressToUpdate = result.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFooterAddress(UpdateFooterAddressViewModel updateFooterAddressViewModel)
        {
            var result = await _footerAddressService.UpdateFooterAddress(updateFooterAddressViewModel.FooterAddressToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateFooterAddressViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFooterAddress(int id)
        {
            var result = await _footerAddressService.DeleteFooterAddress(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

using CarBook.Dto.AboutDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AboutViewModel();
            var abouts = await _aboutService.GetAllAbouts();
            if (!abouts.Success)
            {
                viewModel.ErrorMessage = abouts.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            viewModel.Abouts = abouts.Data;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var result = await _aboutService.CreateAbout(createAboutDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createAboutDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var viewModel = new UpdateAboutViewModel();
            var about = await _aboutService.GetAboutById(id);
            if (!about.Success)
            {
                viewModel.ErrorMessage = about.Message;
                return View(viewModel);
            }
            viewModel.AboutToUpdate = about.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutViewModel updateAboutViewModel)
        {
            var result = await _aboutService.UpdateAbout(updateAboutViewModel.AboutToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateAboutViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAbout(int id)
        {
            var result = await _aboutService.DeleteAbout(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

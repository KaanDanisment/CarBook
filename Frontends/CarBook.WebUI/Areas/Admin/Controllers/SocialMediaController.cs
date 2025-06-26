using CarBook.Dto.SocialMediaDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new SocialMediasViewModel();
            var socialMedias = await _socialMediaService.GetAllSociaMedias();
            if (!socialMedias.Success)
            {
                viewModel.ErrorMessage = socialMedias.Message;
                return View(viewModel);
            }
            viewModel.SocialMedias = socialMedias.Data;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            var result = await _socialMediaService.CreateSocialMedia(createSocialMediaDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createSocialMediaDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            var viewModel = new UpdateSocialMediaViewModel();
            var socialMedia = await _socialMediaService.GetSocialMediaById(id);
            if (!socialMedia.Success)
            {
                viewModel.ErrorMessage = socialMedia.Message;
                return View(viewModel);
            }
            viewModel.SocialMediaToUpdate = socialMedia.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaViewModel updateSocialMediaViewModel)
        {
            var result = await _socialMediaService.UpdateSocialMedia(updateSocialMediaViewModel.SocialMediaToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateSocialMediaViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var result = await _socialMediaService.DeleteSocialMedia(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

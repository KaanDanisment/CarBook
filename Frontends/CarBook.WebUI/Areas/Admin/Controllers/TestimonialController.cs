using CarBook.Dto.TestimonialDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new TestimonialViewModel();
            var testimonials = await _testimonialService.GetAllTestimonials();
            if (!testimonials.Success)
            {
                viewModel.ErrorMessage = testimonials.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMessage"] is string errorMessage)
            {
                viewModel.ErrorMessage = errorMessage;
            }
            viewModel.Testimonials = testimonials.Data;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var result = await _testimonialService.CreateTestimonial(createTestimonialDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createTestimonialDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var viewModel = new UpdateTestimonialViewModel();
            var result = await _testimonialService.GetTestimonialById(id);
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            viewModel.TestimonialToUpdate = result.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialViewModel updateTestimonialViewModel)
        {
            var result = await _testimonialService.UpdateTestimonial(updateTestimonialViewModel.TestimonialToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateTestimonialViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var result = await _testimonialService.DeleteTestimonial(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

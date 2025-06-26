using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.TestimonialViewComponents
{
    public class _TestimonialComponentPartial: ViewComponent
    {
        private readonly ITestimonialService _testimonialService;

        public _TestimonialComponentPartial(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new TestimonialViewModel();
            var testimonials = await _testimonialService.GetAllTestimonials();
            if (!testimonials.Success)
            {
                viewModel.ErrorMessage = testimonials.Message;
                return View(viewModel);
            }
            viewModel.Testimonials = testimonials.Data;
            return View(viewModel);
        }
    }
}

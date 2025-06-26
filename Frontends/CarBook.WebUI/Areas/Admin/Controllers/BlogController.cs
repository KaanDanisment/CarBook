using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new BlogsViewModel();
            var result = await _blogService.GetAllBlogs();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            viewModel.Blogs = result.Data;
            return View(viewModel);
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _blogService.DeleteBlog(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

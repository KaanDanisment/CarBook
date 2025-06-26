using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailMainComponentPartial : ViewComponent
    {
        private readonly IBlogService _blogService;

        public _BlogDetailMainComponentPartial(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var viewModel = new BlogViewModel();
            var blog = await _blogService.GetBlogById(id);
            if (!blog.Success)
            {
                viewModel.ErrorMessage = blog.Message;
                return View(viewModel);
            }
            viewModel.Blog = blog.Data;
            return View(viewModel);
        }
    }
}

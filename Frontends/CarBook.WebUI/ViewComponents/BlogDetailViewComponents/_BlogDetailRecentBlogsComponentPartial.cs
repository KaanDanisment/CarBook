using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailRecentBlogsComponentPartial: ViewComponent
    {
        private readonly IBlogService _blogService;

        public _BlogDetailRecentBlogsComponentPartial(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new BlogsViewModel();
            var blogs = await _blogService.GetLatest3Blogs();
            if (!blogs.Success)
            {
                viewModel.ErrorMessage = blogs.Message;
                return View(viewModel);
            }
            viewModel.Blogs = blogs.Data;
            return View(viewModel);
        }
    }
}

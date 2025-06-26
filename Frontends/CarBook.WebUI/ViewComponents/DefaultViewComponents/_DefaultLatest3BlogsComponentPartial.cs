using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultLatest3BlogsComponentPartial:ViewComponent
    {
        private readonly IBlogService _blogService;

        public _DefaultLatest3BlogsComponentPartial(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new BlogsViewModel();
            var result = await _blogService.GetLatest3Blogs();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            viewModel.Blogs = result.Data;
            return View(viewModel);
        }
    }
}

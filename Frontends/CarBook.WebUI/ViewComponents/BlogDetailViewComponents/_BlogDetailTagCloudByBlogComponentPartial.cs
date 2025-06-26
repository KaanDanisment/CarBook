using CarBook.Dto.TagCloudDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailTagCloudByBlogComponentPartial: ViewComponent
    {
        private readonly ITagCloudService _tagCloudService;

        public _BlogDetailTagCloudByBlogComponentPartial(ITagCloudService tagCloudService)
        {
            _tagCloudService = tagCloudService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var viewModel = new TagCloudsViewModel();
            var tagClouds = await _tagCloudService.GetTagCloudsByBlogId(id);
            if (!tagClouds.Success)
            {
                viewModel.ErrorMessage = tagClouds.Message;
                return View(viewModel);
            }
            viewModel.TagClouds = tagClouds.Data;
            return View(viewModel);
        }
    }
}

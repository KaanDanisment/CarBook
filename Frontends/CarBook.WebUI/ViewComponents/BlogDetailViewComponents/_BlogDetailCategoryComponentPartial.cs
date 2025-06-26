using CarBook.Dto.CategoryDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailCategoryComponentPartial: ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _BlogDetailCategoryComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new CategoryViewModel();
            var categories = await _categoryService.GetAllCategories();
            if (!categories.Success)
            {
                viewModel.ErrorMessage = categories.Message;
                return View(viewModel);
            }
            viewModel.Categories = categories.Data;
            return View(viewModel);
        }
    }
}

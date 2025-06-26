using CarBook.Dto.CategoryDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new CategoryViewModel();
            var categories = await _categoryService.GetAllCategories();
            if (!categories.Success)
            {
                viewModel.ErrorMessage = categories.Message;
                return View(viewModel);
            }

            if (TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            viewModel.Categories = categories.Data;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var result = await _categoryService.CreateCategory(createCategoryDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createCategoryDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var viewModel = new UpdateCategoryViewModel();
            var category = await _categoryService.GetCategoryById(id);
            if (!category.Success)
            {
                viewModel.ErrorMessage = category.Message;
                return View(viewModel);
            }
            viewModel.CategoryToUpdate = category.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel updateCategoryViewModel)
        {
            var resuult = await _categoryService.UpdateCategory(updateCategoryViewModel.CategoryToUpdate);
            if (!resuult.Success)
            {
                ModelState.AddModelError("", resuult.Message);
                return View(updateCategoryViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

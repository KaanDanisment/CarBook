using CarBook.Dto.BrandDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Controllers
{
    public class AdminBrandController : Controller
    {
        private readonly IBrandService _brandService;

        public AdminBrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new BrandViewModel();
            var result = await _brandService.GetAllBrands();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            viewModel.Brands = result.Data;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            var result = await _brandService.CreateBrand(createBrandDto);
            if(!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createBrandDto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            var viewModel = new UpdateBrandViewModel();
            var brand = await _brandService.GetBrandById(id);
            if (!brand.Success)
            {
                viewModel.ErrorMessage = brand.Message;
                return View(viewModel);
            }
            viewModel.BrandToUpdate = brand.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandViewModel updateBrandViewModel)
        {
            var result = await _brandService.UpdateBrand(updateBrandViewModel.BrandToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateBrandViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBrand(int id)
        {
            var result = await _brandService.DeleteBrand(id);
            if (!result.Success)
            {
                var viewModel = new BrandViewModel();
                var brands = await _brandService.GetAllBrands();
                if (!brands.Success)
                {
                    viewModel.ErrorMessage = brands.Message;
                    return View(viewModel);
                }
                viewModel.Brands = brands.Data;
                return View("Index", viewModel);
            }
            return RedirectToAction("Index");
        }
    }
}

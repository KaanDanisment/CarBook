using CarBook.Dto.AuthorDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AuthorViewModel();
            var authors = await _authorService.GetAllAuthors();
            if (!authors.Success)
            {
                viewModel.ErrorMessage = authors.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            viewModel.Authors = authors.Data;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            var result = await _authorService.CreateAuthor(createAuthorDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(createAuthorDto);
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> UpdateAuthor(int id)
        {
            var viewModel = new UpdateAuthorViewModel();
            var author = await _authorService.GetAuthorById(id);
            if (!author.Success)
            {
                viewModel.ErrorMessage = author.Message;
                return View(viewModel);
            }
            viewModel.AuthorToUpdate = author.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorViewModel updateAuthorViewModel)
        {
            var result = await _authorService.UpdateAuthor(updateAuthorViewModel.AuthorToUpdate);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(updateAuthorViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthor(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] =  result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

using CarBook.Dto.BlogDtos;
using CarBook.Dto.CommentDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;

        public BlogController(IBlogService blogService, ICommentService commentService)
        {
            _blogService = blogService;
            _commentService = commentService;
        }

        public async Task<IActionResult>Index()
        {
            ViewBag.v1 = "Blog";
            ViewBag.v2 = "Bloglar";
            var viewModel = new BlogsViewModel();
            var blogs = await _blogService.GetAllBlogs();
            if (!blogs.Success)
            {
                viewModel.ErrorMessage = blogs.Message;
                return View(viewModel);
            }
            viewModel.Blogs = blogs.Data;
            return View(viewModel);
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            ViewBag.v1 = "Blog";
            ViewBag.v2 = "Blog Detayı ve Yorumlar";
            
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentByBlogId(CreateCommentDto createCommentDto)
        {
            var result = await _commentService.CreateComment(createCommentDto);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("BlogDetail", new { id = createCommentDto.BlogId });
            }
            return RedirectToAction("BlogDetail", new { id = createCommentDto.BlogId });
        }
    }
}

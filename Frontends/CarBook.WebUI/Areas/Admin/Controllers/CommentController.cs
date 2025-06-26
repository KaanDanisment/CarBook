using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var viewModel = new CommentsViewModel();
            var comments = await _commentService.GetCommentsByBlogId(id);
            if (!comments.Success)
            {
                viewModel.ErrorMessage = comments.Message;
                return View(viewModel);
            }
            if (TempData["ErrorMessage"] is string errorMessage)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            viewModel.Comments = comments.Data;
            return View(viewModel);
        }

        public async Task<IActionResult> DeleteComment(int commentId, int blogId)
        {
            var result = await _commentService.DeleteComment(commentId);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("Index", new { id = blogId });
        }
    }
}

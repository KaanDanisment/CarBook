using CarBook.WebUI.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogsCommentViewComponents
{
    public class _CommentListByBlogComponentPartial: ViewComponent
    {
        private readonly ICommentService _commentService;

        public _CommentListByBlogComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var viewModel = new CommentsViewModel();
            var comments = await _commentService.GetCommentsByBlogId(id);
            if (!comments.Success)
            {
                viewModel.ErrorMessage = comments.Message;
                return View(viewModel);
            }
            var commentsCount = await _commentService.GetCommentsCountByBlogId(id);
            viewModel.TotalCommentsCount = commentsCount.Data;
            viewModel.Comments = comments.Data;
            return View(viewModel);
        }
    }
}

using CarBook.Dto.CommentDtos;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogsCommentViewComponents
{
    public class _CreateCommentComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            CreateCommentDto createCommentDto = new CreateCommentDto
            {
                BlogId = id
            };
            return View(createCommentDto);
        }
    }
}

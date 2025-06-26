using CarBook.Dto.CommentDtos;

namespace CarBook.WebUI.Models
{
    public class CommentsViewModel
    {
        public IEnumerable<CommentDto>? Comments { get; set; }
        public CommentsCountDto TotalCommentsCount { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

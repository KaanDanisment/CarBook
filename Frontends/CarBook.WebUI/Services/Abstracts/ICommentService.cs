using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.CommentDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface ICommentService
    {
        Task<IDataResult<IEnumerable<CommentDto>>> GetCommentsByBlogId(int blogId);
        Task<IResult> DeleteComment(int commentId);
        Task<IDataResult<CommentsCountDto>> GetCommentsCountByBlogId(int blogId);
        Task<IResult> CreateComment(CreateCommentDto createCommentDto);
    }
}

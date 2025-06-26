using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.BlogDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IBlogService
    {
        Task<IDataResult<IEnumerable<BlogDto>>> GetAllBlogs();
        Task<IDataResult<BlogDto>> GetBlogById(int id);
        Task<IDataResult<IEnumerable<BlogDto>>> GetLatest3Blogs();
        Task<IResult> CreateBlog(CreateBlogDto createBlogDto);
        Task<IResult> UpdateBlog(BlogDto blogDto);
        Task<IResult> DeleteBlog(int id);
    }
}

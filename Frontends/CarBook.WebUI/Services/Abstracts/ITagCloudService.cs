using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.TagCloudDtos;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface ITagCloudService
    {
        Task<IDataResult<IEnumerable<TagCloudDto>>> GetTagCloudsByBlogId(int blogId);
    }
}

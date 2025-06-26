using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.AuthorDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IAuthorService
    {
        Task<IDataResult<IEnumerable<AuthorDto>>> GetAllAuthors();
        Task<IResult> CreateAuthor(CreateAuthorDto createAuthorDto);
        Task<IResult> UpdateAuthor(AuthorDto authorDto);
        Task<IDataResult<AuthorDto>> GetAuthorById(int id);
        Task<IResult> DeleteAuthor(int id);

    }
}

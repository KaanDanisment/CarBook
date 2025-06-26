using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.CategoryDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<IDataResult<IEnumerable<CategoryDto>>> GetAllCategories();
        Task<IDataResult<CategoryDto>> GetCategoryById(int id);
        Task<IResult> CreateCategory(CreateCategoryDto createCategoryDto);
        Task<IResult> UpdateCategory(CategoryDto categoryDto);
        Task<IResult> DeleteCategory(int id);
    }
}

using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.TestimonialDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface ITestimonialService
    {
        Task<IDataResult<IEnumerable<TestimonialDto>>> GetAllTestimonials();
        Task<IDataResult<TestimonialDto>> GetTestimonialById(int id);
        Task<IResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto);
        Task<IResult> UpdateTestimonial(TestimonialDto testimonialDto);
        Task<IResult> DeleteTestimonial(int id);

    }
}

using CarBook.Dto.TestimonialDtos;

namespace CarBook.WebUI.Models
{
    public class TestimonialViewModel
    {
        public IEnumerable<TestimonialDto>? Testimonials { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

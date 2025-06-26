using CarBook.Dto.TestimonialDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateTestimonialViewModel
    {
        public TestimonialDto? TestimonialToUpdate{ get; set; }
        public string ErrorMessage { get; set; }
    }
}

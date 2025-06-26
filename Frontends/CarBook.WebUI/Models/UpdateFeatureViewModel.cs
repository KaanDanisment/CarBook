using CarBook.Dto.FeatureDtos;

namespace CarBook.WebUI.Models
{
    public class UpdateFeatureViewModel
    {
        public FeatureDto? Feature { get; set; }
        public string? ErrorMessage { get; set; }

    }
}

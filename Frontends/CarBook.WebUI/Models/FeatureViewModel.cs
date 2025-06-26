using CarBook.Dto.FeatureDtos;

namespace CarBook.WebUI.Models
{
    public class FeatureViewModel
    {
        public IEnumerable<FeatureDto>? Features { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

using CarBook.Dto.TagCloudDtos;

namespace CarBook.WebUI.Models
{
    public class TagCloudsViewModel
    {
        public IEnumerable<TagCloudDto>? TagClouds { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

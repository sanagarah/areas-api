using System.ComponentModel.DataAnnotations;


namespace areas_api.Models.DTOs
{
	public class ImageRequestDto
    {

        [Required]
        public required IFormFile File { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}


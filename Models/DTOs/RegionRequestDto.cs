using System.ComponentModel.DataAnnotations;

namespace areas_api.Models.DTOs
{
	public class RegionRequestDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public required string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}


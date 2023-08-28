using System.ComponentModel.DataAnnotations.Schema;

namespace areas_api.Models.Domin
{
	public class Image
	{
		public Guid Id { get; set; }
        [NotMapped]
        public required IFormFile File { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Extension { get; set; }
        public long SizeInBytes { get; set; }
        public string? Path { get; set; }
    }
}


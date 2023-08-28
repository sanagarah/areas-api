using areas_api.Models.Domin;
using areas_api.Models.DTOs;
using areas_api.Repositores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace areas_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageRequestDto imageRequestDto)
        {
            ValidateFileUpload(imageRequestDto);
            if(ModelState.IsValid)
            {
                Image imageDomain = new Image()
                {
                    File = imageRequestDto.File,
                    Name = imageRequestDto.Name,
                    Description = imageRequestDto.Description,
                    Extension = Path.GetExtension(imageRequestDto.File.FileName),
                    SizeInBytes = imageRequestDto.File.Length,
                };
                await _imageRepository.UploadImage(imageDomain);
                return Ok(imageDomain);
            }
            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageRequestDto imageRequestDto)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtentions.Contains(Path.GetExtension(imageRequestDto.File.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(imageRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10 MB");
            }
        }

    }
}


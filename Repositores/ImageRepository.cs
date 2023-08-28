using areas_api.Data;
using areas_api.Models.Domin;

namespace areas_api.Repositores
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _db;

        public ImageRepository(IWebHostEnvironment webHostEnviroment, IHttpContextAccessor contextAccessor, AppDbContext db)
        {
            _webHostEnviroment = webHostEnviroment;
            _contextAccessor = contextAccessor;
            _db = db;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnviroment.ContentRootPath, "Images", image.Name + image.Extension);
            using var fileStream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(fileStream);

            var urlFilePath = $"{_contextAccessor.HttpContext?.Request.Scheme}://{_contextAccessor.HttpContext?.Request.Host}{_contextAccessor.HttpContext?.Request.PathBase}/Images/{image.Name}{image.Extension}";
            image.Path = urlFilePath;
            await _db.Images.AddAsync(image);
            await _db.SaveChangesAsync();
            return image;
        }
    }
}


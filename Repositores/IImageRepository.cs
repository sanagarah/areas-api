using areas_api.Models.Domin;

namespace areas_api.Repositores
{
	public interface IImageRepository
	{
		Task<Image> UploadImage(Image image);
	}
}


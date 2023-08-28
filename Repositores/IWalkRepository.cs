using areas_api.Models;

namespace areas_api.Repositores
{
	public interface IWalkRepository
	{
        Task<List<Walk>> GetAllAsync(string? filterColumn, string? filterValue, string? sortBy, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}


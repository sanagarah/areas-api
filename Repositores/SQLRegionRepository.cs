using areas_api.Data;
using areas_api.Models;
using Microsoft.EntityFrameworkCore;

namespace areas_api.Repositores
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly AppDbContext _db;

        public SQLRegionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _db.Regions.AddAsync(region);
            await _db.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            Region? regionDomain = await _db.Regions.FindAsync(id);
            if (regionDomain == null)
            {
                return null;
            }

            _db.Regions.Remove(regionDomain);
            await _db.SaveChangesAsync();
            return regionDomain;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _db.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _db.Regions.FindAsync(id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            Region? regionDomain = await _db.Regions.FindAsync(id);
            if (regionDomain == null)
            {
                return null;
            }

            regionDomain.Code = region.Code;
            regionDomain.Name = region.Name;
            regionDomain.RegionImageUrl = region.RegionImageUrl;

            _db.Regions.Update(regionDomain);
            await _db.SaveChangesAsync();
            return regionDomain;
        }

    }
}


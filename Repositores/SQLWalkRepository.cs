using areas_api.Data;
using areas_api.Models;
using Microsoft.EntityFrameworkCore;

namespace areas_api.Repositores
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly AppDbContext _db;

        public SQLWalkRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _db.Walks.AddAsync(walk);
            await _db.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            Walk? walkDomain = await _db.Walks.FindAsync(id);
            if(walkDomain == null)
            {
                return null;
            }
            _db.Walks.Remove(walkDomain);
            await _db.SaveChangesAsync();

            return walkDomain;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterColumn, string? filterValue, string? sortBy, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            IQueryable<Walk> walks = _db.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(filterColumn) == false && string.IsNullOrWhiteSpace(filterValue) == false)
            {
                if (filterColumn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterValue));
                }
            }

            //sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
             {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
            }

            //pagination
            var skipResult = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _db.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            Walk? walkDomain = await _db.Walks.FindAsync(id);
            if(walkDomain == null)
            {
                return null;
            }

            walkDomain.Name = walk.Name;
            walkDomain.Description = walk.Description;
            walkDomain.LengthInKm = walk.LengthInKm;
            walkDomain.WalkImageUrl = walk.WalkImageUrl;
            walkDomain.DifficultyId = walk.DifficultyId;
            walkDomain.RegionId = walk.RegionId;

            _db.Walks.Update(walkDomain);
            await _db.SaveChangesAsync();

            return walkDomain;
        }
    }
}


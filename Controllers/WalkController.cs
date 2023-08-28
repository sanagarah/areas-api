using areas_api.CustomActionFilters;
using areas_api.Models;
using areas_api.Models.DTOs;
using areas_api.Repositores;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace areas_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalkController : Controller
    {
        private readonly IWalkRepository _walkRepositery;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepositery, IMapper mapper)
        {
            _walkRepositery = walkRepositery;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterColumn, [FromQuery] string? filterValue, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            List<Walk> walkDomain = await _walkRepositery.GetAllAsync(filterColumn, filterValue, sortBy, isAscending?? true, pageNumber, pageSize) ;
            List<WalkDto> walkDto = _mapper.Map<List<WalkDto>>(walkDomain);

            return Ok(walkDto);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetWalk([FromRoute] Guid id)
        {
            Walk? walkDomain = await _walkRepositery.GetByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            WalkDto walkDto = _mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> PostWalk([FromBody] WalkRequestDto walkRequestDto)
        {
            Walk walkDomain = _mapper.Map<Walk>(walkRequestDto);
            await _walkRepositery.CreateAsync(walkDomain);
            WalkDto walkDto = _mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> PostWalk([FromRoute] Guid id, [FromBody] WalkRequestDto walkRequestDto)
        {
            Walk walk = _mapper.Map<Walk>(walkRequestDto);
            Walk? walkDomain = await _walkRepositery.UpdateAsync(id, walk);
            if (walkDomain == null)
            {
                return NotFound();
            }
            WalkDto walkDto = _mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            Walk? walkDomain = await _walkRepositery.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            WalkDto walkDto = _mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);

        }
    }
}


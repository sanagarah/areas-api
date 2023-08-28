using areas_api.CustomActionFilters;
using areas_api.Models;
using areas_api.Models.DTOs;
using areas_api.Repositores;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace areas_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepositery;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepositery, IMapper mapper)
        {
            _regionRepositery = regionRepositery;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            List<Region> regionsDomain = await _regionRepositery.GetAllAsync();
            List<RegionDto> regionDto = _mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegion([FromRoute] Guid id)
        {
            Region? regionDomain = await _regionRepositery.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            RegionDto regionDto = _mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PostRegion([FromBody] RegionRequestDto regionRequestDto)
        {
            Region regionDomain = _mapper.Map<Region>(regionRequestDto);

            await _regionRepositery.CreateAsync(regionDomain);
            RegionDto regionDto = _mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetRegion), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> PutRegion([FromRoute] Guid id, [FromBody] RegionRequestDto regionRequestDto)
        {
            Region region = _mapper.Map<Region>(regionRequestDto);

            Region? regionDomain = await _regionRepositery.UpdateAsync(id, region);
            if (regionDomain == null)
            {
                return NotFound();
            }

            RegionDto regionDto = _mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            Region? regionDomain = await _regionRepositery.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            RegionDto regionDto = _mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }
    }
}


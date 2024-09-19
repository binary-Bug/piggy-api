using AngularWebApi.Models;
using AngularWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly RegionManager _regionManager;
        public RegionController(RegionManager regionManager) 
        {
            this._regionManager = regionManager;
        }

        [HttpGet("allRegions")]
        public IEnumerable<Region> Get()
        {
            return _regionManager.GetRegions();
        }

        [HttpPost("addRegion")]
        public async Task<Region?> Add(string region)
        {
            return await _regionManager.AddRegionAsync(region);
        }
    }
}

using AngularWebApi.Models;
using AngularWebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Piggy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurentController : ControllerBase
    {
        private readonly RestaurentManager _restaurentManager;
        public RestaurentController(RestaurentManager restaurentManager) {
            _restaurentManager = restaurentManager;
        }
        [HttpGet("types")]
        public IEnumerable<RestaurentType> Get()
        {
            return _restaurentManager.GetAllRestaurentTypes();
        }
    }
}

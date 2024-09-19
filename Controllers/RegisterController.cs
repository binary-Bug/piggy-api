using AngularWebApi.Dtos;
using AngularWebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RegionManager _regionManager;

        public RegisterController(UserManager<IdentityUser> userManager, RegionManager regionManager)
        {
            _userManager = userManager;
            _regionManager = regionManager;
        }

        [HttpPost("user")]
        public async Task<ActionResult<string>> Register(UserRegistrationDTO userRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser user = new IdentityUser
            {
                Email = userRegistrationDto.Email,
                UserName = userRegistrationDto.UserName,
                PhoneNumber = userRegistrationDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "User");

            var isMapped = await _regionManager.MapUserToRegionAsync(user, userRegistrationDto.RegionId);

            if(!isMapped) return BadRequest("User Could not be mapped to the selected region");

            return Ok("Account Created Successfully");
        }
    }
}

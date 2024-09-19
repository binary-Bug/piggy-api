using AngularWebApi.Dtos;
using AngularWebApi.Models;
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
        private readonly RestaurentManager _restaurentManager;

        public RegisterController(UserManager<IdentityUser> userManager, RegionManager regionManager, RestaurentManager restaurentManager)
        {
            _userManager = userManager;
            _regionManager = regionManager;
            _restaurentManager = restaurentManager;
        }

        [HttpPost("user")]
        public async Task<ActionResult<string>> RegisterUser(UserRegistrationDTO userRegistrationDto)
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

        [HttpPost("restaurent")]
        public async Task<ActionResult<string>> RegisterRestaurent(RestaurentRegistrationDTO restaurentRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser user = new IdentityUser
            {
                Email = restaurentRegistrationDto.Email,
                UserName = restaurentRegistrationDto.UserName,
                PhoneNumber = restaurentRegistrationDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, restaurentRegistrationDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "Owner");

            Restaurent? restaurent = await _restaurentManager.AddRestaurentAsync(user,restaurentRegistrationDto.RestaurentName,restaurentRegistrationDto.RestaurentTypeId);

            if(restaurent != null)
            {
                return Ok("Account Created Successfully and Added New Restaurent");
            }
            else
            {
                return BadRequest("Error Creating Account");
            }
            
        }
    }
}

using AngularWebApi.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("user")]
        public async Task<ActionResult<string>> Register(UserRegistrationDTO userRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser
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

            foreach (var role in userRegistrationDto.Roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            
            return Ok("Account Created Successfully");
        }
    }
}

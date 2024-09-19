using AngularWebApi.Dtos;
using AngularWebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenService _tokenService;

        public LoginController(UserManager<IdentityUser> userManager, TokenService tokenService) 
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginCredentialsDTO loginCredentialsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginCredentialsDto.UsernameOrEmail);
            if(user == null) { user = await _userManager.FindByNameAsync(loginCredentialsDto.UsernameOrEmail); }
            if (user is null) { return Unauthorized(); }

            var result = await _userManager.CheckPasswordAsync(user, loginCredentialsDto.Password);

            if (!result)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(token);
        }
    }
}

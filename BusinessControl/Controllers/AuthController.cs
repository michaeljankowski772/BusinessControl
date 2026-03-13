using BusinessControlService.Models;
using BusinessControlService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthController(
            UserManager<IdentityUser> userManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
                return Unauthorized();

            var valid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!valid)
                return Unauthorized();

            var token = _jwtService.GenerateJwt(user.Id, user.UserName);

            return Ok(new { token });
        }

    }
}

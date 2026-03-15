using BusinessControlService.Models;
using BusinessControlService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

            var token = _jwtService.GenerateJwt(user.Id, user.UserName!);

            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> IsUserAuthenticated()
        {
            var user = User;
            var isAuthenticated = User.Identity != null && User.Identity.IsAuthenticated;
            return Ok(new { isAuthenticated });
        }

        private bool IsUserAuthenticated(string userName)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
                return false;
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name && c.Value == userName);
            return claim != null;
        }
    }
}

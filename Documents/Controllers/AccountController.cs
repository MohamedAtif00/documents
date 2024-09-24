using Documents.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Documents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AccountController(UserManager<IdentityUser<Guid>> userManager, SignInManager<IdentityUser<Guid>> signInManager, JwtTokenHelper jwtHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenHelper = jwtHelper;
        }

        // POST: api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser<Guid>
            {
                Id = Guid.NewGuid(),
                UserName = model.username,
                Email = model.email,
            };

            var result = await _userManager.CreateAsync(user, model.password);

            // Generate JWT Token
            if (result.Succeeded)
            {
                var token = _jwtTokenHelper.GenerateJwtToken(user.Id.ToString(), user.UserName, user.Email);

                return Ok(new
                {
                    Token = token,
                    UserId = user.Id // Return userId with the response
                });
            }

            return BadRequest(result.Errors);
        }

        // POST: api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.username, model.password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.username);

                // Generate JWT Token
                var token = _jwtTokenHelper.GenerateJwtToken(user.Id.ToString(), user.UserName, user.Email);

                return Ok(new
                {
                    Token = token,
                    UserId = user.Id // Return userId with the response
                });
            }

            return Unauthorized(new { Message = "Invalid username or password." });
        }
    }

    public record RegisterDto(string username,string email,string password);
    public record LoginDto(string username,string password);
}

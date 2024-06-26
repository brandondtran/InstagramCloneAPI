using InstagramCloneAPI.Dtos;
using InstagramCloneAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InstagramCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginRequest);

            if (!result.IsSuccess)
            {
                return Unauthorized(result.ErrorMessage);
            }

            return Ok(new { Token = result.Token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return NoContent();
        }
    }
}

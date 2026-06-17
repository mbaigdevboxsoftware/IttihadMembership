using IttihadmembershipAPI.Business;
using IttihadmembershipAPI.DTO_s;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IttihadmembershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationAPIController :ControllerBase
    {
        private readonly IAuthenticationService _AuthenticationService;
        public AuthenticationAPIController(IAuthenticationService authenticationService)
        {
            _AuthenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
        [FromBody] LoginRequestDTO request)
        {
            var result =
                await _AuthenticationService.Authentication(request);

            if (result == null)
                return Unauthorized(new
                {
                    Message = "Invalid username or password"
                });
            // Set refresh token in HttpOnly cookie
            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });


            // Return only access token to frontend
            return Ok(new
            {
                accessToken = result.AccessToken,
                expiresAt = result.ExpiresAt
            });
        }

        [HttpPost("Register")]
        
        public async Task<IActionResult> Register(RegisterDTO request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            var result = await _AuthenticationService.Register(request);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = _AuthenticationService.RefreshToken(refreshToken);

            if (result == null)
                return Unauthorized();
            // Replace old cookie with new refresh token
            Response.Cookies.Append(
                "refreshToken",
                result.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(30)
                });

            return Ok(result);
        }

    }
}

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

            return Ok(result);
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
        public IActionResult Refresh([FromBody] RefreshTokenRequestDTO request)
        {
            var result =_AuthenticationService.RefreshToken(request.RefreshToken);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }

    }
}

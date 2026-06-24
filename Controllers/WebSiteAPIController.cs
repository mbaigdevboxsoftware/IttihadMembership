using Microsoft.AspNetCore.Mvc;
using IttihadmembershipAPI.Business;
using IttihadmembershipAPI.DTO_s;

namespace IttihadmembershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSiteAPIController : ControllerBase
    {
        private readonly IWebsiteService _WebsiteService;
        public WebSiteAPIController(IWebsiteService WebsiteService)
        {
            _WebsiteService = WebsiteService;
        }
        [HttpPost("Userlogin")]
        public async Task<IActionResult> Login(
       [FromBody] LoginRequestDTO request)
        {
            var result =
                await _WebsiteService.UserAuthentication(request);

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
                MemeberId = result.MemberId,
                EmailId = result.EmailId,
                MobileNo = result.MobileNo,
                expiresAt = result.ExpiresAt
            });
        }
        [HttpPost("UserRegister")]

        public async Task<IActionResult> Register(WebsiteDTO request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            var result = await _WebsiteService.UserRegister(request);
            return Ok(result);
        }
        [HttpPost]
        [Route("NewGetNationality")]
        public IActionResult NewGetNationality([FromBody] NationalityDTO obj)
        {
            var result = _WebsiteService.GetNationality(obj);
            return Ok(result);
        }
        [HttpPost]
        [Route("NewWebsitePackages")]
        public IActionResult WebsitePackages([FromBody] PackageDTO obj)
        {
            var result = _WebsiteService.WebsitePackages(obj);
            return Ok(result);
        }

    }
}

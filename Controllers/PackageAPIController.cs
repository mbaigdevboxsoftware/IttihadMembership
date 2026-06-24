using IttihadmembershipAPI.Business;
using IttihadmembershipAPI.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IttihadmembershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageAPIController : ControllerBase
    {
        private readonly IPackageService _PackageService;
        public PackageAPIController(IPackageService PackageService)
        {
           _PackageService = PackageService;
        }
        [Authorize]
        [HttpPost]
        [Route("NewPackage")]
        public IActionResult NewPackage([FromBody] PackageDTO obj)
        {
            var userId = int.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? throw new UnauthorizedAccessException());

            var result = _PackageService.NewPackage(obj, userId);
            if (result == null)
                return StatusCode(500, "Something went wrong");

            return Ok(result);
        }
        
       // [Authorize]
        [HttpPost]
        [Route("NewGetPackages")]
        public IActionResult NewGetPackages([FromBody] PackageDTO obj)
        {
            var result = _PackageService.GetPackages(obj);
            return Ok(result);
        }
    }
}

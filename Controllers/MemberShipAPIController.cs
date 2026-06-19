using IttihadmembershipAPI.Business;
using IttihadmembershipAPI.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IttihadmembershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberShipAPIController:ControllerBase
    {
        private readonly IMemberShipService _MemberShipService;
        public MemberShipAPIController(IMemberShipService MemberShipService)
        {
            _MemberShipService = MemberShipService;
        }
        [Authorize]
        [HttpPost]
        [Route("ManageMembership")]
        public IActionResult ManageMembership([FromBody] MemberShipDTO obj)
        {
            var result = _MemberShipService.ManageMembership(obj);
            if (result == null)
                return StatusCode(500, "Something went wrong");

            return Ok(result);
        }
        //[Authorize]
        [HttpPost]
        [Route("NewGetMemberships")]
        public IActionResult NewGetMemberships([FromBody] MemberShipDTO obj)
        {
            var result = _MemberShipService.GetMemberships(obj);
            return Ok(result);
        }
    }
}

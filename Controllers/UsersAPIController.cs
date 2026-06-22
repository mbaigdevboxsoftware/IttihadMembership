using IttihadmembershipAPI.Business;
using IttihadmembershipAPI.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IttihadmembershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController: ControllerBase
    {
        private readonly IUsersService _UsersService;
        public UsersAPIController(IUsersService UsersService)
        {
            _UsersService = UsersService;
        }

        //[Authorize]
        [HttpPost]
        [Route("NewgetUsers")]
        public IActionResult NewgetUsers([FromBody] UsersDTO obj)
        {
            var result = _UsersService.GetUsers(obj);
            return Ok(result);
        }
    }
}

using IttihadmembershipAPI.Business;
using IttihadmembershipAPI.DTO_s;
using Microsoft.AspNetCore.Mvc;

namespace IttihadmembershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAPIController: ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleAPIController(IRoleService roleService)
        { 
            _roleService = roleService;
        }
        [HttpPost]
        [Route("ManageRole")]
        public IActionResult ManageRole([FromBody] RoleDTO obj)
        {
            var result = _roleService.ManageRole(obj);
            return Ok(result);
        }
        [HttpPost]
        [Route("GetRoles")]
        public IActionResult GetRoles([FromBody] RoleDTO obj)
        {
            var result = _roleService.GetRoles(obj);
            return Ok(result);
        }
    }
}

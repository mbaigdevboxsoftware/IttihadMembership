using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;

namespace IttihadmembershipAPI.Business
{
    public class RoleService :IRoleService
    {
        private readonly IRoleModel _roleModel;
        public RoleService(IRoleModel _roleModel)
        {
            this._roleModel = _roleModel;
        }
        public RoleDTO ManageRole(RoleDTO obj)
        {
            return _roleModel.ManageRole(obj);
        }
        public List<RoleDTO> GetRoles(RoleDTO obj)
        {
            return _roleModel.GetRoles(obj);
        }
    }
    public interface IRoleService
    {
        RoleDTO ManageRole(RoleDTO obj);
        List<RoleDTO> GetRoles(RoleDTO obj);

    }
}

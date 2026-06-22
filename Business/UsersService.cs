using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;

namespace IttihadmembershipAPI.Business
{
    public class UsersService : IUsersService
    {
        private readonly IUsersModel _UsersModel;
        
        public UsersService(IUsersModel _UsersModel)
        {
            {
                this._UsersModel = _UsersModel;
               
            }
        }
        public UsersResponseDTO GetUsers(UsersDTO obj)
        {
            return _UsersModel.GetUsers(obj);
        }
    }
    public interface IUsersService
    {
        UsersResponseDTO GetUsers(UsersDTO obj);
    }
}

using IttihadmembershipAPI.Controllers;
using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;

namespace IttihadmembershipAPI.Business
{
    public class UsersService : IUsersService
    {
        private readonly IUsersModel _UsersModel;
        private readonly ILogger<UsersAPIController> _logger;

        public UsersService(IUsersModel _UsersModel, ILogger<UsersAPIController> logger)
        {
            {
                this._UsersModel = _UsersModel;
                _logger = logger;

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

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
            var response = new UsersResponseDTO();

            try
            {
                var data = _UsersModel.getUsers(obj);

                if (data != null && data.HasRows)
                {
                    while (data.Read())
                    {
                        response.Users.Add(new UsersDTO
                        {
                            UserId = Convert.ToInt32(data["UserId"]),
                            UserName = data["UserName"]?.ToString(),
                            EmployeeId = Convert.ToInt32(data["EmployeeId"]),
                            DisplayName = data["DisplayName"]?.ToString(),
                            FullName = data["FullName"]?.ToString(),
                            Email = data["Email"]?.ToString(),
                            MobileNumber = data["MobileNumber"]?.ToString(),
                            RoleId = Convert.ToInt32(data["RoleId"]),
                            IsActive = Convert.ToBoolean(data["IsActive"]),
                            CreatedBy = data["CreatedBy"]?.ToString(),
                            CreatedDate = data["CreatedDate"] != DBNull.Value
                            ? Convert.ToDateTime(data["CreatedDate"])
                            : null,
                        });
                    }

                    response.StatusCode = 1;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = 0;
                    response.Message = "No records found";
                    _logger.LogWarning("No users found for UserId {UserId}",obj.UserId);
                }

                data?.Close();

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred in GetUsers service");
                return new UsersResponseDTO
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
    public interface IUsersService
    {
        UsersResponseDTO GetUsers(UsersDTO obj);
    }
}

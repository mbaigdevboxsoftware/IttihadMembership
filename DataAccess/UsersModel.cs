using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DataAccess
{
    public class UsersModel : IUsersModel
    {
        private readonly ILogger<UsersModel> _logger;
        public UsersModel(ILogger<UsersModel> logger)
        {
            _logger = logger;
        }
        public SqlDataReader getUsers(UsersDTO obj)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@Id",obj.UserId),
                    
                };
                return DbConnector.ExecuteReader("[Admin].[GetUsers]", parameter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Database error while executing Admin.GetUsers");
                throw;
            }
        }
    }
    public interface IUsersModel
    {
        SqlDataReader getUsers(UsersDTO obj);
    }
}

using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DataAccess
{
    public class UsersModel : IUsersModel
    {
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
               
                return null;
            }
        }
    }
    public interface IUsersModel
    {
        SqlDataReader getUsers(UsersDTO obj);
    }
}

using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DataAccess
{
    public class UsersModel : IUsersModel
    {
        public UsersResponseDTO GetUsers(UsersDTO obj)
        {
            var response = new UsersResponseDTO();

            try
            {
                var parameter = new[]
                {
            new SqlParameter("@Id", obj.UserId)
        };

                using SqlDataReader reader =
                    DbConnector.ExecuteReader("[Admin].[GetUsers]", parameter);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        response.Users.Add(new UsersDTO
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            UserName = reader["UserName"]?.ToString(),
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            DisplayName = reader["DisplayName"]?.ToString(),
                            FullName = reader["FullName"]?.ToString(),
                            Email = reader["Email"]?.ToString(),
                            MobileNumber = reader["MobileNumber"]?.ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            CreatedBy = reader["CreatedBy"]?.ToString(),
                            CreatedDate = reader["CreatedDate"] != DBNull.Value
                                ? Convert.ToDateTime(reader["CreatedDate"])
                                : null
                        });
                    }

                    response.StatusCode = 1;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = 0;
                    response.Message = "No records found";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new UsersResponseDTO
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
    public interface IUsersModel
    {
        UsersResponseDTO GetUsers(UsersDTO obj);
    }
}

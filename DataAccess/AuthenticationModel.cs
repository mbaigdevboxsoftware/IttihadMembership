using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using IttihadmembershipAPI.GenericHelper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IttihadmembershipAPI.DataAccess
{
    public class AuthenticationModel : IAuthenticationModel
    {
        public UserDTO Authentication(string Username)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@UserName",Username),
                   
                };
                using (var result = DbConnector.ExecuteReader("[Admin].[uspValidateCredentials]", parameter))
                {
                    var list = CustomDataReaderToGenericExtension.GetDataObjects<UserDTO>(result);

                    if (list.Count > 0)
                        return list.First();  
                    else
                        return null;          
                }
            }
            catch (Exception ex)
            {
                //DataModelExceptionUtility.LogException(ex, "UsersModel -> Authentication");
                //DataModelExceptionUtility.NotifySystemOperators(ex);
                return null;
            }
        }
        public CommonDTO Register(RegisterDTO request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@UserName", request.UserName),
                    new SqlParameter("@Id", request.UserId),
                    new SqlParameter("@PasswordHash", request.Password),
                    new SqlParameter("@EmployeeId", request.EmployeeId),
                    new SqlParameter("@DisplayName", request.DisplayName),
                    new SqlParameter("@FullName", request.FullName),
                    new SqlParameter("@Email", request.Email),
                    new SqlParameter("@MobileNumber", request.MobileNumber),
                    new SqlParameter("@RoleId", request.RoleId),
                    new SqlParameter("@CreatedBy", request.CreatedBy),
                    new SqlParameter("@ModifiedBy", request.ModifiedBy),
                    new SqlParameter("@IsActive", request.IsActive),
                    new SqlParameter("@FlagId", request.FlagId)
                };

                using SqlDataReader reader =
                    DbConnector.ExecuteReader("Admin.uspRegisterAdmin", parameters);

                if (reader.Read())
                {
                    return new CommonDTO
                    {
                        StatusCode = Convert.ToInt32(reader["StatusCode"]),
                        Message = reader["Message"]?.ToString() ?? string.Empty
                    };
                }

                return new CommonDTO
                {
                    StatusCode = 500,
                    Message = "No response from database."
                };
            }
            catch (Exception ex)
            {
                return new CommonDTO
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
        public void SaveRefreshToken(int EmployeeId, string refreshToken,DateTime expiryDate)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@EmployeeId", EmployeeId),
                    new SqlParameter("@Token", refreshToken),
                    new SqlParameter("@ExpiryDate", expiryDate)
                };

                DbConnector.ExecuteNonQuery("[Admin].[uspSaveRefreshToken]", parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public RefreshTokenDTO GetRefreshToken(string token)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Token", token)
                };

                using (var result =
                    DbConnector.ExecuteReader("[Admin].[uspGetRefreshToken]", parameters))
                {
                    var list =CustomDataReaderToGenericExtension.GetDataObjects<RefreshTokenDTO>(result);

                    return list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void RevokeRefreshToken(string token)
        {
            var parameters = new[]
            {
                new SqlParameter("@Token", token)
            };

            DbConnector.ExecuteNonQuery("[Admin].[uspRevokeRefreshToken]", parameters);
        }
        public UserDTO GetUserById(int employeeId)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@EmployeeId", employeeId)
                };

                using (var result =
                    DbConnector.ExecuteReader("[Admin].[uspGetUserById]", parameters))
                {
                    var list =
                        CustomDataReaderToGenericExtension
                            .GetDataObjects<UserDTO>(result);

                    return list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    public interface IAuthenticationModel
    {
        UserDTO Authentication(string Username);
        CommonDTO Register(RegisterDTO request);

        void SaveRefreshToken(int userId,string refreshToken,DateTime expiryDate);
        RefreshTokenDTO GetRefreshToken(string token);
        void RevokeRefreshToken(string token);
        UserDTO GetUserById(int userId);
    }
}

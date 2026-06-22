using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using IttihadmembershipAPI.GenericHelper;
using Microsoft.Data.SqlClient;
namespace IttihadmembershipAPI.DataAccess

{
    public class WebsiteModel : IWebsiteModel
    {
        public UserDTO UserAuthentication(string Username)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@UserName",Username),

                };
                using (var result = DbConnector.ExecuteReader("[Admin].[uspValidateUserCredentials]", parameter))
                {
                    var list = CustomDataReaderToGenericExtension.GetDataObjects<UserDTO>(result);

                    if (list.Count > 0)
                        return list.First();  // return the user info directly
                    else
                        return null;          // no user found
                }
            }
            catch (Exception ex)
            {
                //DataModelExceptionUtility.LogException(ex, "UsersModel -> Authentication");
                //DataModelExceptionUtility.NotifySystemOperators(ex);
                return null;
            }
        }
        public CommonDTO UserRegister(WebsiteDTO request)
        {
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@FirstName", request.FirstName),
            new SqlParameter("@LastName", request.LastName),
            new SqlParameter("@Gender", request.Gender),
            new SqlParameter("@DOB", request.DOB),
            new SqlParameter("@PasswordHash", request.Password),
            new SqlParameter("@NationalId", request.NationalId),
            new SqlParameter("@EmailId", request.Email),
            new SqlParameter("@MobileNo", request.MobileNo),
            new SqlParameter("@CreatedBy", request.CreatedBy),
            new SqlParameter("@ModifiedBy", request.ModifiedBy),
            new SqlParameter("@IsActive", request.IsActive)
        };

                using SqlDataReader reader =
                    DbConnector.ExecuteReader("[Common].[uspRegisterUser]", parameters);

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
        public SqlDataReader GetNationality(NationalityDTO obj)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@Id",obj.Id),

                };
                return DbConnector.ExecuteReader("[Admin].[GetNationality]", parameter);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
    public interface IWebsiteModel
    {
        UserDTO UserAuthentication(string Username);
        CommonDTO UserRegister(WebsiteDTO request);
        SqlDataReader GetNationality(NationalityDTO obj);

    }
}

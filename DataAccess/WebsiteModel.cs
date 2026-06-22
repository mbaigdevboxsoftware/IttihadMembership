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
        public NationalityResponseDTO GetNationality(NationalityDTO obj)
        {
            var response = new NationalityResponseDTO();

            try
            {
                var parameter = new[]
                {
            new SqlParameter("@Id", obj.Id)
        };

                using SqlDataReader data =
                    DbConnector.ExecuteReader("[Admin].[GetNationality]", parameter);

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        response.Nationality.Add(new NationalityDTO
                        {
                            Id = Convert.ToInt32(data["Id"]),

                            NameEn = data["NameEn"]?.ToString(),
                            NameAr = data["NameAr"]?.ToString()


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
                return new NationalityResponseDTO
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
        public PackageResponseDTO WebsitePackages(PackageDTO obj)
        {
            var response = new PackageResponseDTO();

            try
            {
                var parameter = new[]
                {
            new SqlParameter("@Id", obj.Id)
        };

                using SqlDataReader data =
                    DbConnector.ExecuteReader("[Common].[WebsitePackages]", parameter);

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        response.Packages.Add(new PackageDTO
                        {
                            Id = Convert.ToInt32(data["PakageID"]),
                            MembershipId = Convert.ToInt32(data["MembershipID"]),
                            Price = Convert.ToInt32(data["Price"]),
                            StartDate = data["StartDate"] != DBNull.Value
                               ? DateOnly.FromDateTime((DateTime)data["StartDate"])
                               : null,
                            EndDate = data["EndDate"] != DBNull.Value
                               ? DateOnly.FromDateTime((DateTime)data["EndDate"])
                               : null,
                            MembershipName = data["MembershipName"]?.ToString(),
                            IsActive = Convert.ToBoolean(data["IsActive"]),

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
                return new PackageResponseDTO
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

    }
    public interface IWebsiteModel
    {
        UserDTO UserAuthentication(string Username);
        CommonDTO UserRegister(WebsiteDTO request);
        NationalityResponseDTO GetNationality(NationalityDTO obj);
        PackageResponseDTO WebsitePackages(PackageDTO obj);

    }
}

using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DataAccess
{
    public class PackageModel:IPackageModel
    {
        public PackageDTO NewPackage(PackageDTO obj)
        {
            try
            {
                var parameter = new[]
               {
                    new SqlParameter("@Id",obj.Id),
                    new SqlParameter("@Price",obj.Price),
                    new SqlParameter("@MembershipId",obj.MembershipId),
                    new SqlParameter("@StartDate",obj.StartDate),
                    new SqlParameter("@EndDate",obj.EndDate),
                    new SqlParameter("@IsActive",obj.IsActive),
                    new SqlParameter("@FlagId",obj.FlagId),
                    new SqlParameter("@CreatedBy",obj.CreatedBy),
                    new SqlParameter("@ModifiedBy",obj.ModifiedBy)
                };

                using SqlDataReader reader =
                    DbConnector.ExecuteReader("[Admin].[ManagePackages]", parameter);

                if (reader.Read())
                {
                    obj.StatusCode = Convert.ToInt32(reader["StatusCode"]);
                    obj.Message = reader["Message"]?.ToString() ?? string.Empty;
                }
                else
                {
                    obj.StatusCode = 500;
                    obj.Message = "No response from database.";
                }

                return obj;
            }
            catch (Exception ex)
            {
                return new PackageDTO
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
        
        public PackageResponseDTO GetPackages(PackageDTO obj)
        {
            var response = new PackageResponseDTO();

            try
            {
                var parameter = new[]
                {
            new SqlParameter("@Id", obj.Id)
        };

                using SqlDataReader data =
                    DbConnector.ExecuteReader("[Admin].[GetPackages]", parameter);

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
    public interface IPackageModel {
        PackageDTO NewPackage(PackageDTO obj);
        PackageResponseDTO GetPackages(PackageDTO obj);
    }
}

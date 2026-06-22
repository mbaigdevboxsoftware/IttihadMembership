using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DataAccess
{
    public class MemberShipModel: IMemberShipModel
    {
        public MemberShipDTO ManageMembership(MemberShipDTO obj)
        {
            try
            {
                var parameter = new[]
                {
            new SqlParameter("@Id", obj.Id),
            new SqlParameter("@DurationDays", obj.DurationDays),
            new SqlParameter("@MembershipName", obj.MembershipName),
            new SqlParameter("@Description", obj.Description),
            new SqlParameter("@IsActive", obj.IsActive),
            new SqlParameter("@FlagId", obj.FlagId),
            new SqlParameter("@CreatedBy", obj.CreatedBy),
            new SqlParameter("@ModifiedBy", obj.ModifiedBy)
        };

                using SqlDataReader reader =
                    DbConnector.ExecuteReader("[Admin].[ManageMembership]", parameter);

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
                return new MemberShipDTO
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public MemberShipResponseDTO GetMemberships(MemberShipDTO obj)
        {
            var response = new MemberShipResponseDTO();

            try
            {
                var parameter = new[]
                {
            new SqlParameter("@Id", obj.Id)
        };

                using SqlDataReader data =
                    DbConnector.ExecuteReader("[Admin].[GetMemberships]", parameter);

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        response.Members.Add(new MemberShipDTO
                        {
                            Id = Convert.ToInt32(data["MembershipID"]),
                            DurationDays = Convert.ToInt32(data["DurationDays"]),
                            MembershipName = data["MembershipName"]?.ToString(),
                            Description = data["Description"]?.ToString(),
                            IsActive = Convert.ToBoolean(data["IsActive"]),
                            CreatedBy = Convert.ToInt32(data["CreatedBy"]),
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
                }

                return response;
            }
            catch (Exception ex)
            {
                return new MemberShipResponseDTO
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
    public interface IMemberShipModel
    {

        MemberShipDTO ManageMembership(MemberShipDTO obj);
        MemberShipResponseDTO GetMemberships(MemberShipDTO obj);
    }

}

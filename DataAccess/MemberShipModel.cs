using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DataAccess
{
    public class MemberShipModel: IMemberShipModel
    {
        public SqlDataReader ManageMembership(MemberShipDTO obj)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@Id",obj.Id),
                    new SqlParameter("@DurationDays",obj.DurationDays),
                    new SqlParameter("@MembershipName",obj.MembershipName),
                    new SqlParameter("@Description",obj.Description),
                    new SqlParameter("@IsActive",obj.IsActive),
                    new SqlParameter("@FlagId",obj.FlagId),
                    new SqlParameter("@CreatedBy",obj.CreatedBy),
                    new SqlParameter("@ModifiedBy",obj.ModifiedBy)
                };
                return DbConnector.ExecuteReader("[Admin].[ManageMembership]", parameter);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public SqlDataReader GetMemberships(MemberShipDTO obj)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@Id",obj.Id),

                };
                return DbConnector.ExecuteReader("[Admin].[GetMemberships]", parameter);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
    public interface IMemberShipModel
    {

        SqlDataReader ManageMembership(MemberShipDTO obj);
        SqlDataReader GetMemberships(MemberShipDTO obj);
    }

}

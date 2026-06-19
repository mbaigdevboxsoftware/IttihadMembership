using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DataAccess
{
    public class PackageModel:IPackageModel
    {
        public SqlDataReader NewPackage(PackageDTO obj)
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
                return DbConnector.ExecuteReader("[Admin].[ManagePackages]", parameter);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public SqlDataReader GetPackages(PackageDTO obj)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@Id",obj.Id),

                };
                return DbConnector.ExecuteReader("[Admin].[GetPackages]", parameter);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
    public interface IPackageModel {
        SqlDataReader NewPackage(PackageDTO obj);
        SqlDataReader GetPackages(PackageDTO obj);
    }
}

using IttihadmembershipAPI.DbConnection;
using IttihadmembershipAPI.DTO_s;
using IttihadmembershipAPI.GenericHelper;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DataAccess
{
    public class RoleModel:IRoleModel
    {
        public RoleDTO ManageRole(RoleDTO obj) 
        {
            try 
            {
                var parameter = new[]
                {
                    new SqlParameter("@Id",obj.Id),
                    new SqlParameter("@FlagId",obj.FlagId),
                    new SqlParameter("@RoleName",obj.RoleName),
                    new SqlParameter("Description",obj.Description),


                };
                using SqlDataReader reader = DbConnector.ExecuteReader("[Admin].[ManageRoles]", parameter);
                if(reader.Read())
                {
                    obj.StatusCode = Convert.ToInt32(reader["StatusCode"]);
                    obj.Message = reader["Message"]?.ToString() ?? string.Empty;
                }
                else
                {
                    obj.StatusCode = 500;
                    obj.Message = "No response from database";
                }
                return obj;
            }
            catch (Exception ex)
            {
                return new RoleDTO
                {
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }
        public List<RoleDTO> GetRoles(RoleDTO obj)
        {
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@Id", obj.RoleID)
        };

                using (var result =
                    DbConnector.ExecuteReader("[Admin].[GetRoles]", parameters))
                {
                    return CustomDataReaderToGenericExtension.GetDataObjects<RoleDTO>(result).ToList();
                }
            }
            catch
            {
                return new List<RoleDTO>();
            }
        }
    }
    public interface IRoleModel
    {
        RoleDTO ManageRole(RoleDTO obj);
        List<RoleDTO> GetRoles(RoleDTO obj);
    }
}

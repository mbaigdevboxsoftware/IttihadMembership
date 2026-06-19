using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;

namespace IttihadmembershipAPI.Business
{
    public class PackageService: IPackageService
    {
        private readonly IPackageModel _PackageModel;

        public PackageService(IPackageModel _PackageModel)
        {
            {
                this._PackageModel = _PackageModel;

            }

        }
        public PackageDTO NewPackage(PackageDTO obj)
        {
            try
            {
                var data = _PackageModel.NewPackage(obj);

                if (data != null && data.HasRows)
                {
                    while (data.Read())
                    {
                        obj.Message = data["Message"].ToString();
                        obj.StatusCode = Convert.ToInt32(data["StatusCode"]);
                    }
                    data.Close();
                }

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public PackageResponseDTO GetPackages(PackageDTO obj)
        {
            var response = new PackageResponseDTO();

            try
            {
                var data = _PackageModel.GetPackages(obj);

                if (data != null && data.HasRows)
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

                data?.Close();

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
    public interface IPackageService
    {
        PackageDTO NewPackage(PackageDTO obj);
        PackageResponseDTO GetPackages(PackageDTO obj);
    }
}

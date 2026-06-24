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
        
        public PackageDTO NewPackage(PackageDTO obj, int userId)
        {
            obj.CreatedBy = userId;
            obj.ModifiedBy = userId;
            return _PackageModel.NewPackage(obj);
        }
        public PackageResponseDTO GetPackages(PackageDTO obj)
        {
            return _PackageModel.GetPackages(obj);
        }
       

    }
    public interface IPackageService
    {
        PackageDTO NewPackage(PackageDTO obj ,int userId);
        PackageResponseDTO GetPackages(PackageDTO obj);
    }
}

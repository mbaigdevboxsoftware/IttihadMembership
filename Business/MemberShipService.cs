using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;

namespace IttihadmembershipAPI.Business
{
    public class MemberShipService : IMemberShipService
    {
        private readonly IMemberShipModel _MemberShipModel;
        private readonly IJwtTokenService _jwtTokenService;
        public MemberShipService(IMemberShipModel _MemberShipModel, IJwtTokenService jwtTokenService)
        {
            {
                this._MemberShipModel = _MemberShipModel;
                _jwtTokenService = jwtTokenService;
            }
        }
        public MemberShipDTO ManageMembership(MemberShipDTO obj)
        {
            return _MemberShipModel.ManageMembership(obj);
        }


        public MemberShipResponseDTO GetMemberships(MemberShipDTO obj)
        {
            return _MemberShipModel.GetMemberships(obj);
        }

    }

    public interface IMemberShipService
    {
        MemberShipDTO ManageMembership(MemberShipDTO obj);
        MemberShipResponseDTO GetMemberships(MemberShipDTO obj);
    }
}

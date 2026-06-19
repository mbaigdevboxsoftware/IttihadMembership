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
            try
            {
                var data = _MemberShipModel.ManageMembership(obj);

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
        public MemberShipResponseDTO GetMemberships(MemberShipDTO obj)
        {
            var response = new MemberShipResponseDTO();

            try
            {
                var data = _MemberShipModel.GetMemberships(obj);

                if (data != null && data.HasRows)
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

                data?.Close();

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

    public interface IMemberShipService
    {
        MemberShipDTO ManageMembership(MemberShipDTO obj);
        MemberShipResponseDTO GetMemberships(MemberShipDTO obj);
    }
}

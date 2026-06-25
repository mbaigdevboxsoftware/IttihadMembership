using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;
using IttihadmembershipAPI.GenericHelper;

namespace IttihadmembershipAPI.Business
{
    public class WebsiteService :IWebsiteService
    {
        private readonly IWebsiteModel _WebsiteModel;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAuthenticationModel _AuthenticationModel;
        public WebsiteService(IWebsiteModel _WebsiteModel, IJwtTokenService jwtTokenService, IAuthenticationModel _AuthenticationModel)
        {
            {
                this._WebsiteModel = _WebsiteModel;
                _jwtTokenService = jwtTokenService;
                this._AuthenticationModel = _AuthenticationModel;
            }
        }
              public async Task<AuthResponseDTO> UserAuthentication(LoginRequestDTO request)
        {
            var user = _WebsiteModel.UserAuthentication(request.Username);

            if (user == null)
                return null;

            bool isValidPassword =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!isValidPassword)
                return null;

            var authResponse = _jwtTokenService.GenerateToken(user);

            authResponse.MemberId = user.MemberID;
             authResponse.FullName = user.FullName;
            authResponse.MobileNo = user.MobileNo;
            authResponse.EmailId = user.EmailId;

            _AuthenticationModel.SaveRefreshToken(
                authResponse.MemberId,
                authResponse.RefreshToken,
                DateTime.UtcNow.AddDays(30));


            return authResponse;
        
        }
        public Task<CommonDTO> UserRegister(WebsiteDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Password = passwordHash;

            return Task.FromResult(_WebsiteModel.UserRegister(request));
        }
        public NationalityResponseDTO GetNationality(NationalityDTO obj)
        {
            return _WebsiteModel.GetNationality(obj);
        }
        public PackageResponseDTO WebsitePackages(PackageDTO obj)
        {
            return _WebsiteModel.WebsitePackages(obj);
        }

        public List<WebsiteDTO> GetUsers(WebsiteDTO obj)
        {
            return _WebsiteModel.GetUsers(obj);
        }

        public CheckPasswordDTO CheckPassword(CheckPasswordDTO obj)
        {
            var member = _WebsiteModel.CheckPassword(obj).FirstOrDefault();

            if (member == null)
            {
                obj.StatusCode = 0;
                obj.Message = "Member not found";
                return obj;
            }

            // Validate old password
            if (!BCryption.CheckPassword(obj.OldPassword, member.Password))
            {
                obj.StatusCode = 0;
                obj.Message = "Old password is incorrect";
                return obj;
            }

            // Validate new password is different
            if (BCryption.CheckPassword(obj.NewPassword, member.Password))
            {
                obj.StatusCode = 0;
                obj.Message = "New password cannot be same as old password";
                return obj;
            }

            obj.StatusCode = 1;
            obj.Message = "Validation successful";

            return obj;
        }
        public CommonDTO ChangePassword(CheckPasswordDTO obj)
        {
            var response = new CommonDTO();

            try
            {
                // Hash new password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(obj.NewPassword);

                obj.NewPassword = hashedPassword;

                response = _WebsiteModel.ChangePassword(obj);

                return response;
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
        public PaymentDTO PaymentDetails(PaymentDTO obj)
        {
            return _WebsiteModel.PaymentDetails(obj);
        }

    }
    
    public interface IWebsiteService
    {
        Task<AuthResponseDTO> UserAuthentication(LoginRequestDTO request);
        Task<CommonDTO> UserRegister(WebsiteDTO request);
        NationalityResponseDTO GetNationality(NationalityDTO obj);
        PackageResponseDTO WebsitePackages(PackageDTO obj);
        List<WebsiteDTO> GetUsers(WebsiteDTO obj);

        CheckPasswordDTO CheckPassword(CheckPasswordDTO obj);
        CommonDTO ChangePassword(CheckPasswordDTO obj);

        PaymentDTO PaymentDetails(PaymentDTO obj);
    }
}

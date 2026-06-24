using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;

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

    }
    
    public interface IWebsiteService
    {
        Task<AuthResponseDTO> UserAuthentication(LoginRequestDTO request);
        Task<CommonDTO> UserRegister(WebsiteDTO request);
        NationalityResponseDTO GetNationality(NationalityDTO obj);
        PackageResponseDTO WebsitePackages(PackageDTO obj);
    }
}

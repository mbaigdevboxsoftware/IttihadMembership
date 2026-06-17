using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;

namespace IttihadmembershipAPI.Business
{
    public class WebsiteService :IWebsiteService
    {
        private readonly IWebsiteModel _WebsiteModel;
        private readonly IJwtTokenService _jwtTokenService;
        public WebsiteService(IWebsiteModel _WebsiteModel, IJwtTokenService jwtTokenService)
        {
            {
                this._WebsiteModel = _WebsiteModel;
                _jwtTokenService = jwtTokenService;
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

            return _jwtTokenService.GenerateToken(user);
        }
        public Task<CommonDTO> UserRegister(WebsiteDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Password = passwordHash;

            return Task.FromResult(_WebsiteModel.UserRegister(request));
        }
    }
    
    public interface IWebsiteService
    {
        Task<AuthResponseDTO> UserAuthentication(LoginRequestDTO request);
        Task<CommonDTO> UserRegister(WebsiteDTO request);
    }
}

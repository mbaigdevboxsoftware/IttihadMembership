using IttihadmembershipAPI.DataAccess;
using IttihadmembershipAPI.DTO_s;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;

namespace IttihadmembershipAPI.Business
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationModel _AuthenticationModel;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthenticationService(IAuthenticationModel _AuthenticationModel, IJwtTokenService jwtTokenService)
        {
            {
                this._AuthenticationModel = _AuthenticationModel;
                _jwtTokenService = jwtTokenService;
            }
        }

        public async Task<AuthResponseDTO> Authentication(LoginRequestDTO request)
        {
            var user = _AuthenticationModel.Authentication(request.Username);

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
        public Task<CommonDTO> Register(RegisterDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Password = passwordHash;

            return Task.FromResult(_AuthenticationModel.Register(request));
        }
    }

    public interface IAuthenticationService
    {
        Task<AuthResponseDTO> Authentication(LoginRequestDTO request);
        Task<CommonDTO> Register(RegisterDTO request);

    }
}

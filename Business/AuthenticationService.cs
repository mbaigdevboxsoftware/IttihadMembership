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

            var authResponse =_jwtTokenService.GenerateToken(user);

            _AuthenticationModel.SaveRefreshToken(
                user.EmployeeId,
                authResponse.RefreshToken,
                DateTime.UtcNow.AddDays(30));

            return authResponse;
        }

        public Task<CommonDTO> Register(RegisterDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Password = passwordHash;

            return Task.FromResult(_AuthenticationModel.Register(request));
        }

        public AuthResponseDTO RefreshToken(string refreshToken)
        {
            var storedToken =
                _AuthenticationModel.GetRefreshToken(refreshToken);

            if (storedToken == null)
                return null;

            if (storedToken.IsRevoked)
                return null;

            if (storedToken.ExpiryDate <= DateTime.UtcNow)
                return null;

            var user =_AuthenticationModel.GetUserById(storedToken.EmployeeId);

            if (user == null)
                return null;

            // Optional SECURITY UPGRADE: rotate token
            _AuthenticationModel.RevokeRefreshToken(refreshToken);

            var newAuth =
                _jwtTokenService.GenerateToken(user);

            // save new refresh token
            _AuthenticationModel.SaveRefreshToken(
                user.EmployeeId,
                newAuth.RefreshToken,
                DateTime.UtcNow.AddDays(30));

            return newAuth;
        }
    }

    public interface IAuthenticationService
    {
        Task<AuthResponseDTO> Authentication(LoginRequestDTO request);
        Task<CommonDTO> Register(RegisterDTO request);
        AuthResponseDTO RefreshToken(string refreshToken);

    }
}

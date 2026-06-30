using IttihadmembershipAPI.DTO_s;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }
    public AuthResponseDTO GenerateToken(UserDTO user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.EmployeeId.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expiry = DateTime.UtcNow.AddMinutes(
            Convert.ToInt32(_configuration["Jwt:ExpiryMinutes"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: credentials);

        var accessToken =
            new JwtSecurityTokenHandler().WriteToken(token);

        var refreshToken = GenerateRefreshToken();

        return new AuthResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiry,
           
        };
    }

}
public interface IJwtTokenService
{
    AuthResponseDTO GenerateToken(UserDTO user);
}
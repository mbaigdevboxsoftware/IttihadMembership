using System.ComponentModel.DataAnnotations;

namespace IttihadmembershipAPI.DTO_s
{
    public class UserDTO
    {
        public int EmployeeId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
    public class LoginRequestDTO
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
    public class AuthResponseDTO
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }
    }

    public class RegisterDTO()
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int EmployeeId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string DisplayName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int RoleId { get; set; } = 0;    
        public int CreatedBy { get; set; } = 0;
        public int ModifiedBy { get; set; } = 0;
        public int FlagId { get; set; } = 0;


    }
    public class RefreshTokenDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class RefreshTokenRequestDTO
    {
        public string RefreshToken { get; set; }
    }
}

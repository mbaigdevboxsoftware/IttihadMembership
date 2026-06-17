namespace IttihadmembershipAPI.DTO_s
{
    public class UsersDTO
    {
        public int UserId { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public int EmployeeId { get; set; } = 0;
        public string DisplayName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public int RoleId { get; set; } = 0;
        public bool IsActive { get; set; } = false;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
    }

    public class UsersResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<UsersDTO> Users { get; set; } = new();
    }
}

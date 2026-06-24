namespace IttihadmembershipAPI.DTO_s
{
    public class RoleDTO:CommonDTO
    {
        public int Id { get; set; } = 0;
        public int RoleID { get; set; } = 0;
        public string RoleName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
    public class RoleResponseDTO
    {
        public List<RoleDTO> Roles { get; set; } = new();
        public int StatusCode { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
    }
}

namespace IttihadmembershipAPI.DTO_s
{
    public class PackageDTO:CommonDTO
    {
        public int MembershipId { get; set; } = 0;
        public string MembershipName { get; set; } = string.Empty;
        public int Id { get; set; } = 0;
        public int Price { get; set; } = 0;

        public DateOnly? StartDate { get; set; } 
        public DateOnly? EndDate { get; set;} 
       

    }
    public class PackageResponseDTO
    {
        public List<PackageDTO> Packages { get; set; } = new();
        public int StatusCode { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
    }
}

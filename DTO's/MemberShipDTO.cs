namespace IttihadmembershipAPI.DTO_s
{
    public class MemberShipDTO :CommonDTO
    {
        public int Id { get; set; } = 0;
        public int DurationDays { get; set; } = 0;
        public string MembershipName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        
        
       


    }
    public class MemberShipResponseDTO  
    {
        public List<MemberShipDTO> Members { get; set; } = new();
            public int StatusCode { get; set; } = 0;
            public string Message { get; set; } = string.Empty;
    }
}

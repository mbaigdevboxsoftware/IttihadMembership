namespace IttihadmembershipAPI.DTO_s
{
    public class CommonDTO
    {
        public int StatusCode { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public int CreatedBy { get; set; } = 0;
        public int FlagId { get; set; } = 0;

        public int ModifiedBy { get; set; } = 0;
        public DateTime? CreatedDate { get; set; }

    }
}

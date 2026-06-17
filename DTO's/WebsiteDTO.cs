namespace IttihadmembershipAPI.DTO_s
{
    public class WebsiteDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public int NationalId { get; set; }
        public string Nationality { get; set; }
        public string Password { get; set; }
  
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; } = 0;
        public int ModifiedBy { get; set; } = 0;

    }
}

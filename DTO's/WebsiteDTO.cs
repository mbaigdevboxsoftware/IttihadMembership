namespace IttihadmembershipAPI.DTO_s
{
    public class WebsiteDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public int NationalId { get; set; } = 0;
       // public string Nationality { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
  
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public int CreatedBy { get; set; } = 0; 
        public int ModifiedBy { get; set; } = 0;

    }
    public class NationalityDTO
    {
        public int Id { get; set; } = 0;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
       



    }
    public class NationalityResponseDTO
    {
        public List<NationalityDTO> Nationality { get; set; } = new();
        public int StatusCode { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
    }
}

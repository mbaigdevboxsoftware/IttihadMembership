namespace IttihadmembershipAPI.DTO_s
{
    public class WebsiteDTO:CommonDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public int NationalId { get; set; } = 0;
        public int MemberID { get; set; } = 0;
        public string Nationality { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
  
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public int CreatedBy { get; set; } = 0; 
        public int Id { get; set; } = 0; 
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
    public class CheckPasswordDTO
    {
        public int MemberId { get; set; } = 0;

        public string OldPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;

        public int StatusCode { get; set; } = 0;

        public string Message { get; set; } = string.Empty;
    }

    public class PaymentDTO:CommonDTO
    {
        public int SubscriptionId { get; set; } = 0;
        public int ID { get; set; } = 0;

        public decimal TransactionAmount { get; set; }
        public int FirstDigits { get; set; } = 0;
        public int LastDigits { get; set; } = 0;
        public int ExpireMonth { get; set; } = 0;
        public int ExpireYear { get; set; } = 0;
        public string CardHolder { get; set; } = string.Empty;
        public string CardBrand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MembershipName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Invoice { get; set; } = string.Empty;

    }
}

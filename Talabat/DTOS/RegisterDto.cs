using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.DTOS
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()-_=+{};:,<.>]).{8,}$", ErrorMessage = "password must contain uppercase and 1 lowercase and 1 Digit and 1 special character")]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone] 
        public string PhoneNumber {  get; set; }
    }
}

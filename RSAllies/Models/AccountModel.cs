using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RSAllies.Models
{
    public class AccountModel
    {
        [Phone]
        [Required(ErrorMessage = "Namba ya Simu ni muhimu")]
        [DisplayName("Phone Number")]
        [MinLength(10, ErrorMessage = "Phone number must be 10 digits long")]
        [MaxLength(10, ErrorMessage = "Phone number must be 10 digits long")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits long")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Neno la Siri ni muhimu")]
        [PasswordPropertyText]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(12, ErrorMessage = "Password must be less than 12 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rudia tena neno la siri")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}

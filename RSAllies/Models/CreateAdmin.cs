using System.ComponentModel.DataAnnotations;

namespace RSAllies.Models
{
    public class CreateAdmin
    {
        [Required(ErrorMessage = "Please provide your first name")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First name must contain only letters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide your last name")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name must contain only letters")]
        public string LastName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide your phone number")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits long")]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide your email")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide your password")]
        [Display(Name = "Password")]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{12,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a role")]
        public Guid RoleId { get; set; }
    }
}

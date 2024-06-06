using System.ComponentModel.DataAnnotations;

namespace RSAllies.Models.English
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your middle name.")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your identification number.")]
        public string Identification { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your address.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your date of birth.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select an option.")]
        public bool IsForeigner { get; set; }

        [Required(ErrorMessage = "Please select your gender.")]
        public Guid GenderId { get; set; }

        [Required(ErrorMessage = "Please select your education level.")]
        public Guid EducationLevelId { get; set; }

        [Required(ErrorMessage = "Please select your license class.")]
        public Guid LicenseClassId { get; set; }
    }
}

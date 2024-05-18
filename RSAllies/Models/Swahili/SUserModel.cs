namespace RSAllies.Models.Swahili
{
    public class SUserModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool IsForeigner { get; set; }
        public Guid GenderId { get; set; }
        public Guid EducationLevelId { get; set; }
        public Guid LicenseClassId { get; set; }
    }
}

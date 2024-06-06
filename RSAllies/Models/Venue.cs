using System.ComponentModel.DataAnnotations;

namespace RSAllies.Models
{
    public class Venue
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Range(50, 2000, MinimumIsExclusive = true, MaximumIsExclusive = true, ErrorMessage = "The capacity should be from 50 to 2000")]
        [RegularExpression(@"^\d{2,4}$", ErrorMessage = "The capacity should be a number")]
        public int Capacity { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        [Required]
        public Guid DistrictId { get; set; }

        public IFormFile ImageUrl { get; set; } = default!;
    }
}

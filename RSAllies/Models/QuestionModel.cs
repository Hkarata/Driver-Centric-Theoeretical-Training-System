using System.ComponentModel.DataAnnotations;

namespace RSAllies.Models
{
    public class QuestionModel
    {
        [Required]
        public string Scenario { get; set; } = string.Empty;

        [Required]
        public string? ImageUrl { get; set; } = string.Empty;

        [Required]
        public string Question { get; set; } = string.Empty;

        [Required]
        public bool IsEnglish { get; set; }

        [Required]
        public List<String>? Choices { get; set; }
    }
}

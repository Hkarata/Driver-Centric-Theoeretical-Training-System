using System.ComponentModel.DataAnnotations;

namespace RSAllies.Models
{
    public class QuestionModel
    {
        [Required(ErrorMessage = "Please provide the question scenario")]
        public string Scenario { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string Question { get; set; } = string.Empty;

        [Required]
        public bool IsEnglish { get; set; }

        public String ChoiceA { get; set; } = string.Empty;

        public String ChoiceB { get; set; } = string.Empty;

        public String ChoiceC { get; set; } = string.Empty;

        public String ChoiceD { get; set; } = string.Empty;

        public String Answer { get; set; } = string.Empty;
    }
}

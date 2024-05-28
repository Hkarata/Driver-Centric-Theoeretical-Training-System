using System.ComponentModel.DataAnnotations;

namespace RSAllies.Models
{
    public class QuestionModel
    {
        [Required(ErrorMessage = "Please provide the question scenario")]
        public string Scenario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide an image")]
        public IFormFile Image { get; set; } = default!;

        [Required(ErrorMessage = "Please provide a question")]
        public string Question { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please specify the language")]
        public bool IsEnglish { get; set; }

        [Required(ErrorMessage = "Please provide a choice")]
        public String ChoiceA { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a choice")]
        public String ChoiceB { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a choice")]
        public String ChoiceC { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a choice")]
        public String ChoiceD { get; set; } = string.Empty;
    }
}

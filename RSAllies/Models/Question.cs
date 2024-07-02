namespace RSAllies.Models
{
    public class Question
    {
        public Guid Id { get; init; }
        public string Scenario { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public string QuestionText { get; init; } = string.Empty;
        public bool IsEnglish { get; init; }
        public List<Choice>? Choices { get; init; }

    }
}

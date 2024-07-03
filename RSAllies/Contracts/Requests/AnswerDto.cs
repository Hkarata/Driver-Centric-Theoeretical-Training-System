namespace RSAllies.Contracts.Requests
{
	public class AnswerDto
	{
		public Guid QuestionId { get; set; }
		public Guid ChoiceId { get; set; }
	}
}

namespace RSAllies.Contracts.Requests
{
	public class UserResponseDto
	{
		public Guid UserId { get; set; }
		public List<AnswerDto>? Answers { get; set; }

	}
}

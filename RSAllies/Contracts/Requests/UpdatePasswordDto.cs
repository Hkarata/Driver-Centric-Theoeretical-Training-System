namespace RSAllies.Contracts.Requests
{
    public class UpdatePasswordDto
    {
        public Guid UserId { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}

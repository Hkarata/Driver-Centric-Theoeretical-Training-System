namespace RSAllies.Contracts.Requests
{
    public record AdminLogin
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}

namespace RSAllies.Responses
{
    public record AccountCheckResult
    {
        public bool PhoneNumberExists { get; set; }
        public bool EmailExists { get; set; }
    }
}

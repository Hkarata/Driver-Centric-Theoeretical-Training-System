namespace RSAllies.Contracts.Requests
{
    public record CreateBookingDto
    {
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
    }
}

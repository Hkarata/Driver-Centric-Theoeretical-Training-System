namespace RSAllies.Contracts.Responses
{
    public record BookingDto
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public DateTime BookedAt { get; set; }
        public BookingStatus Status { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string VenueAddress { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }

    public enum BookingStatus
    {
        Booked,
        Paid,
        Confirmed,
        Cancelled
    }
}

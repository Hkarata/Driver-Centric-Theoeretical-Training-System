namespace RSAllies.Contracts.Requests
{
    public record struct CreateSessionDto(
        DateTime Date,
        TimeOnly StartTime,
        TimeOnly EndTime,
        Guid VenueId
        );
}

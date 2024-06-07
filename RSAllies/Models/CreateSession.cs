namespace RSAllies.Models
{
    public class CreateSession
    {
        public Guid VenueId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}

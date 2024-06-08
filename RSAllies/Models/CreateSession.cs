using System.ComponentModel.DataAnnotations;

namespace RSAllies.Models
{
    public class CreateSession
    {
        public Guid VenueId { get; set; }

        [Required(ErrorMessage = "Please provide the date for the session")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please provide the start time for the session")]
        [DataType(DataType.Time)]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "Please provide the end time for the session")]
        [DataType(DataType.Time)]
        public TimeOnly EndTime { get; set; }
    }
}

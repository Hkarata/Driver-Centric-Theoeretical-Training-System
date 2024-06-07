namespace RSAllies.Contracts.Requests
{
    public class CreateVenueDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid DistrictId { get; set; }
        public Guid RegionId { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

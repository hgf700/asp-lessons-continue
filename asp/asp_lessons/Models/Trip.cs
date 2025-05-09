namespace aspapp.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Destination { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // FK to Guide
        public int GuideId { get; set; }
        public Guide Guide { get; set; } = null!;

        // Many-to-many relationship with Travelers
        public ICollection<Traveler> Travelers { get; set; } = new List<Traveler>();
    }
}

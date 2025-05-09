namespace aspapp.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Destination { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int GuideId { get; set; }
        public ICollection<Traveler> Travelers { get; set; } = new List<Traveler>();
        public ICollection<Guide> Guides { get; set; } = new List<Guide>();
    }
}

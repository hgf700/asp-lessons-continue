namespace aspapp.Models
{
    public class Trip
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Jeden przewodnik
        public int? GuideId { get; set; }

        public Guide? Guide { get; set; }

        // Lista podróżników (many-to-many)
        public List<Traveler> Travelers { get; set; } = new();
    }
}

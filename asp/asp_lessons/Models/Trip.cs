namespace aspapp.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Title of the trip
        public string Description { get; set; } = string.Empty; // Description of the trip

        // Foreign key to Guide
        public int? GuideId { get; set; }
        public Guide? Guide { get; set; } // Navigation property to Guide

        // Foreign key to Traveler
        public int? TravelerId { get; set; }
        public Traveler? Traveler { get; set; } // Navigation property to Traveler

        // Many-to-many relationship: A Trip can have many Travelers
        public List<Traveler> Travelers { get; set; } = new();
    }
}

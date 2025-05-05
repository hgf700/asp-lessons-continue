namespace aspapp.Models
{
    public class Traveler
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        // Many-to-many relationship: A Traveler can participate in many Trips
        public List<Trip>? Trips { get; set; } = new();
    }
}

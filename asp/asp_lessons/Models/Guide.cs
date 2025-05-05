namespace aspapp.Models
{
    public enum TitleEnum
    {
        Veteran,
        Beginner
    }

    public class Guide
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TitleEnum Title { get; set; }

        // One-to-many relationship: A Guide can lead many Trips
        public List<Trip> Trips { get; set; } = new();
    }
}

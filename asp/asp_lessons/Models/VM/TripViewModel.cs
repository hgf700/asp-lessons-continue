namespace aspapp.Models.VM
{
    public class TripViewModel
    {
        public int TripId { get; set; }
        public string Destination { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuideId { get; set; }
        public List<TravelerViewModel> Travelers { get; set; } = new();
        public List<GuideViewModel> Guides { get; set; } = new(); // do wyboru w formularzu
    }
}

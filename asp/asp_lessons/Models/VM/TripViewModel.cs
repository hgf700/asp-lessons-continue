namespace aspapp.Models.VM
{
    public class TripViewModel
    {
        public int TripId { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? GuideId { get; set; }
        public int? TravelerId { get; set; }

        public IEnumerable<GuideViewModel> Guides { get; set; }
        public IEnumerable<TravelerViewModel> Travelers { get; set; }

    }

}

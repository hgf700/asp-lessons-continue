namespace aspapp.Models.VM
{
    public class TravelerViewModel
    {
        public int TravelerId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        // Lista podróży związanych z podróżnikiem (w wersji widoku)
        public List<TripViewModel> Trips { get; set; } = new();
    }
}

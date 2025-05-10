namespace aspapp.Models.VM
{
    public class TripViewModel
    {
        public int TripId { get; set; }
        public string Destination { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int GuideId { get; set; } // Wybrany przewodnik
        public GuideViewModel? Guide { get; set; }

        public List<GuideViewModel> Guides { get; set; } = new(); // Lista przewodników do wyboru w formularzu

        public List<TravelerViewModel> Travelers { get; set; } = new(); // Lista dostępnych podróżników do wyświetlenia

        public List<int> SelectedTravelerIds { get; set; } = new(); // <-- NOWE: zaznaczeni podróżnicy
    }
}

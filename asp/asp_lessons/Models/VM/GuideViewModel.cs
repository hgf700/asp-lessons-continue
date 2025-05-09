﻿namespace aspapp.Models.VM
{
    public class GuideViewModel
    {
        public int GuideId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Title { get; set; }

        // Lista podróży w widoku (jeśli chcesz je wyświetlać)
        public List<TripViewModel> Trips { get; set; } = new();
    }
}

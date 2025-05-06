using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspapp.Models.VM
{
    public class TripViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Tytuł podróży jest wymagany.")]
        [StringLength(50, ErrorMessage = "Tytuł nie może przekraczać 50 znaków.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Opis podróży jest wymagany.")]
        [StringLength(100, ErrorMessage = "Opis nie może przekraczać 100 znaków.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Id przewodnika musi być większe niż 0.")]
        public int? GuideId { get; set; }

        [Required(ErrorMessage = "Wybierz przynajmniej jednego podróżnika.")]
        public List<int> TravelerIds { get; set; } = new();

        public List<Guide> Guides { get; set; }

        public List<Traveler> Travelers { get; set; }
    }
}

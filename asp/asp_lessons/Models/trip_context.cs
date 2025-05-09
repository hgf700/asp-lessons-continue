using Microsoft.EntityFrameworkCore;

namespace aspapp.Models
{
    public class trip_context : DbContext
    {
        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public trip_context(DbContextOptions<trip_context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacja między Trip a Traveler
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Travelers)  // Trip ma wielu Travelerów
                .WithMany(t => t.Trips)     // Traveler ma wiele Tripów
                .UsingEntity(j => j.ToTable("TripTraveler")); // Dodatkowa tabela pośrednia, ponieważ mamy relację wielu do wielu

            // Relacja między Trip a Guide
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Guides)    // Trip ma wielu Guides
                .WithMany(g => g.Trips)    // Guide ma wiele Tripów
                .UsingEntity(j => j.ToTable("TripGuide")); // Dodatkowa tabela pośrednia

            // Opcjonalnie możesz dodać konfigurację dla tabel pośrednich, jeśli chcesz przechowywać dodatkowe dane (np. daty)
            // modelBuilder.Entity<TripTraveler>()
            //     .HasKey(tt => new { tt.TripId, tt.TravelerId }); // Klucz złożony
            // modelBuilder.Entity<TripGuide>()
            //     .HasKey(tg => new { tg.TripId, tg.GuideId }); // Klucz złożony
        }
    }
}

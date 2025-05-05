using Microsoft.EntityFrameworkCore;
using aspapp.Models;

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
            // Many-to-Many relationship between Traveler and Trip
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Travelers)
                .WithMany(tr => tr.Trips)
                .UsingEntity<Dictionary<string, object>>(
                    "TripTraveler", // Nazwa tabeli pośredniej
                    j => j.HasOne<Traveler>().WithMany().HasForeignKey("TravelerId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Trip>().WithMany().HasForeignKey("TripId").OnDelete(DeleteBehavior.Cascade)
                );

            // One-to-Many relationship between Guide and Trip
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Guide)
                .WithMany(g => g.Trips)
                .HasForeignKey(t => t.GuideId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many relationship between Traveler and Trip (for main traveler)
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Traveler)
                .WithMany() // This is fine because Traveler does not need a collection of trips
                .HasForeignKey(t => t.TravelerId)
                .OnDelete(DeleteBehavior.SetNull); // Allow null on TravelerId if Traveler is deleted
        }
    }
}

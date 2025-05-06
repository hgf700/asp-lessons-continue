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
            // Many-to-Many: Trip - Traveler
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Travelers)
                .WithMany(tr => tr.Trips)
                .UsingEntity<Dictionary<string, object>>(
                    "TripTraveler",
                    j => j
                        .HasOne<Traveler>()
                        .WithMany()
                        .HasForeignKey("TravelerId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Trip>()
                        .WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            // One-to-Many: Guide - Trip
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Guide)
                .WithMany(g => g.Trips)
                .HasForeignKey(t => t.GuideId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

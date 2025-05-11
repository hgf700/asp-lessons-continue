//using Microsoft.EntityFrameworkCore;

//namespace aspapp.Models
//{
//    public class TripContext : DbContext
//    {
//        public DbSet<Traveler> Travelers { get; set; }
//        public DbSet<Guide> Guides { get; set; }
//        public DbSet<Trip> Trips { get; set; }

//        public TripContext(DbContextOptions<TripContext> options) : base(options) { }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Konfiguracja kolumn ID jako automatycznie generowane
//            modelBuilder.Entity<Guide>()
//                .Property(g => g.GuideId)
//                .ValueGeneratedOnAdd(); // Wartości dla GuideId są generowane automatycznie

//            modelBuilder.Entity<Traveler>()
//                .Property(t => t.TravelerId)
//                .ValueGeneratedOnAdd(); // Wartości dla TravelerId są generowane automatycznie

//            // Trip–Traveler (many-to-many relationship)
//            modelBuilder.Entity<Trip>()
//                .HasMany(t => t.Travelers)
//                .WithMany(t => t.Trips)
//                .UsingEntity<TripTraveler>(
//                    j => j.HasOne(tt => tt.Traveler).WithMany().HasForeignKey(tt => tt.TravelerId),
//                    j => j.HasOne(tt => tt.Trip).WithMany().HasForeignKey(tt => tt.TripId),
//                    j => j.ToTable("TripTraveler"));

//            // Trip–Guide (one-to-many relationship)
//            modelBuilder.Entity<Trip>()
//                .HasOne(t => t.Guide) // One Trip has one Guide
//                .WithMany(g => g.Trips) // One Guide can be assigned to many Trips
//                .HasForeignKey(t => t.GuideId)
//                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes, adjust as needed
//        }
//    }
//}

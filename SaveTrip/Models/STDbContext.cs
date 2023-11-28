using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace SaveTrip.Models
{
    public class STDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Travel> travels { get; set; }
        public DbSet<Cost> costs { get; set; }

        public STDbContext(DbContextOptions<STDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.Travels)
            .WithMany(t => t.Travelers);

            modelBuilder.Entity<Travel>()
                .HasMany(t => t.Travelers)
                .WithMany(u => u.Travels);

            modelBuilder.Entity<Travel>()
                .HasMany(t => t.TravelCosts)
                .WithOne(c => c.Travel)
                .HasForeignKey(c => c.TravelId);

            modelBuilder.Entity<Cost>()
                .HasOne(c => c.Travel)
                .WithMany(t => t.TravelCosts)
                .HasForeignKey(c => c.TravelId);

            modelBuilder.Entity<Cost>()
            .HasMany(c => c.Travelers)
            .WithMany(u => u.UserCosts);


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Travel>().ToTable("travels");
            modelBuilder.Entity<Cost>().ToTable("costs");

        }
    }
}

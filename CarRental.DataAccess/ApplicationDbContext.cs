using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {

        }


        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuelType>().HasData(
              new FuelType { Id = 1, Name = "Petrol" },
              new FuelType { Id = 2, Name = "Diesel" },
              new FuelType { Id = 3, Name = "Electric" },
              new FuelType { Id = 4, Name = "Hybrid" },
              new FuelType { Id = 5, Name = "Plug-in Hybrid" },
              new FuelType { Id = 6, Name = "LPG" },
              new FuelType { Id = 7, Name = "Mild Hybrid", IsActive = false },
              new FuelType { Id = 8, Name = "CNG", IsActive = false },
              new FuelType { Id = 9, Name = "Range Extender", IsActive = false },
              new FuelType { Id = 10, Name = "Hydrogen", IsActive = false }
          );

            modelBuilder.Entity<TransmissionType>().HasData(
                new TransmissionType { Id = 1, Name = "Manual" },
                new TransmissionType { Id = 2, Name = "Automatic" },
                new TransmissionType { Id = 3, Name = "Semi-Automatic" }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Economy", Description = "Small, cheap to run — ideal for city driving and short trips." },
                new Category { Id = 2, Name = "Sedan", Description = "Comfortable four-door cars with a separate boot." },
                new Category { Id = 3, Name = "SUV", Description = "Higher driving position and more space — good for rough roads." },
                new Category { Id = 4, Name = "Van", Description = "Maximum passenger and luggage capacity for groups." },
                new Category { Id = 5, Name = "Luxury", Description = "Premium vehicles with high-end interiors and performance." });
        }
    }
}

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuelType>().HasData(
                new FuelType { Id = 1, Name = "Petrol" },
                new FuelType { Id = 2, Name = "Diesel" },
                new FuelType { Id = 3, Name = "Electric" },
                new FuelType { Id = 4, Name = "Hybrid" }
            );
        }
    }
}

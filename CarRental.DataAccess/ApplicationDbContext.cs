using CarRental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {

        }


        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleImage> VehicleImages { get; set; }
        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                new Category { Id = 5, Name = "Luxury", Description = "Premium vehicles with high-end interiors and performance." }
            );
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Alfa Romeo" },
                new Brand { Id = 2, Name = "Audi" },
                new Brand { Id = 3, Name = "BMW" },
                new Brand { Id = 4, Name = "Chevrolet" },
                new Brand { Id = 5, Name = "Citroën" },
                new Brand { Id = 6, Name = "Dacia" },
                new Brand { Id = 7, Name = "Fiat" },
                new Brand { Id = 8, Name = "Ford" },
                new Brand { Id = 9, Name = "Honda" },
                new Brand { Id = 10, Name = "Hyundai" },
                new Brand { Id = 11, Name = "Jaguar" },
                new Brand { Id = 12, Name = "Jeep" },
                new Brand { Id = 13, Name = "Kia" },
                new Brand { Id = 14, Name = "Lancia" },
                new Brand { Id = 15, Name = "Land Rover" },
                new Brand { Id = 16, Name = "Lexus" },
                new Brand { Id = 17, Name = "Mazda" },
                new Brand { Id = 18, Name = "Mercedes-Benz" },
                new Brand { Id = 19, Name = "Mini" },
                new Brand { Id = 20, Name = "Mitsubishi" },
                new Brand { Id = 21, Name = "Nissan" },
                new Brand { Id = 22, Name = "Opel" },
                new Brand { Id = 23, Name = "Peugeot" },
                new Brand { Id = 24, Name = "Porsche" },
                new Brand { Id = 25, Name = "Renault" },
                new Brand { Id = 26, Name = "Seat" },
                new Brand { Id = 27, Name = "Škoda" },
                new Brand { Id = 28, Name = "Smart" },
                new Brand { Id = 29, Name = "Subaru" },
                new Brand { Id = 30, Name = "Suzuki" },
                new Brand { Id = 31, Name = "Tesla" },
                new Brand { Id = 32, Name = "Toyota" },
                new Brand { Id = 33, Name = "Volkswagen" },
                new Brand { Id = 34, Name = "Volvo" }
               );

            modelBuilder.Entity<VehicleModel>().HasOne(u => u.Brand)
                                               .WithMany()
                                               .HasForeignKey(u => u.BrandId)
                                               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feature>().HasData(
                new Feature { Id = 1, Name = "Air Conditioning" },
                new Feature { Id = 2, Name = "Bluetooth" },
                new Feature { Id = 3, Name = "GPS Navigation" },
                new Feature { Id = 4, Name = "Parking Sensors" },
                new Feature { Id = 5, Name = "Reversing Camera" },
                new Feature { Id = 6, Name = "Cruise Control" },
                new Feature { Id = 7, Name = "Heated Seats" },
                new Feature { Id = 8, Name = "Leather Seats" },
                new Feature { Id = 9, Name = "Sunroof" },
                new Feature { Id = 10, Name = "Roof Rack" },
                new Feature { Id = 11, Name = "Tow Bar" },
                new Feature { Id = 12, Name = "Child Seat" },
                new Feature { Id = 13, Name = "USB Charging" },
                new Feature { Id = 14, Name = "Apple CarPlay" },
                new Feature { Id = 15, Name = "Android Auto" },
                new Feature { Id = 16, Name = "Winter Tyres" }
            );

            modelBuilder.Entity<Vehicle>().HasOne(u => u.FuelType)
                                          .WithMany()
                                          .HasForeignKey(u => u.FuelTypeId)
                                          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle>().HasOne(u => u.TransmissionType)
                                          .WithMany()
                                          .HasForeignKey(u => u.TransmissionTypeId)
                                          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle>().HasOne(u => u.Category)
                                          .WithMany()
                                          .HasForeignKey(u => u.CategoryId)
                                          .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vehicle>().HasOne(u => u.VehicleModel)
                                          .WithMany()
                                          .HasForeignKey(u => u.VehicleModelId)
                                          .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vehicle>().HasIndex(u => u.RegistrationPlate)
                                          .IsUnique();

            modelBuilder.Entity<Vehicle>().Property(u => u.EngineSize).HasPrecision(3, 1);

            modelBuilder.Entity<Vehicle>().Property(u => u.RentalPricePerDay).HasPrecision(10, 2);

            modelBuilder.Entity<Vehicle>().Property(u => u.SalePrice).HasPrecision(10, 2);

            modelBuilder.Entity<VehicleImage>().HasOne(u => u.Vehicle)
                                               .WithMany(u => u.VehicleImages)
                                               .HasForeignKey(u => u.VehicleId)
                                               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Registration plate")]
        public string RegistrationPlate { get; set; } = string.Empty;

        // ---- relationships -------------------------------------------------
        // Each FK holds the number that is stored; each navigation property is
        // where EF puts the related object when you ask for it with Include.

        [Range(1, int.MaxValue)]
        [Display(Name = "Model")]
        public int VehicleModelId { get; set; }

        [ForeignKey(nameof(VehicleModelId))]
        public VehicleModel? VehicleModel { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Fuel type")]
        public int FuelTypeId { get; set; }

        [ForeignKey(nameof(FuelTypeId))]
        public FuelType? FuelType { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Transmission")]
        public int TransmissionTypeId { get; set; }

        [ForeignKey(nameof(TransmissionTypeId))]
        public TransmissionType? TransmissionType { get; set; }

        // ---- specification -------------------------------------------------

        [Range(1950, 2100)]
        public int Year { get; set; }

        [Required]
        [StringLength(50)]
        public string Color { get; set; } = string.Empty;

        [Range(1, 20)]
        public int Seats { get; set; }

        [Range(1, 10)]
        public int Doors { get; set; }

        // Null for electric vehicles, which have no engine displacement.
        [Display(Name = "Engine size (L)")]
        [Range(0.1, 99.9)]
        public decimal? EngineSize { get; set; }

        [Range(0, 2000000)]
        public int Mileage { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        // ---- pricing -------------------------------------------------------
        // A car can be offered for rent, for sale, or both — hence two
        // independent flag/price pairs rather than a single "type" field.
        // Each price is nullable because it only applies when its flag is set.

        [Display(Name = "For rent")]
        public bool IsForRent { get; set; }

        [Display(Name = "Rental price per day")]
        public decimal RentalPricePerDay { get; set; }

        [Display(Name = "For sale")]
        public bool IsForSale { get; set; }

        [Display(Name = "Sale price")]
        public decimal SalePrice { get; set; }

        // ---- state ---------------------------------------------------------

        // Soft delete: hides the car from the public site without losing the record.
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // When the listing was added — not the year the car was made.
        // Set once on create, never changed on edit.
        [Display(Name = "Added")]
        public DateTime CreatedAt { get; set; }

        public ICollection<VehicleImage>? VehicleImages { get; set; }
    }
}

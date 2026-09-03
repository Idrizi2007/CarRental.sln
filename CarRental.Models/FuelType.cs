using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class FuelType : ILookup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Display Order must be between 1 and 50 characters long.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; } = true;
    }
}

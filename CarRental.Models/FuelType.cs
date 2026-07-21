using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class FuelType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Display Order must be between 1 and 100 characters long.")]
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}

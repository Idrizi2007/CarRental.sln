using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class VehicleImage
    {
        [Key]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int VehicleId { get; set; }
        [ForeignKey(nameof(VehicleId))]
        public Vehicle? Vehicle { get; set; }
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        [StringLength(200)]
        [Display(Name = "Alt text")]
        public string? AltText { get; set; }
        [Display(Name = "Order")]
        public int SortOrder { get; set; }
    }
}

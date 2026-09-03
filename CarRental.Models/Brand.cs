using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Brand : ILookup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? LogoUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

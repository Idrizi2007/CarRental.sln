using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class TransmissionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; } = true;
    }
}

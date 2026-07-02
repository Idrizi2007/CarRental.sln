using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class FuelType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Fuel Type")]
        public string Name { get; set; }
    }
}

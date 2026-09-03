using CarRental.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.Web.ViewModels
{
    public class VehicleModelVM
    {
        public VehicleModel? VehicleModel { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? BrandList { get; set; }
    }
}

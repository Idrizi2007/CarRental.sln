using CarRental.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.Web.ViewModels
{
    public class VehicleVM
    {
        public Vehicle? Vehicle { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? VehicleModelList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? FuelTypeList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? TransmissionTypeList { get; set; }
    }
}

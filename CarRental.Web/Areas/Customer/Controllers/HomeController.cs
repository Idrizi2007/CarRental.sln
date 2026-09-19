using CarRental.DataAccess.Repository.IRepository;
using CarRental.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRental.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var vehicles = _unitOfWork.Vehicle.GetAll(u => u.IsActive, includeProperties: "VehicleModel.Brand,Category,FuelType,TransmissionType,VehicleImages").OrderByDescending(u => u.CreatedAt).ToList();
            return View(vehicles);
        }

        public IActionResult Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var vehicleFromDb = _unitOfWork.Vehicle.Get(u => u.Id == id && u.IsActive, includeProperties: "VehicleModel.Brand,Category,FuelType,TransmissionType,VehicleImages");
            if (vehicleFromDb == null)
            {
                return NotFound();
            }
            return View(vehicleFromDb);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

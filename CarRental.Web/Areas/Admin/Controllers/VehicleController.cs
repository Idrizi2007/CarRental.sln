using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;
using CarRental.Web.Extensions;
using CarRental.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace CarRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var vehicleFromDb = new VehicleVM()
            {
                Vehicle = new Vehicle(),
                VehicleModelList = _unitOfWork.VehicleModel.GetAll(u => u.IsActive).ToSelectList(),
                CategoryList = _unitOfWork.Category.GetAll(u => u.IsActive).ToSelectList(),
                FuelTypeList = _unitOfWork.FuelType.GetAll(u => u.IsActive).ToSelectList(),
                TransmissionTypeList = _unitOfWork.TransmissionType.GetAll(u => u.IsActive).ToSelectList()
            };
            if (id == null || id == 0)
            {
                return View(vehicleFromDb);
            }
            vehicleFromDb.Vehicle = _unitOfWork.Vehicle.Get(u => u.Id == id, includeProperties: "VehicleModel.Brand,Category,FuelType,TransmissionType");
            if (vehicleFromDb.Vehicle == null)
            {
                return NotFound();
            }
            return View(vehicleFromDb);
        }
        [HttpPost]
        public IActionResult Upsert(VehicleVM vehicleVM)
        {
            if (ModelState.IsValid)
            {
                bool isNew = vehicleVM.Vehicle.Id == 0;
                if (isNew)
                {
                    vehicleVM.Vehicle.CreatedAt = DateTime.UtcNow;
                    _unitOfWork.Vehicle.Add(vehicleVM.Vehicle);
                }
                else
                {
                    _unitOfWork.Vehicle.Update(vehicleVM.Vehicle);
                }
                _unitOfWork.Save();
                TempData["success"] = isNew ? "Vehicle created successfully" : "Vehicle updated successfully";
                return RedirectToAction(nameof(Index));
            }
            vehicleVM.VehicleModelList = _unitOfWork.VehicleModel.GetAll(u => u.IsActive).ToSelectList();
            vehicleVM.CategoryList = _unitOfWork.Category.GetAll(u => u.IsActive).ToSelectList();
            vehicleVM.FuelTypeList = _unitOfWork.FuelType.GetAll(u => u.IsActive).ToSelectList();
            vehicleVM.TransmissionTypeList = _unitOfWork.TransmissionType.GetAll(u => u.IsActive).ToSelectList();
            return View(vehicleVM);
        }


        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var vehiclesFromDb = _unitOfWork.Vehicle.GetAll(includeProperties: "VehicleModel.Brand,Category,FuelType,TransmissionType").ToList();
            return Json(new { data = vehiclesFromDb });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest(new { message = "Invalid vehicle ID" });
            }
            var vehcicleFromDb = _unitOfWork.Vehicle.Get(u => u.Id == id);
            if (vehcicleFromDb == null)
            {
                return NotFound(new { message = "Vehicle not found" });
            }
            _unitOfWork.Vehicle.Remove(vehcicleFromDb);
            _unitOfWork.Save();
            return Ok(new { message = "Vehicle deleted successfully" });
        }
        #endregion
    }
}

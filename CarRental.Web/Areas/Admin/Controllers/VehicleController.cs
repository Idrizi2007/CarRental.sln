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
        private readonly IWebHostEnvironment _hostEnvironment;
        public VehicleController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment=hostEnvironment;

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
        public IActionResult Upsert(VehicleVM vehicleVM, List<IFormFile>? files)
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
                if (files != null && files.Count > 0)
                {
                    string rootPath = _hostEnvironment.WebRootPath;
                    string path = Path.Combine(rootPath, "images", "vehicles", vehicleVM.Vehicle.Id.ToString());
                    Directory.CreateDirectory(path);
                    var imageFromDb = _unitOfWork.VehicleImage.GetAll(u => u.VehicleId == vehicleVM.Vehicle.Id);
                    foreach (var file in files)
                    {
                        if (file.Length > 10 * 1024 * 1024|| file.Length == 0) continue; // Skip files larger than 10MB or empty files
                        var fileName = Guid.NewGuid().ToString() + (Path.GetExtension(file.FileName));
                        var filePath = Path.Combine(path, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }

                }
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

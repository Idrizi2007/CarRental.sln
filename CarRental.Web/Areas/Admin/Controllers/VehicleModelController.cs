using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;
using CarRental.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleModelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleModelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            VehicleModelVM VehicleModelViewModel = new VehicleModelVM()
            {
                VehicleModel = new VehicleModel(),
                BrandList = _unitOfWork.Brand.GetAll(u => u.IsActive).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            if (id == 0 || id == null)
            {
                return View(VehicleModelViewModel);
            }
            VehicleModelViewModel.VehicleModel = _unitOfWork.VehicleModel.Get(u => u.Id == id);
            if (VehicleModelViewModel.VehicleModel == null)
            {
                return NotFound();
            }
            return View(VehicleModelViewModel);


        }

        [HttpPost]
        public IActionResult Upsert(VehicleModelVM vehicleModelViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNew = vehicleModelViewModel.VehicleModel.Id == 0;
                if (isNew)
                {
                    _unitOfWork.VehicleModel.Add(vehicleModelViewModel.VehicleModel);
                }
                else
                {
                    _unitOfWork.VehicleModel.Update(vehicleModelViewModel.VehicleModel);
                }
                _unitOfWork.Save();
                TempData["success"] = isNew ? "Vehicle Model created successfully" : "Vehicle Model updated successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(vehicleModelViewModel);

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var vehicleModelFromDb = _unitOfWork.VehicleModel.GetAll(u => u.Brand.IsActive, includeProperties: "Brand").OrderBy(x => x.Name).ToList();
            return Json(new { data = vehicleModelFromDb });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest(new { message = "Invalid Model id." });
            }
            VehicleModel vehicleModel = _unitOfWork.VehicleModel.Get(u => u.Id == id);
            if (vehicleModel == null)
            {
                return NotFound(new { message = "Model not found." });
            }
            _unitOfWork.VehicleModel.Remove(vehicleModel);
            _unitOfWork.Save();
            return Ok(new { message = "Model deleted successfully." });
        }
        #endregion
    }
}

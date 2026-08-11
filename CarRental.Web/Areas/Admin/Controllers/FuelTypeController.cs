using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    [Area("Admin")]
    public class FuelTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FuelTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<FuelType> fuelTypes = _unitOfWork.FuelType.GetAll().ToList();
            return View(fuelTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FuelType fuelType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FuelType.Add(fuelType);
                _unitOfWork.Save();
                TempData["success"] = "Fuel Type created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(fuelType);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            FuelType fuelType = _unitOfWork.FuelType.Get(u => u.Id == id);
            if (fuelType == null)
                return NotFound();
            return View(fuelType);
        }

        [HttpPost]
        public IActionResult Edit(FuelType fuelType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FuelType.Update(fuelType);
                _unitOfWork.Save();
                TempData["success"] = "Fuel Type updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(fuelType);
        }

        #region Api Call
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return Json(new { success = false, message = "Invalid fuel type id." });
            }
            var fuelType = _unitOfWork.FuelType.Get(u => u.Id == id);
            if (fuelType == null)
            {
                return Json(new { success = false, message = "Fuel type not found." });
            }
            _unitOfWork.FuelType.Remove(fuelType);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Fuel type deleted successfully." });
        }

        #endregion

    }
}

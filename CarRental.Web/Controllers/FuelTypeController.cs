using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
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
            return View(fuelType);
        }

        [HttpPost]
        public IActionResult Edit(FuelType fuelType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FuelType.Update(fuelType);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(fuelType);
        }

    }
}

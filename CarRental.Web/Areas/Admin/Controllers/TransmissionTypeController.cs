using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransmissionTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransmissionTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<TransmissionType> transmissionTypes = _unitOfWork.TransmissionType.GetAll().ToList();
            return View(transmissionTypes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TransmissionType transmissionType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TransmissionType.Add(transmissionType);
                _unitOfWork.Save();
                TempData["success"] = "Transmission Type created successfully";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(transmissionType);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0  || id == null)
                return NotFound();
            TransmissionType transmissionType = _unitOfWork.TransmissionType.Get(u => u.Id == id);
            if (transmissionType == null)

                return NotFound();
            return View(transmissionType);
        }

        [HttpPost]
        public IActionResult Edit(TransmissionType transmissionType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TransmissionType.Update(transmissionType);
                _unitOfWork.Save();
                TempData["success"] = "Transmission Type updated successfully";
                return RedirectToAction(nameof(Index));

            }
            return View(transmissionType);
        }
    }
}
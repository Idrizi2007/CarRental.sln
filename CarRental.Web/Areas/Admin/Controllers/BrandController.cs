using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Brand());
            }
            Brand brandFromDb = _unitOfWork.Brand.Get(u => u.Id == id);
            if (brandFromDb == null)
            {
                return NotFound();
            }
            return View(brandFromDb);
        }
        [HttpPost]

        public IActionResult Upsert(Brand brand)
        {
            if (ModelState.IsValid)
            {
                bool isNew = brand.Id == 0;
                if (isNew)
                {
                    _unitOfWork.Brand.Add(brand);
                }
                else
                {
                    _unitOfWork.Brand.Update(brand);
                }
                _unitOfWork.Save();
                TempData["success"] = isNew ? "Brand created successfully" : "Brand updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var brandsFromDb = _unitOfWork.Brand.GetAll().OrderBy(u => u.Name).ToList();
            return Json(new { data = brandsFromDb });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest(new { message = "Invalid brand id." });
            }
            Brand brand = _unitOfWork.Brand.Get(u => u.Id == id);
            if (brand == null)
            {
                return NotFound(new { message = "Brand not found." });
            }
            _unitOfWork.Brand.Remove(brand);
            _unitOfWork.Save();
            return Ok(new { message = "Brand deleted successfully." });
        }

        #endregion
    }
}

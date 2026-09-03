using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeatureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Feature> featuresFromDb = _unitOfWork.Feature.GetAll().OrderBy(u => u.Name).ToList();
            return View(featuresFromDb);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Feature());
            }

            Feature featureFromDb = _unitOfWork.Feature.Get(u => u.Id == id);
            if (featureFromDb == null)
            {
                return NotFound();
            }

            return View(featureFromDb);
        }

        [HttpPost]
        public IActionResult Upsert(Feature feature)
        {
            if (ModelState.IsValid)
            {
                // Captured before Save, because EF writes the new id back after an insert.
                bool isNew = feature.Id == 0;

                if (isNew)
                {
                    _unitOfWork.Feature.Add(feature);
                }
                else
                {
                    _unitOfWork.Feature.Update(feature);
                }

                _unitOfWork.Save();
                TempData["success"] = isNew ? "Feature created successfully." : "Feature updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(feature);
        }

        #region API CALLS
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest(new { message = "Invalid feature id." });
            }

            Feature featureFromDb = _unitOfWork.Feature.Get(u => u.Id == id);
            if (featureFromDb == null)
            {
                return NotFound(new { message = "Feature not found." });
            }

            _unitOfWork.Feature.Remove(featureFromDb);
            _unitOfWork.Save();
            return Ok(new { message = "Feature deleted successfully." });
        }
        #endregion
    }
}

using GymManagementSystem.BLL.Interfaces;
using GymManagementSystem.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.PL.Controllers
{
    [Authorize]  
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
            => _trainerService = trainerService;

        public IActionResult Index()
            => View(_trainerService.GetAllTrainers());

        public IActionResult Details(int id)
        {
            var model = _trainerService.GetTrainerDetails(id);
            return model is null ? NotFound() : View(model);
        }

        public IActionResult Create()
            => View(new TrainerCreateViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(TrainerCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _trainerService.CreateTrainer(model);
            TempData["Success"] = "Trainer added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var model = _trainerService.GetTrainerForEdit(id);
            return model is null ? NotFound() : View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(TrainerEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _trainerService.EditTrainer(model);
            TempData["Success"] = "Trainer updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (_trainerService.HasScheduledSessions(id))
            {
                TempData["Error"] = "Cannot delete a trainer with scheduled sessions.";
                return RedirectToAction(nameof(Index));
            }

            var model = _trainerService.GetTrainerForDelete(id);
            return model is null ? NotFound() : View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool ok = _trainerService.DeleteTrainer(id);

            TempData[ok ? "Success" : "Error"] = ok
                ? "Trainer deleted successfully."
                : "Cannot delete a trainer with scheduled sessions.";

            return RedirectToAction(nameof(Index));
        }
    }
}

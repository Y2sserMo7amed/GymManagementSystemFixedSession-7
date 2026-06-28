using GymManagementSystem.BLL.Interfaces;
using GymManagementSystem.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.PL.Controllers
{
    [Authorize]  
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
            => _planService = planService;

        public IActionResult Index()
            => View(_planService.GetAllPlans());

        public IActionResult Details(int id)
        {
            var model = _planService.GetPlanDetails(id);
            return model is null ? NotFound() : View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _planService.GetPlanForEdit(id);
            return model is null ? NotFound() : View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(PlanEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (!_planService.EditPlan(model))
            {
                ModelState.AddModelError("", "Failed to update plan.");
                return View(model);
            }

            TempData["Success"] = "Plan updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Activate(int id)
        {
            bool ok = _planService.ToggleActivation(id);

            TempData[ok ? "Success" : "Error"] = ok
                ? "Plan status changed successfully."
                : "Cannot deactivate a plan that has active memberships.";

            return RedirectToAction(nameof(Index));
        }
    }
}

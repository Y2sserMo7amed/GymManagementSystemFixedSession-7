using GymManagementSystem.BLL.Interfaces;
using GymManagementSystem.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.PL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
            => _memberService = memberService;

        public IActionResult Index()
            => View(_memberService.GetAllMembers());

        public IActionResult MemberDetails(int id)
        {
            var model = _memberService.GetMemberDetails(id);
            return model is null ? NotFound() : View(model);
        }

        public IActionResult Create()
            => View(new CreateMemberViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateMemberViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _memberService.CreateMember(model);
            TempData["Success"] = "Member Created Successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditMember(int id)
        {
            var model = _memberService.GetMemberForEdit(id);
            return model is null ? NotFound() : View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditMember(EditMemberViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _memberService.EditMember(model);
            TempData["Success"] = "Member Updated Successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = _memberService.GetMemberDetails(id);
            return model is null ? NotFound() : View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _memberService.DeleteMember(id);
            TempData["Success"] = "Member Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

using GymManagementSystem.BLL.ViewModels;
using GymManagementSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.PL.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly SignInManager<GymUser> _signInManager;

        public AccountController(SignInManager<GymUser> signInManager)
        {
            _signInManager = signInManager;
        }

       
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(new LoginViewModel());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            
            var result = await _signInManager.PasswordSignInAsync(
                userName:          model.Email,
                password:          model.Password,
                isPersistent:      model.RememberMe,
                lockoutOnFailure:  true
            );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("InvalidLogin", "Account is temporarily locked. Please try again in 2 minutes.");
            }
            else
            {
                ModelState.AddModelError("InvalidLogin", "Invalid email or password.");
            }

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

       
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

using Fab.Models.UserFolder;
using Fab.ViewModels.AccountFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabAdmin.Controllers
{
    [Area("FabAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(LoginVM model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View(model);
            };

            

             Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (User.Identity.IsAuthenticated)
            {
                // Kullanıcı giriş yapmışsa buraya gelir
                return RedirectToAction("Index", "Dashboard");
            }
            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Please Confirm Your Accaunt");
                    return View(model);
                }
                ModelState.AddModelError("", "Email or Password is Wrong");
                return View(model);
            }
            
            if (await _userManager.IsInRoleAsync(user, "Admin") || true)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View("Login", "Account");

           



        }

        
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // Redirect to the home page or any other desired page after logout
            return RedirectToAction("Login", "Account");
        }



    }
}

using Data.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController:Controller
    {
      
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
          
            return View(new LoginModel() {ReturnUrl=returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user =await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("","Istifadeci adi tapilmadi");
                return View(model);
            }
            var result =await _signInManager.PasswordSignInAsync(user,model.Password,true,false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");
            }
            ModelState.AddModelError("", "Istifadeci adi ve parol yalnisdir");
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LogiDriverPortal.Models;
using LogiDriverPortal.Models.ViewModels;
using System.Threading.Tasks;

namespace LogiDriverPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// DEVELOPMENT ONLY: Simplified login that bypasses password validation.
        /// This allows you to explore the website without password issues.
        /// 
        /// HOW IT WORKS:
        /// - Enter ANY email and ANY password
        /// - It will find or create a user with that email
        /// - Signs you in automatically
        /// 
        /// ⚠️ REMOVE THIS BEFORE PRODUCTION! Replace with secure login.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // ==============================================================
            // 🚀 QUICK LOGIN BYPASS - Works with ANY email/password
            // ==============================================================
            /*
            // Step 1: Try to find existing user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            // Step 2: If user doesn't exist, create them automatically
            if (user == null)
            {
                user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = "Demo User",
                    Role = "Supervisor",
                    Status = "Active",
                    EmailConfirmed = true
                };
                // Create user with a default password (we won't validate it anyway)
                var createResult = await _userManager.CreateAsync(user, "DemoPass123!");
                if (!createResult.Succeeded)
                {
                    // If creation fails, show error
                    ModelState.AddModelError(string.Empty, "Failed to create user account.");
                    return View(model);
                }
            }
            // Step 3: Sign in the user WITHOUT password validation
            await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
            // Step 4: Redirect to dashboard
            return RedirectToAction("Dashboard", "Home");
            */
            // ==============================================================
            // 🔒 FOR PRODUCTION: Replace everything above with this:
            // ==============================================================
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false
                );

                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                // In a real app, you would email the code to the user.
                // For this demo, we'll redirect to the ResetPassword page with the code.
                return RedirectToAction("ResetPassword", new { code = code, email = model.Email });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null, string email = null)
        {
            if (code == null || email == null)
            {
                return View("Error");
            }
            var model = new ResetPasswordViewModel { Code = code, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
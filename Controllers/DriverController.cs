using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity; // MUST be included

namespace LogiDriverPortal.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager; // Using your custom User class

        // Constructor now includes UserManager
        public DriverController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ... (Index GET method) ...

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Driver driver)
        {
            // ModelState.IsValid now checks the required fields in the Driver model,
            // including the new [NotMapped] Email and Password properties.
            if (ModelState.IsValid)
            {
                // 1. CREATE THE ASP.NET IDENTITY USER (Login Account)
                var user = new User
                {
                    UserName = driver.Email,
                    Email = driver.Email,
                    EmailConfirmed = true,
                    FullName = driver.FullName,
                    Role = "Driver", // Assign the appropriate role
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow // Ensure this property is set if required by your User.cs
                };

                // The UserManager hashes the password and saves the User to AspNetUsers
                var result = await _userManager.CreateAsync(user, driver.Password);

                if (result.Succeeded)
                {
                    // 2. CREATE THE DRIVER ENTITY (Profile/Details)
                    _context.Drivers.Add(driver);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"Driver {driver.FullName} and login account created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                // If user creation failed (e.g., duplicate email)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If validation failed or Identity creation failed
            return View(driver);
        }


        // GET: Driver
        public async Task<IActionResult> Index()
        {
            var drivers = await _context.Drivers
                .OrderBy(d => d.FullName)
                .ToListAsync();

            return View(drivers);
        }

    }
}
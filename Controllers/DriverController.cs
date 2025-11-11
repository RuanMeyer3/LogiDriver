using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LogiDriverPortal.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager; 

        public DriverController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Driver driver)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = driver.Email,
                    Email = driver.Email,
                    EmailConfirmed = true,
                    FullName = driver.FullName,
                    Role = "Driver",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow 
                };

                var result = await _userManager.CreateAsync(user, driver.Password);

                if (result.Succeeded)
                {
                    _context.Drivers.Add(driver);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"Driver {driver.FullName} and login account created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(driver);
        }


        public async Task<IActionResult> Index()
        {
            var drivers = await _context.Drivers
                .OrderBy(d => d.FullName)
                .ToListAsync();

            return View(drivers);
        }

    }
}
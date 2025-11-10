// These are the required namespaces for authorization, MVC, database access, and async operations
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics; // Added for diagnostic logging

namespace LogiDriverPortal.Controllers
{
    // Only logged-in users can access this controller
    [Authorize]
    public class RoutePlanningController : Controller
    {
        // This connects the controller to the database
        private readonly ApplicationDbContext _context;

        // Constructor sets up the database connection
        public RoutePlanningController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- INDEX ACTION ---
        // Show a list of all route plans, newest first
        public async Task<IActionResult> Index()
        {
            var routes = await _context.RoutePlans
                .Include(r => r.Driver)
                .Include(r => r.Vehicle)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(routes);
        }

        // --- CREATE ACTIONS ---

        // Show the form to create a new route plan
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Load active drivers and available/in-transit vehicles for dropdowns
            ViewBag.Drivers = await _context.Drivers
                .Where(d => d.Status == "Active")
                .OrderBy(d => d.FullName)
                .ToListAsync();

            ViewBag.Vehicles = await _context.Vehicles
                .Where(v => v.Status == "Available" || v.Status == "In-Transit")
                .OrderBy(v => v.RegistrationNumber)
                .ToListAsync();

            return View();
        }

        // Save the new route plan to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoutePlan routePlan)
        {
            // 🛑 DIAGNOSTIC LOGGING START 🛑
            // Logs the received model data and any validation errors
            Debug.WriteLine("==============================================");
            Debug.WriteLine("ROUTE PLAN SUBMISSION RECEIVED:");
            Debug.WriteLine($"StartLocation: {routePlan.StartLocation}");
            Debug.WriteLine($"DriverId: {routePlan.DriverId}");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("--- MODEL STATE IS INVALID ---");
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        Debug.WriteLine($"Field: {state.Key}");
                        foreach (var error in state.Value.Errors)
                        {
                            Debug.WriteLine($"  Error: {error.ErrorMessage}");
                        }
                    }
                }
                Debug.WriteLine("------------------------------");
            }
            // 🛑 DIAGNOSTIC LOGGING END 🛑

            if (ModelState.IsValid)
            {
                // Set auto-generated values
                routePlan.RouteCode = await GenerateRouteCodeAsync(); // AWAIT the async code generation
                routePlan.Status = "Active";
                routePlan.Progress = 0;
                routePlan.StartTime = DateTime.UtcNow;
                routePlan.CreatedAt = DateTime.UtcNow;

                // Save to database
                _context.RoutePlans.Add(routePlan);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Route plan created successfully!";
                return RedirectToAction(nameof(Index));
            }

            // If form validation fails, reload dropdowns and show form again
            // NOTE: Ensure this logic matches the HTTP GET for consistent data
            ViewBag.Drivers = await _context.Drivers
                .Where(d => d.Status == "Active")
                .OrderBy(d => d.FullName)
                .ToListAsync();

            ViewBag.Vehicles = await _context.Vehicles
                .Where(v => v.Status == "Available" || v.Status == "In-Transit") // Corrected for consistency
                .OrderBy(v => v.RegistrationNumber)
                .ToListAsync();

            return View(routePlan);
        }

        // --- EDIT ACTIONS ---

        // Show the form to edit an existing route plan
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var routePlan = await _context.RoutePlans
                .Include(r => r.Driver)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.RoutePlanId == id);

            if (routePlan == null)
            {
                return NotFound();
            }

            // Load dropdowns again for editing
            ViewBag.Drivers = await _context.Drivers
                .Where(d => d.Status == "Active")
                .ToListAsync();

            ViewBag.Vehicles = await _context.Vehicles
                .Where(v => v.Status == "Available" || v.Status == "In-Transit")
                .ToListAsync();

            return View(routePlan);
        }

        // Save changes to an existing route plan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoutePlan routePlan)
        {
            if (id != routePlan.RoutePlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routePlan);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Route plan updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutePlanExists(routePlan.RoutePlanId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns if validation fails
            ViewBag.Drivers = await _context.Drivers.ToListAsync();
            ViewBag.Vehicles = await _context.Vehicles.ToListAsync();
            return View(routePlan);
        }

        // --- UTILITY/STATUS ACTIONS ---

        // Delete a route plan from the system
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var routePlan = await _context.RoutePlans.FindAsync(id);
            if (routePlan != null)
            {
                _context.RoutePlans.Remove(routePlan);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Route plan deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        // Mark a route plan as completed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteRoute(int id)
        {
            var routePlan = await _context.RoutePlans.FindAsync(id);
            if (routePlan != null)
            {
                routePlan.Status = "Completed";
                routePlan.Progress = 100;
                routePlan.EndTime = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Route marked as completed!";
            }

            return RedirectToAction(nameof(Index));
        }

        // Check if a route plan exists in the database
        private bool RoutePlanExists(int id)
        {
            return _context.RoutePlans.Any(e => e.RoutePlanId == id);
        }

        // Generate a unique route code like RT001, RT002, etc.
        // CHANGED TO ASYNCHRONOUS METHOD to avoid deadlocks
        private async Task<string> GenerateRouteCodeAsync()
        {
            var lastRoute = await _context.RoutePlans
                .OrderByDescending(r => r.RoutePlanId)
                .FirstOrDefaultAsync(); // Used Async version

            int nextNumber = 1;
            if (lastRoute != null && lastRoute.RouteCode.StartsWith("RT"))
            {
                string numberPart = lastRoute.RouteCode.Substring(2);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"RT{nextNumber:D3}";
        }
    }
}
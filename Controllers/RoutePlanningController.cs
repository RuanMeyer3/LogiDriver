using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogiDriverPortal.Controllers
{
    [Authorize]
    public class RoutePlanningController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoutePlanningController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /RoutePlanning/Index
        public async Task<IActionResult> Index()
        {
            var routes = await _context.RoutePlans
                .Include(r => r.Driver)
                .Include(r => r.Vehicle)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(routes);
        }

        // GET: /RoutePlanning/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
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

        // POST: /RoutePlanning/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoutePlan routePlan)
        {
            if (ModelState.IsValid)
            {
                routePlan.RouteCode = GenerateRouteCode();
                routePlan.Status = "Active";
                routePlan.Progress = 0;
                routePlan.StartTime = DateTime.UtcNow;
                routePlan.CreatedAt = DateTime.UtcNow;

                _context.RoutePlans.Add(routePlan);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Route plan created successfully!";
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdown data if validation fails
            ViewBag.Drivers = await _context.Drivers
                .Where(d => d.Status == "Active")
                .ToListAsync();

            ViewBag.Vehicles = await _context.Vehicles
                .Where(v => v.Status == "Available")
                .ToListAsync();

            return View(routePlan);
        }

        // GET: /RoutePlanning/Edit/5
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

            ViewBag.Drivers = await _context.Drivers
                .Where(d => d.Status == "Active")
                .ToListAsync();

            ViewBag.Vehicles = await _context.Vehicles
                .Where(v => v.Status == "Available" || v.Status == "In-Transit")
                .ToListAsync();

            return View(routePlan);
        }

        // POST: /RoutePlanning/Edit/5
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

            ViewBag.Drivers = await _context.Drivers.ToListAsync();
            ViewBag.Vehicles = await _context.Vehicles.ToListAsync();
            return View(routePlan);
        }

        // POST: /RoutePlanning/Delete/5
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

        // POST: /RoutePlanning/CompleteRoute/5
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

        private bool RoutePlanExists(int id)
        {
            return _context.RoutePlans.Any(e => e.RoutePlanId == id);
        }

        private string GenerateRouteCode()
        {
            var lastRoute = _context.RoutePlans
                .OrderByDescending(r => r.RoutePlanId)
                .FirstOrDefault();

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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

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
            try
            {
                var drivers = await _context.Drivers
                    .Where(d => d.Status == "Active")
                    .OrderBy(d => d.FullName)
                    .ToListAsync();

                var vehicles = await _context.Vehicles
                    .Where(v => v.Status == "Available" || v.Status == "In-Transit")
                    .OrderBy(v => v.RegistrationNumber)
                    .ToListAsync();

                ViewBag.Drivers = drivers;
                ViewBag.Vehicles = vehicles;

                Debug.WriteLine($"✓ Loaded {drivers.Count} drivers and {vehicles.Count} vehicles");

                return View();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"✗ Error loading Create form: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error loading form data");
                return View();
            }
        }

        // POST: /RoutePlanning/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoutePlan routePlan)
        {
            try
            {
                Debug.WriteLine("============================================");
                Debug.WriteLine("🚀 CREATE ROUTE PLAN SUBMISSION");
                Debug.WriteLine("============================================");
                Debug.WriteLine($"StartLocation: '{routePlan.StartLocation}'");
                Debug.WriteLine($"EndLocation: '{routePlan.EndLocation}'");
                Debug.WriteLine($"Waypoints: '{routePlan.Waypoints}'");
                Debug.WriteLine($"DriverId: {routePlan.DriverId}");
                Debug.WriteLine($"VehicleId: {routePlan.VehicleId}");
                Debug.WriteLine($"DistanceKm: {routePlan.DistanceKm}");
                Debug.WriteLine($"EstimatedArrival: {routePlan.EstimatedArrival}");
                Debug.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

                // CRITICAL: Remove navigation property validation errors
                ModelState.Remove("Driver");
                ModelState.Remove("Vehicle");

                // Log all validation errors
                if (!ModelState.IsValid)
                {
                    Debug.WriteLine("❌ MODEL VALIDATION ERRORS:");
                    foreach (var state in ModelState)
                    {
                        if (state.Value.Errors.Count > 0)
                        {
                            Debug.WriteLine($"  Field: {state.Key}");
                            foreach (var error in state.Value.Errors)
                            {
                                Debug.WriteLine($"    Error: {error.ErrorMessage}");
                            }
                        }
                    }
                    Debug.WriteLine("--------------------------------------------");
                }

                if (ModelState.IsValid)
                {
                    // Generate unique route code
                    routePlan.RouteCode = await GenerateRouteCodeAsync();
                    Debug.WriteLine($"✓ Generated RouteCode: {routePlan.RouteCode}");

                    // Set system-generated fields
                    routePlan.Status = "Active";
                    routePlan.Progress = 0;
                    routePlan.StartTime = DateTime.UtcNow;
                    routePlan.CreatedAt = DateTime.UtcNow;

                    // Calculate distance if not provided or zero
                    if (routePlan.DistanceKm == null || routePlan.DistanceKm == 0)
                    {
                        routePlan.DistanceKm = CalculateEstimatedDistance(
                            routePlan.StartLocation,
                            routePlan.EndLocation
                        );
                        Debug.WriteLine($"✓ Calculated DistanceKm: {routePlan.DistanceKm}");
                    }

                    Debug.WriteLine("💾 Saving to database...");

                    // Add to database context
                    _context.RoutePlans.Add(routePlan);

                    // Save to database
                    int recordsSaved = await _context.SaveChangesAsync();

                    Debug.WriteLine($"✓ Records saved: {recordsSaved}");
                    Debug.WriteLine($"✓ New RoutePlanId: {routePlan.RoutePlanId}");

                    if (recordsSaved > 0)
                    {
                        TempData["SuccessMessage"] = $"✓ Route plan '{routePlan.RouteCode}' created successfully!";
                        Debug.WriteLine("✅ SUCCESS! Route plan saved to database");
                        Debug.WriteLine("============================================");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Debug.WriteLine("❌ ERROR: SaveChanges returned 0 records");
                        ModelState.AddModelError(string.Empty, "Failed to save route plan. Please try again.");
                    }
                }

                // Validation failed - reload form data
                Debug.WriteLine("🔄 Reloading form with validation errors...");

                ViewBag.Drivers = await _context.Drivers
                    .Where(d => d.Status == "Active")
                    .OrderBy(d => d.FullName)
                    .ToListAsync();

                ViewBag.Vehicles = await _context.Vehicles
                    .Where(v => v.Status == "Available" || v.Status == "In-Transit")
                    .OrderBy(v => v.RegistrationNumber)
                    .ToListAsync();

                return View(routePlan);
            }
            catch (DbUpdateException dbEx)
            {
                Debug.WriteLine("============================================");
                Debug.WriteLine("❌ DATABASE UPDATE EXCEPTION");
                Debug.WriteLine($"Message: {dbEx.Message}");
                Debug.WriteLine($"InnerException: {dbEx.InnerException?.Message}");
                Debug.WriteLine($"StackTrace: {dbEx.StackTrace}");
                Debug.WriteLine("============================================");

                ModelState.AddModelError(string.Empty,
                    $"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");

                // Reload form
                ViewBag.Drivers = await _context.Drivers
                    .Where(d => d.Status == "Active")
                    .ToListAsync();

                ViewBag.Vehicles = await _context.Vehicles
                    .Where(v => v.Status == "Available" || v.Status == "In-Transit")
                    .ToListAsync();

                return View(routePlan);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("============================================");
                Debug.WriteLine("❌ GENERAL EXCEPTION");
                Debug.WriteLine($"Type: {ex.GetType().Name}");
                Debug.WriteLine($"Message: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                Debug.WriteLine("============================================");

                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");

                // Reload form
                ViewBag.Drivers = await _context.Drivers
                    .Where(d => d.Status == "Active")
                    .ToListAsync();

                ViewBag.Vehicles = await _context.Vehicles
                    .Where(v => v.Status == "Available" || v.Status == "In-Transit")
                    .ToListAsync();

                return View(routePlan);
            }
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

            // Remove navigation property validation
            ModelState.Remove("Driver");
            ModelState.Remove("Vehicle");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routePlan);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Route plan updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutePlanExists(routePlan.RoutePlanId))
                    {
                        return NotFound();
                    }
                    throw;
                }
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

        // Helper: Check if route plan exists
        private bool RoutePlanExists(int id)
        {
            return _context.RoutePlans.Any(e => e.RoutePlanId == id);
        }

        // Helper: Generate unique route code (ASYNC VERSION)
        private async Task<string> GenerateRouteCodeAsync()
        {
            try
            {
                var lastRoute = await _context.RoutePlans
                    .OrderByDescending(r => r.RoutePlanId)
                    .FirstOrDefaultAsync();

                int nextNumber = 1;
                if (lastRoute != null && !string.IsNullOrEmpty(lastRoute.RouteCode) && lastRoute.RouteCode.StartsWith("RT"))
                {
                    string numberPart = lastRoute.RouteCode.Substring(2);
                    if (int.TryParse(numberPart, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }

                string routeCode = $"RT{nextNumber:D3}";
                Debug.WriteLine($"✓ Generated route code: {routeCode}");
                return routeCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"✗ Error generating route code: {ex.Message}");
                return "RT001";
            }
        }

        private decimal CalculateEstimatedDistance(string start, string end)
        {
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                return 500;

            var distances = new Dictionary<string, Dictionary<string, decimal>>
            {
                ["Johannesburg"] = new Dictionary<string, decimal>
                {
                    ["Cape Town"] = 1400,
                    ["Durban"] = 570,
                    ["Pretoria"] = 50,
                    ["Port Elizabeth"] = 1050,
                    ["Bloemfontein"] = 400,
                    ["Polokwane"] = 270,
                    ["Nelspruit"] = 330,
                    ["East London"] = 1000,
                    ["Kimberley"] = 480,
                    ["George"] = 1300
                },
                ["Cape Town"] = new Dictionary<string, decimal>
                {
                    ["Johannesburg"] = 1400,
                    ["Durban"] = 1650,
                    ["Pretoria"] = 1450,
                    ["Port Elizabeth"] = 770,
                    ["Bloemfontein"] = 1000,
                    ["George"] = 430,
                    ["East London"] = 1050,
                    ["Kimberley"] = 960
                },
                ["Durban"] = new Dictionary<string, decimal>
                {
                    ["Johannesburg"] = 570,
                    ["Cape Town"] = 1650,
                    ["Pretoria"] = 600,
                    ["Port Elizabeth"] = 700,
                    ["Bloemfontein"] = 680,
                    ["East London"] = 400,
                    ["Pietermaritzburg"] = 80
                },
                ["Pretoria"] = new Dictionary<string, decimal>
                {
                    ["Johannesburg"] = 50,
                    ["Cape Town"] = 1450,
                    ["Durban"] = 600,
                    ["Polokwane"] = 270,
                    ["Nelspruit"] = 350,
                    ["Bloemfontein"] = 430
                },
                ["Port Elizabeth"] = new Dictionary<string, decimal>
                {
                    ["Johannesburg"] = 1050,
                    ["Cape Town"] = 770,
                    ["Durban"] = 700,
                    ["East London"] = 300,
                    ["George"] = 330
                },
                ["Bloemfontein"] = new Dictionary<string, decimal>
                {
                    ["Johannesburg"] = 400,
                    ["Cape Town"] = 1000,
                    ["Durban"] = 680,
                    ["Pretoria"] = 430,
                    ["Kimberley"] = 160
                },
                ["Polokwane"] = new Dictionary<string, decimal>
                {
                    ["Johannesburg"] = 270,
                    ["Pretoria"] = 270,
                    ["Nelspruit"] = 450
                },
                ["Nelspruit"] = new Dictionary<string, decimal>
                {
                    ["Johannesburg"] = 330,
                    ["Pretoria"] = 350,
                    ["Polokwane"] = 450,
                    ["Durban"] = 520
                }
            };

            if (distances.ContainsKey(start) && distances[start].ContainsKey(end))
            {
                return distances[start][end];
            }

            if (distances.ContainsKey(end) && distances[end].ContainsKey(start))
            {
                return distances[end][start];
            }

            Debug.WriteLine($"⚠️ No distance data for {start} → {end}, using default 500km");
            return 500;
        }
    }
}
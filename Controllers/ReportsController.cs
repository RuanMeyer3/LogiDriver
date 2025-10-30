using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;

namespace LogiDriverPortal.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var completedRoutes = await _context.RoutePlans
                .Where(r => r.Status == "Completed")
                .CountAsync();

            var totalDrivers = await _context.Drivers.CountAsync();
            var activeDrivers = await _context.Drivers.CountAsync(d => d.Status == "Active");

            var totalVehicles = await _context.Vehicles.CountAsync();
            var availableVehicles = await _context.Vehicles.CountAsync(v => v.Status == "Available");

            ViewBag.CompletedRoutes = completedRoutes;
            ViewBag.TotalDrivers = totalDrivers;
            ViewBag.ActiveDrivers = activeDrivers;
            ViewBag.TotalVehicles = totalVehicles;
            ViewBag.AvailableVehicles = availableVehicles;

            return View();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LogiDriverPortal.Controllers
{
    [Authorize]
    public class LiveTrackingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LiveTrackingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /LiveTracking/Index
        public async Task<IActionResult> Index()
        {
            var activeRoutes = await _context.RoutePlans
                .Include(r => r.Driver)
                .Include(r => r.Vehicle)
                .Where(r => r.Status == "Active")
                .OrderBy(r => r.RouteCode)
                .ToListAsync();

            return View(activeRoutes);
        }

        // GET: /LiveTracking/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var route = await _context.RoutePlans
                .Include(r => r.Driver)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.RoutePlanId == id);

            if (route == null)
            {
                return NotFound();
            }

            // Get related alerts
            ViewBag.PanicEvents = await _context.PanicEvents
                .Where(p => p.RoutePlanId == id)
                .OrderByDescending(p => p.OccurredAt)
                .Take(5)
                .ToListAsync();

            ViewBag.DeviationAlerts = await _context.DeviationAlerts
                .Where(d => d.RoutePlanId == id)
                .OrderByDescending(d => d.DetectedAt)
                .Take(5)
                .ToListAsync();

            return View(route);
        }

        // API endpoint for real-time updates
        // GET: /LiveTracking/GetRouteStatus/5
        [HttpGet]
        public async Task<IActionResult> GetRouteStatus(int id)
        {
            var route = await _context.RoutePlans
                .Include(r => r.Driver)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.RoutePlanId == id);

            if (route == null)
            {
                return NotFound();
            }

            return Json(new
            {
                routeCode = route.RouteCode,
                progress = route.Progress,
                status = route.Status,
                driverName = route.Driver.FullName,
                fatigueLevel = route.Driver.FatigueLevel,
                vehicleReg = route.Vehicle.RegistrationNumber,
                eta = route.EstimatedArrival?.ToString("HH:mm"),
                currentLocation = route.Driver.CurrentLocation ?? "Unknown"
            });
        }
    }
}
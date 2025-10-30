using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using LogiDriverPortal.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogiDriverPortal.Controllers
{
    [Authorize]
    public class AlertsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var panicEvents = await _context.PanicEvents
                .Include(p => p.RoutePlan)
                .ThenInclude(r => r.Driver)
                .Include(p => p.RoutePlan.Vehicle)
                .OrderByDescending(p => p.OccurredAt)
                .ToListAsync();

            var deviationAlerts = await _context.DeviationAlerts
                .Include(d => d.RoutePlan)
                .ThenInclude(r => r.Driver)
                .Include(d => d.RoutePlan.Vehicle)
                .OrderByDescending(d => d.DetectedAt)
                .ToListAsync();

            ViewBag.DeviationAlerts = deviationAlerts;
            return View(panicEvents);
        }

        [HttpPost]
        public async Task<IActionResult> RespondToPanic(int id)
        {
            var panicEvent = await _context.PanicEvents.FindAsync(id);
            if (panicEvent != null)
            {
                panicEvent.Status = "responded";
                panicEvent.ResponseTime = DateTime.UtcNow;

                
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

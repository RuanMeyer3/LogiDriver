using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models.ViewModels;
using System.Linq; 
using System.Threading.Tasks; 

namespace LogiDriverPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Dashboard()
        {
           

            var viewModel = new DashboardViewModel
            {
                ActiveRoutes = await _context.RoutePlans.CountAsync(r => r.Status == "Active"),
                ActiveDrivers = await _context.Drivers.CountAsync(d => d.Status == "Active"),
                ActiveAlerts = await _context.PanicEvents.CountAsync(p => p.Status == "active") +
                                 await _context.DeviationAlerts.CountAsync(d => d.Status == "investigating"),
                OnTimeRate = 89,
                CriticalAlerts = await _context.PanicEvents
                    .Include(p => p.RoutePlan)
                    .ThenInclude(r => r.Driver)
                    .Where(p => p.Status == "active")
                    .OrderByDescending(p => p.OccurredAt)
                    .Take(5)
                    .ToListAsync(),
                ActiveRoutesList = await _context.RoutePlans
                    .Include(r => r.Driver)
                    .Include(r => r.Vehicle)
                    .Where(r => r.Status == "Active")
                    .OrderByDescending(r => r.StartTime)
                    .ToListAsync()
            };

            return View(viewModel);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using System.Linq;
using System.Threading.Tasks; 

namespace LogiDriverPortal.Controllers
{
    [Authorize]
    public class FleetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FleetController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.Vehicles
                .OrderBy(v => v.RegistrationNumber)
                .ToListAsync();

            return View(vehicles);
        }
    }
}

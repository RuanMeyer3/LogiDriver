using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models;
using LogiDriverPortal.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> AssignVehicle()
        {
            var viewModel = new AssignVehicleViewModel
            {
                Vehicles = new SelectList(await _context.Vehicles.ToListAsync(), "VehicleId", "RegistrationNumber"),
                Drivers = new SelectList(await _context.Drivers.ToListAsync(), "DriverId", "FullName")
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignVehicle(AssignVehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = await _context.Vehicles.FindAsync(viewModel.VehicleId);
                var driver = await _context.Drivers.FindAsync(viewModel.DriverId);

                if (vehicle != null && driver != null)
                {
                    vehicle.AssignedDriver = driver.FullName;
                    driver.AssignedVehicle = vehicle.RegistrationNumber;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Invalid vehicle or driver.");
            }

            viewModel.Vehicles = new SelectList(await _context.Vehicles.ToListAsync(), "VehicleId", "RegistrationNumber", viewModel.VehicleId);
            viewModel.Drivers = new SelectList(await _context.Drivers.ToListAsync(), "DriverId", "FullName", viewModel.DriverId);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleMaintenance()
        {
            var viewModel = new ScheduleMaintenanceViewModel
            {
                Vehicles = new SelectList(await _context.Vehicles.ToListAsync(), "VehicleId", "RegistrationNumber")
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ScheduleMaintenance(ScheduleMaintenanceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = await _context.Vehicles.FindAsync(viewModel.VehicleId);

                if (vehicle != null)
                {
                    vehicle.NextService = viewModel.NextService;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Invalid vehicle.");
            }

            viewModel.Vehicles = new SelectList(await _context.Vehicles.ToListAsync(), "VehicleId", "RegistrationNumber", viewModel.VehicleId);
            return View(viewModel);
        }
    }
}

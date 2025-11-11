using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models;
using LogiDriverPortal.Models.ViewModels;
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

        public async Task<IActionResult> DriverMap()
        {
            var driverData = await GetDriverLocationsData();
            return View(driverData);
        }

        [HttpGet]
        public async Task<IActionResult> GetDriverLocations()
        {
            var driverData = await GetDriverLocationsData();
            return Json(driverData);
        }

        private async Task<List<DriverMapViewModel>> GetDriverLocationsData()
        {
            var driversWithLocations = await _context.Drivers
                .Where(d => d.Status == "Active")
                .Select(d => new
                {
                    Driver = d,
                    Locations = _context.DriverLocations
                        .Where(dl => dl.DriverId == d.DriverId)
                        .OrderByDescending(dl => dl.Timestamp)
                        .Take(50) 
                        .ToList()
                })
                .Where(x => x.Locations.Any())
                .ToListAsync();

            var result = driversWithLocations.Select(d => new DriverMapViewModel
            {
                DriverId = d.Driver.DriverId,
                DriverName = d.Driver.FullName,
                DriverCode = d.Driver.DriverCode,
                VehicleRegistration = d.Driver.AssignedVehicle ?? "N/A",
                Latest = d.Locations.First(),
                Route = d.Locations.OrderBy(l => l.Timestamp).ToList(),
                FatigueLevel = d.Driver.FatigueLevel,
                Status = d.Driver.Status
            }).ToList();

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetDriverLocation(int driverId)
        {
            var latest = await _context.DriverLocations
                .Where(dl => dl.DriverId == driverId)
                .OrderByDescending(dl => dl.Timestamp)
                .FirstOrDefaultAsync();

            if (latest == null)
            {
                return NotFound();
            }

            return Json(new
            {
                driverId = latest.DriverId,
                latitude = latest.Latitude,
                longitude = latest.Longitude,
                timestamp = latest.Timestamp,
                speed = latest.Speed,
                heading = latest.Heading
            });
        }

        [HttpPost]
        public async Task<IActionResult> SimulateMovement()
        {
            var random = new Random();
            var drivers = await _context.Drivers
                .Where(d => d.Status == "Active")
                .Take(5)
                .ToListAsync();

            foreach (var driver in drivers)
            {
                var lastLocation = await _context.DriverLocations
                    .Where(dl => dl.DriverId == driver.DriverId)
                    .OrderByDescending(dl => dl.Timestamp)
                    .FirstOrDefaultAsync();

                double lat = lastLocation?.Latitude ?? (-26.7 + random.NextDouble() * 0.5);
                double lng = lastLocation?.Longitude ?? (27.0 + random.NextDouble() * 0.5);

                lat += (random.NextDouble() - 0.5) * 0.001;
                lng += (random.NextDouble() - 0.5) * 0.001;

                var newLocation = new DriverLocation
                {
                    DriverId = driver.DriverId,
                    Latitude = lat,
                    Longitude = lng,
                    Timestamp = DateTime.UtcNow,
                    Speed = random.Next(60, 120),
                    Heading = random.Next(0, 360)
                };

                _context.DriverLocations.Add(newLocation);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Locations updated" });
        }
    }
}
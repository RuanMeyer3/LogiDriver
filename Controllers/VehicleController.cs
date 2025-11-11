using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

public class VehicleController : Controller
{
    private readonly ApplicationDbContext _context;

    public VehicleController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> AssignVehicle()
    {
        var model = new AssignVehicleViewModel
        {
            Vehicles = new SelectList(
                _context.Vehicles
                    .Where(v => v.AssignedDriver == null) 
                    .OrderBy(v => v.RegistrationNumber)
                    .ToList(),
                "VehicleId",
                "RegistrationNumber"
            ),

            Drivers = new SelectList(
                _context.Drivers
                    .Where(d => d.FullName == "Ruan Meyer") 
                    .OrderBy(d => d.FullName)
                    .ToList(),
                "DriverId",
                "FullName"
            )
        };

        return View("~/Views/Fleet/AssignVehicle.cshtml", model);

    }
}

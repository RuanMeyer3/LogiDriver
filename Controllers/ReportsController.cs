using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiDriverPortal.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<IActionResult> ExportCsv()
        {
            var drivers = await _context.Drivers.ToListAsync();
            var builder = new StringBuilder();
            builder.AppendLine("DriverId,FullName,DriverCode,Phone,Status,AssignedVehicle");

            foreach (var driver in drivers)
            {
                builder.AppendLine($"{driver.DriverId},{driver.FullName},{driver.DriverCode},{driver.Phone},{driver.Status},{driver.AssignedVehicle}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "drivers.csv");
        }

        public IActionResult ExportPdf()
        {
            // In a real application, you would use a PDF generation library (e.g., iTextSharp, QuestPDF)
            // to create a professional PDF report from your data.
            // For now, this is a mock implementation that returns a plain text file.

            var content = "Mock PDF Report\n\nThis is a placeholder for a PDF report. " +
                          "Please integrate a PDF generation library for actual PDF export functionality.";

            var fileName = "report.pdf";
            var contentType = "application/pdf"; // Correct content type for PDF

            return File(Encoding.UTF8.GetBytes(content), contentType, fileName);
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Note: Keeping this clean as you removed the ModelBinding namespace
// You must continue to use ModelState.Remove("Driver"); and 
// ModelState.Remove("Vehicle"); in the controller POST method.

namespace LogiDriverPortal.Models
{
    public class RoutePlan
    {
        [Key]
        public int RoutePlanId { get; set; }

        [StringLength(20)]
        public string? RouteCode { get; set; } // <-- FIX: Mark nullable for older data/DB NULLs

        // Foreign keys are fine and required by the form
        [Required(ErrorMessage = "Please select a driver")]
        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        [Required(ErrorMessage = "Please select a vehicle")]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        // Start/End location are [Required] but should not be nullable in the DB either.
        [Required(ErrorMessage = "Start location is required")]
        [StringLength(100)]
        [Display(Name = "Start Location")]
        public string StartLocation { get; set; }

        [Required(ErrorMessage = "End location is required")]
        [StringLength(100)]
        [Display(Name = "End Location")]
        public string EndLocation { get; set; }

        [StringLength(500)]
        public string? Waypoints { get; set; } // <-- FIX: Mark nullable to handle DB NULLs

        public decimal? DistanceKm { get; set; }

        public int Progress { get; set; } = 0;

        public DateTime? EstimatedArrival { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [StringLength(20)]
        public string? Status { get; set; } // <-- FIX: Mark nullable to handle DB NULLs

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public string RouteDescription => $"{StartLocation ?? "?"} → {EndLocation ?? "?"}";
    }
}
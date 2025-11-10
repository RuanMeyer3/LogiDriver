using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding; // <--- This namespace is essential for [BindNever]

namespace LogiDriverPortal.Models
{
    public class RoutePlan
    {
        [Key]
        public int RoutePlanId { get; set; }

        [BindNever] // <-- FIX 1: Ignore RouteCode in validation, as it's set by the controller.
        [Required]
        [StringLength(20)]
        public string RouteCode { get; set; }

        [Required] // DriverId is REQUIRED by the form dropdown
        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        [BindNever] // <-- FIX 2: Ignore the navigation property to prevent validation failure.
        public Driver Driver { get; set; }

        [Required] // VehicleId is REQUIRED by the form dropdown
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [BindNever] // <-- FIX 3: Ignore the navigation property to prevent validation failure.
        public Vehicle Vehicle { get; set; }

        // Note: You should check if StartLocation and EndLocation should be [Required]
        // If the route must have a start/end, add [Required] to these fields as well.
        [StringLength(100)]
        public string? StartLocation { get; set; }

        [StringLength(100)]
        public string? EndLocation { get; set; }

        [StringLength(500)]
        public string? Waypoints { get; set; }

        public decimal? DistanceKm { get; set; }

        [BindNever] // <-- Controller sets this to 0 upon creation
        public int Progress { get; set; } = 0;

        public DateTime? EstimatedArrival { get; set; }

        [BindNever] // <-- Controller sets this to DateTime.UtcNow
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [BindNever] // <-- Controller sets this to "Active"
        [StringLength(20)]
        public string Status { get; set; } = "Active";

        [BindNever] // <-- Controller sets this to DateTime.UtcNow
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public string RouteDescription => $"{StartLocation ?? "?"} → {EndLocation ?? "?"}";
    }
}
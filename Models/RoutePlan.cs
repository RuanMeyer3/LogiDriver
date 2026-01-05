using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LogiDriverPortal.Models
{
    public class RoutePlan
    {
        [Key]
        public int RoutePlanId { get; set; }

        [StringLength(20)]
        public string? RouteCode { get; set; }

        [Required(ErrorMessage = "Please select a driver")]
        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        [Required(ErrorMessage = "Please select a vehicle")]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required(ErrorMessage = "Start location is required")]
        [StringLength(100)]
        [Display(Name = "Start Location")]
        public string StartLocation { get; set; }

        [Required(ErrorMessage = "End location is required")]
        [StringLength(100)]
        [Display(Name = "End Location")]
        public string EndLocation { get; set; }

        [StringLength(500)]
        public string? Waypoints { get; set; }

        public decimal? DistanceKm { get; set; }

        public int Progress { get; set; } = 0;

        public DateTime? EstimatedArrival { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [StringLength(20)]
        public string? Status { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public string RouteDescription => $"{StartLocation ?? "?"} → {EndLocation ?? "?"}";
    }
}
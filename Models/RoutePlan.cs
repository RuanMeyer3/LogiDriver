using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiDriverPortal.Models
{
    public class RoutePlan
    {
        [Key]
        public int RoutePlanId { get; set; }

        [Required]
        [StringLength(20)]
        public string RouteCode { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        public string RouteDescription { get; set; }

        public int Progress { get; set; } = 0; // 0-100%

        public DateTime? EstimatedArrival { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        public string Status { get; set; } = "Active"; // Active, Completed, Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
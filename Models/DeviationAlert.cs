using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiDriverPortal.Models
{
    public class DeviationAlert
    {
        [Key]
        public int DeviationAlertId { get; set; }

        [ForeignKey("RoutePlan")]
        public int RoutePlanId { get; set; }
        public RoutePlan RoutePlan { get; set; }

        public DateTime DetectedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string Reason { get; set; }

        public string Location { get; set; }

        public string Status { get; set; } = "investigating"; 

        public DateTime? ResolvedAt { get; set; }
    }
}
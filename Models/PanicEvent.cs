using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiDriverPortal.Models
{
    public class PanicEvent
    {
        [Key]
        public int PanicEventId { get; set; }

        [ForeignKey("RoutePlan")]
        public int RoutePlanId { get; set; }
        public RoutePlan RoutePlan { get; set; }

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string Severity { get; set; } = "critical"; 

        public string Location { get; set; }

        public string Status { get; set; } = "active"; 

        public DateTime? ResponseTime { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiDriverPortal.Models
{
    public class DeliveryOrder
    {
        [Key]
        [Column("do_id")] 
        public int DoId { get; set; }

      
        [Column("route_plan_id")]
        public int? RoutePlanId { get; set; }
       

        [Column("customer_name")]
        [StringLength(100)]
        public string CustomerName { get; set; } = "Driver Destination"; 

        [Column("address")]
        [Required]
        [StringLength(255)]
        public string Address { get; set; } 

        [Column("notes")]
        [StringLength(500)]
        public string? Notes { get; set; }

        [Column("status")]
        [StringLength(20)]
        public string Status { get; set; } = "Pending"; 
    }
}
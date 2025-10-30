using System.ComponentModel.DataAnnotations;

namespace LogiDriverPortal.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string DriverCode { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public int FatigueLevel { get; set; } = 0;

        [Required]
        public string Status { get; set; } = "Active"; // Active, On Break, Off Duty

        public string? AssignedVehicle { get; set; }

        public string? CurrentLocation { get; set; }

        public DateTime? LastAlertTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
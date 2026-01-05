using System.ComponentModel.DataAnnotations;

namespace LogiDriverPortal.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        [StringLength(20)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string MakeModel { get; set; }

        public int Year { get; set; }

        public decimal Mileage { get; set; }

        public string? AssignedDriver { get; set; }

        [Required]
        public string Status { get; set; } = "Available";

        public DateTime? LastService { get; set; }

        public DateTime? NextService { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
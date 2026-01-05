using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // MUST be included for [NotMapped]

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


        [NotMapped]
        [Required(ErrorMessage = "Email is required for login.")]
        [EmailAddress]
        public string Email { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }
    }
}
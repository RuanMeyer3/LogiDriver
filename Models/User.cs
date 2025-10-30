using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LogiDriverPortal.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; } = "Supervisor";

        [Required]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
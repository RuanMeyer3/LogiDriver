using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiDriverPortal.Models
{
    public class DriverLocation
    {
        [Key]
        public int LocationId { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Optional: Speed in km/h
        public double? Speed { get; set; }

        // Optional: Heading/Direction (0-360 degrees)
        public double? Heading { get; set; }
    }
}
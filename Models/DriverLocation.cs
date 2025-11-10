using System.ComponentModel.DataAnnotations;


namespace LogiDriverPortal.Models
{
    public class DriverLocation
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsActive { get; set; }
    }
}
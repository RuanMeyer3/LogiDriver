namespace LogiDriverPortal.Models.ViewModels
{
    public class DriverMapViewModel
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverCode { get; set; }
        public string VehicleRegistration { get; set; }
        public DriverLocation Latest { get; set; }
        public List<DriverLocation> Route { get; set; }
        public int FatigueLevel { get; set; }
        public string Status { get; set; }
    }

    public class DriverLocationDto
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverCode { get; set; }
        public string VehicleRegistration { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public int FatigueLevel { get; set; }
        public string Status { get; set; }
        public List<LocationPoint> Route { get; set; }
    }

    public class LocationPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
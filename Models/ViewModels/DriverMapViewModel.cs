namespace LogiDriverPortal.Models.ViewModels
{
    public class DriverMapViewModel
    {
        public int DriverId { get; set; }
        public DriverLocation Latest { get; set; }
        public List<DriverLocation> Route { get; set; }
    }
}

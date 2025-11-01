using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LogiDriverPortal.Models.ViewModels
{
    public class AssignVehicleViewModel
    {
        public int VehicleId { get; set; }
        public int DriverId { get; set; }

        public SelectList Vehicles { get; set; }
        public SelectList Drivers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LogiDriverPortal.Models.ViewModels
{
    public class ScheduleMaintenanceViewModel
    {
        public int VehicleId { get; set; }
        public DateTime NextService { get; set; }

        public SelectList Vehicles { get; set; }
    }
}

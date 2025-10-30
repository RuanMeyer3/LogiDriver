using System.Collections.Generic;
using LogiDriverPortal.Models; 

namespace LogiDriverPortal.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int ActiveRoutes { get; set; }
        public int ActiveDrivers { get; set; }
        public int ActiveAlerts { get; set; }
        public int OnTimeRate { get; set; }

        
        public List<PanicEvent> CriticalAlerts { get; set; }

        
        public List<RoutePlan> ActiveRoutesList { get; set; }
    }
}

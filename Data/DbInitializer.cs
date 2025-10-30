using LogiDriverPortal.Models;
using System;
using System.Linq;

namespace LogiDriverPortal.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Check if already seeded
            if (context.Drivers.Any())
            {
                return; // Database has been seeded
            }

            // Seed Drivers
            var drivers = new Driver[]
            {
                new Driver
                {
                    FullName = "Thabo Mthembu",
                    DriverCode = "DRV001",
                    Phone = "+27 82 345 6789",
                    FatigueLevel = 32,
                    Status = "Active",
                    AssignedVehicle = "VH-2341-GP",
                    CurrentLocation = "JHB-PTA-001"
                },
                new Driver
                {
                    FullName = "Sarah van der Merwe",
                    DriverCode = "DRV002",
                    Phone = "+27 83 456 7890",
                    FatigueLevel = 68,
                    Status = "Active",
                    AssignedVehicle = "VH-8821-GP",
                    CurrentLocation = "JHB-DBN-045",
                    LastAlertTime = DateTime.UtcNow.AddHours(-2)
                },
                new Driver
                {
                    FullName = "Lerato Ndlovu",
                    DriverCode = "DRV003",
                    Phone = "+27 84 567 8901",
                    FatigueLevel = 45,
                    Status = "Active",
                    AssignedVehicle = "VH-5532-GP",
                    CurrentLocation = "JHB-CPT-112"
                },
                new Driver
                {
                    FullName = "Pieter Botha",
                    DriverCode = "DRV004",
                    Phone = "+27 73 678 9012",
                    FatigueLevel = 12,
                    Status = "On Break"
                },
                new Driver
                {
                    FullName = "Zanele Khumalo",
                    DriverCode = "DRV005",
                    Phone = "+27 72 789 0123",
                    FatigueLevel = 89,
                    Status = "Active",
                    AssignedVehicle = "VH-9943-GP",
                    CurrentLocation = "JHB-ELS-023"
                }
            };
            context.Drivers.AddRange(drivers);
            context.SaveChanges();

            // Seed Vehicles
            var vehicles = new Vehicle[]
            {
                new Vehicle
                {
                    RegistrationNumber = "VH-2341-GP",
                    MakeModel = "Volvo FH16",
                    Year = 2022,
                    Mileage = 145232,
                    AssignedDriver = "Thabo Mthembu",
                    Status = "In-Transit",
                    LastService = DateTime.Parse("2025-09-15"),
                    NextService = DateTime.Parse("2025-11-15")
                },
                new Vehicle
                {
                    RegistrationNumber = "VH-8821-GP",
                    MakeModel = "Mercedes-Benz Actros",
                    Year = 2023,
                    Mileage = 89421,
                    AssignedDriver = "Sarah van der Merwe",
                    Status = "In-Transit",
                    LastService = DateTime.Parse("2025-10-01"),
                    NextService = DateTime.Parse("2025-12-01")
                },
                new Vehicle
                {
                    RegistrationNumber = "VH-5532-GP",
                    MakeModel = "Scania R500",
                    Year = 2021,
                    Mileage = 203145,
                    AssignedDriver = "Lerato Ndlovu",
                    Status = "In-Transit",
                    LastService = DateTime.Parse("2025-08-20"),
                    NextService = DateTime.Parse("2025-10-20")
                },
                new Vehicle
                {
                    RegistrationNumber = "VH-9943-GP",
                    MakeModel = "MAN TGX",
                    Year = 2023,
                    Mileage = 52341,
                    AssignedDriver = "Zanele Khumalo",
                    Status = "In-Transit",
                    LastService = DateTime.Parse("2025-10-10"),
                    NextService = DateTime.Parse("2025-12-10")
                },
                new Vehicle
                {
                    RegistrationNumber = "VH-7762-GP",
                    MakeModel = "Volvo FH16",
                    Year = 2020,
                    Mileage = 312456,
                    Status = "Available",
                    LastService = DateTime.Parse("2025-09-25"),
                    NextService = DateTime.Parse("2025-11-25")
                }
            };
            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();

            // Seed Route Plans
            var routes = new RoutePlan[]
            {
                new RoutePlan
                {
                    RouteCode = "RT001",
                    DriverId = 1,
                    VehicleId = 1,
                    RouteDescription = "Johannesburg → Durban",
                    Progress = 65,
                    EstimatedArrival = DateTime.UtcNow.AddHours(4),
                    StartTime = DateTime.UtcNow.AddHours(-6),
                    Status = "Active"
                },
                new RoutePlan
                {
                    RouteCode = "RT002",
                    DriverId = 2,
                    VehicleId = 2,
                    RouteDescription = "Cape Town → Port Elizabeth",
                    Progress = 42,
                    EstimatedArrival = DateTime.UtcNow.AddHours(6),
                    StartTime = DateTime.UtcNow.AddHours(-3),
                    Status = "Active"
                },
                new RoutePlan
                {
                    RouteCode = "RT003",
                    DriverId = 3,
                    VehicleId = 3,
                    RouteDescription = "Pretoria → Bloemfontein",
                    Progress = 78,
                    EstimatedArrival = DateTime.UtcNow.AddHours(2),
                    StartTime = DateTime.UtcNow.AddHours(-5),
                    Status = "Active"
                }
            };
            context.RoutePlans.AddRange(routes);
            context.SaveChanges();

            // Seed Panic Events
            var panicEvents = new PanicEvent[]
            {
                new PanicEvent
                {
                    RoutePlanId = 1,
                    Severity = "critical",
                    Location = "N3, KZN",
                    OccurredAt = DateTime.UtcNow.AddMinutes(-15),
                    Status = "active"
                },
                new PanicEvent
                {
                    RoutePlanId = 1,
                    Severity = "high",
                    Location = "N3, KZN",
                    OccurredAt = DateTime.UtcNow.AddMinutes(-15),
                    Status = "active"
                }
            };
            context.PanicEvents.AddRange(panicEvents);

            // Seed Deviation Alerts
            var deviations = new DeviationAlert[]
            {
                new DeviationAlert
                {
                    RoutePlanId = 2,
                    Reason = "Route deviation detected",
                    Location = "N2, Eastern Cape",
                    DetectedAt = DateTime.UtcNow.AddHours(-2),
                    Status = "investigating"
                }
            };
            context.DeviationAlerts.AddRange(deviations);

            context.SaveChanges();
        }
    }
}

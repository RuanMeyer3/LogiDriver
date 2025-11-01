-- ========================================
-- DATABASE INITIALIZATION
-- ========================================

CREATE DATABASE IF NOT EXISTS logidriver_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE logidriver_db;

-- ========================================
-- ASP.NET Identity Core Tables
-- ========================================

CREATE TABLE aspnetusers (
    Id VARCHAR(255) PRIMARY KEY,
    UserName VARCHAR(256),
    NormalizedUserName VARCHAR(256),
    Email VARCHAR(256),
    NormalizedEmail VARCHAR(256),
    EmailConfirmed TINYINT(1) NOT NULL DEFAULT 0,
    PasswordHash LONGTEXT,
    SecurityStamp LONGTEXT,
    ConcurrencyStamp LONGTEXT,
    PhoneNumber TEXT,
    PhoneNumberConfirmed TINYINT(1) NOT NULL DEFAULT 0,
    TwoFactorEnabled TINYINT(1) NOT NULL DEFAULT 0,
    LockoutEnd DATETIME(6),
    LockoutEnabled TINYINT(1) NOT NULL DEFAULT 0,
    AccessFailedCount INT NOT NULL DEFAULT 0,
    FullName VARCHAR(100) NOT NULL,
    Role VARCHAR(50) NOT NULL DEFAULT 'Supervisor',
    Status VARCHAR(20) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    INDEX IX_AspNetUsers_NormalizedUserName (NormalizedUserName),
    INDEX IX_AspNetUsers_NormalizedEmail (NormalizedEmail)
) ENGINE=InnoDB;

CREATE TABLE aspnetroles (
    Id VARCHAR(255) PRIMARY KEY,
    Name VARCHAR(256),
    NormalizedName VARCHAR(256),
    ConcurrencyStamp LONGTEXT,
    INDEX IX_AspNetRoles_NormalizedName (NormalizedName)
) ENGINE=InnoDB;

CREATE TABLE aspnetuserroles (
    UserId VARCHAR(255) NOT NULL,
    RoleId VARCHAR(255) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES aspnetusers(Id) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES aspnetroles(Id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE aspnetuserclaims (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId VARCHAR(255) NOT NULL,
    ClaimType LONGTEXT,
    ClaimValue LONGTEXT,
    FOREIGN KEY (UserId) REFERENCES aspnetusers(Id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE aspnetroleclaims (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RoleId VARCHAR(255) NOT NULL,
    ClaimType LONGTEXT,
    ClaimValue LONGTEXT,
    FOREIGN KEY (RoleId) REFERENCES aspnetroles(Id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE aspnetuserlogins (
    LoginProvider VARCHAR(128) NOT NULL,
    ProviderKey VARCHAR(128) NOT NULL,
    ProviderDisplayName LONGTEXT,
    UserId VARCHAR(255) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey),
    FOREIGN KEY (UserId) REFERENCES aspnetusers(Id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE aspnetusertokens (
    UserId VARCHAR(255) NOT NULL,
    LoginProvider VARCHAR(128) NOT NULL,
    Name VARCHAR(128) NOT NULL,
    Value LONGTEXT,
    PRIMARY KEY (UserId, LoginProvider, Name),
    FOREIGN KEY (UserId) REFERENCES aspnetusers(Id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- ========================================
-- CORE BUSINESS TABLES (from Project Phases 2–4)
-- ========================================

CREATE TABLE drivers (
    DriverId INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    DriverCode VARCHAR(20) UNIQUE NOT NULL,
    Phone VARCHAR(20),
    FatigueLevel INT NOT NULL DEFAULT 0 CHECK (FatigueLevel BETWEEN 0 AND 100),
    Status VARCHAR(20) NOT NULL DEFAULT 'Active',
    AssignedVehicle VARCHAR(20),
    CurrentLocation VARCHAR(255),
    LastAlertTime DATETIME(6),
    CreatedAt DATETIME(6) DEFAULT CURRENT_TIMESTAMP(6),
    INDEX IX_Drivers_Status (Status),
    INDEX IX_Drivers_Code (DriverCode)
) ENGINE=InnoDB;

CREATE TABLE vehicles (
    VehicleId INT AUTO_INCREMENT PRIMARY KEY,
    RegistrationNumber VARCHAR(20) UNIQUE NOT NULL,
    MakeModel VARCHAR(100),
    Year INT,
    Mileage DECIMAL(10,2) DEFAULT 0,
    AssignedDriver VARCHAR(100),
    Status VARCHAR(20) DEFAULT 'Available',
    LastService DATE,
    NextService DATE,
    CreatedAt DATETIME(6) DEFAULT CURRENT_TIMESTAMP(6),
    INDEX IX_Vehicles_Status (Status)
) ENGINE=InnoDB;

CREATE TABLE routeplans (
    RoutePlanId INT AUTO_INCREMENT PRIMARY KEY,
    RouteCode VARCHAR(20) UNIQUE NOT NULL,
    DriverId INT NOT NULL,
    VehicleId INT NOT NULL,
    RouteDescription VARCHAR(255),
    Progress INT DEFAULT 0 CHECK (Progress BETWEEN 0 AND 100),
    EstimatedArrival DATETIME(6),
    StartTime DATETIME(6),
    EndTime DATETIME(6),
    Status VARCHAR(20) DEFAULT 'Active',
    CreatedAt DATETIME(6) DEFAULT CURRENT_TIMESTAMP(6),
    FOREIGN KEY (DriverId) REFERENCES drivers(DriverId),
    FOREIGN KEY (VehicleId) REFERENCES vehicles(VehicleId)
) ENGINE=InnoDB;

CREATE TABLE deviationalerts (
    DeviationAlertId INT AUTO_INCREMENT PRIMARY KEY,
    RoutePlanId INT NOT NULL,
    DetectedAt DATETIME(6) DEFAULT CURRENT_TIMESTAMP(6),
    Reason VARCHAR(255),
    Location VARCHAR(255),
    Severity ENUM('Low','Medium','High') DEFAULT 'Medium',
    Status VARCHAR(20) DEFAULT 'Investigating',
    ResolvedAt DATETIME(6),
    FOREIGN KEY (RoutePlanId) REFERENCES routeplans(RoutePlanId)
) ENGINE=InnoDB;

CREATE TABLE panicevents (
    PanicEventId INT AUTO_INCREMENT PRIMARY KEY,
    RoutePlanId INT NOT NULL,
    OccurredAt DATETIME(6) DEFAULT CURRENT_TIMESTAMP(6),
    Severity ENUM('Low','Medium','High','Critical') DEFAULT 'Critical',
    Location VARCHAR(255),
    Status VARCHAR(20) DEFAULT 'Active',
    ResponseTime DATETIME(6),
    FOREIGN KEY (RoutePlanId) REFERENCES routeplans(RoutePlanId)
) ENGINE=InnoDB;

CREATE TABLE gps_points (
    GpsId BIGINT AUTO_INCREMENT PRIMARY KEY,
    RoutePlanId INT,
    DriverId INT,
    VehicleId INT,
    Latitude DECIMAL(10,7),
    Longitude DECIMAL(10,7),
    Speed DECIMAL(6,2),
    Heading DECIMAL(6,2),
    Timestamp DATETIME(6) DEFAULT CURRENT_TIMESTAMP(6),
    FOREIGN KEY (RoutePlanId) REFERENCES routeplans(RoutePlanId),
    INDEX idx_gps_route_ts (RoutePlanId, Timestamp)
) ENGINE=InnoDB;

-- ========================================
-- REPORTING VIEWS
-- ========================================

CREATE OR REPLACE VIEW vw_active_routes AS
SELECT rp.RouteCode, rp.RouteDescription, rp.Progress, rp.Status,
       rp.EstimatedArrival, d.FullName AS DriverName, v.RegistrationNumber
FROM routeplans rp
JOIN drivers d ON rp.DriverId = d.DriverId
JOIN vehicles v ON rp.VehicleId = v.VehicleId
WHERE rp.Status = 'Active';

CREATE OR REPLACE VIEW vw_alert_summary AS
SELECT 'Panic' AS AlertType, pe.PanicEventId AS AlertId,
       pe.Severity, pe.Location, pe.Status, pe.OccurredAt, rp.RouteCode,
       d.FullName AS DriverName
FROM panicevents pe
JOIN routeplans rp ON pe.RoutePlanId = rp.RoutePlanId
JOIN drivers d ON rp.DriverId = d.DriverId
UNION ALL
SELECT 'Deviation', da.DeviationAlertId, da.Severity,
       da.Location, da.Status, da.DetectedAt, rp.RouteCode, d.FullName
FROM deviationalerts da
JOIN routeplans rp ON da.RoutePlanId = rp.RoutePlanId
JOIN drivers d ON rp.DriverId = d.DriverId
ORDER BY OccurredAt DESC;

-- ========================================
-- STORED PROCEDURES
-- ========================================

DELIMITER //

CREATE PROCEDURE sp_update_route_progress(IN p_route_id INT, IN p_progress INT)
BEGIN
    UPDATE routeplans
    SET Progress = p_progress,
        Status = CASE WHEN p_progress >= 100 THEN 'Completed' ELSE Status END,
        EndTime = CASE WHEN p_progress >= 100 THEN NOW() ELSE EndTime END
    WHERE RoutePlanId = p_route_id;
END //

CREATE PROCEDURE sp_respond_to_panic(IN p_panic_id INT)
BEGIN
    UPDATE panicevents
    SET Status = 'Responded', ResponseTime = NOW()
    WHERE PanicEventId = p_panic_id;
END //

CREATE PROCEDURE sp_get_dashboard_stats()
BEGIN
    SELECT
        (SELECT COUNT(*) FROM routeplans WHERE Status='Active') AS ActiveRoutes,
        (SELECT COUNT(*) FROM drivers WHERE Status='Active') AS ActiveDrivers,
        (SELECT COUNT(*) FROM panicevents WHERE Status='Active') +
        (SELECT COUNT(*) FROM deviationalerts WHERE Status='Investigating') AS ActiveAlerts;
END //

DELIMITER ;

-- ========================================
-- SAMPLE DATA
-- ========================================

INSERT INTO aspnetusers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, FullName, Role, Status)
VALUES (UUID(), 'supervisor@logidriver.com', 'SUPERVISOR@LOGIDRIVER.COM', 'supervisor@logidriver.com', 'SUPERVISOR@LOGIDRIVER.COM', 1, 'System Supervisor', 'Supervisor', 'Active');

INSERT INTO drivers (FullName, DriverCode, Phone, FatigueLevel, Status)
VALUES
('Thabo Mthembu','DRV001','+27 82 345 6789',30,'Active'),
('Sarah van der Merwe','DRV002','+27 83 456 7890',45,'Active');

INSERT INTO vehicles (RegistrationNumber, MakeModel, Year, Mileage, Status)
VALUES
('VH-2341-GP','Volvo FH16',2022,145000,'In-Transit'),
('VH-8821-GP','Mercedes-Benz Actros',2023,87000,'In-Transit');

INSERT INTO routeplans (RouteCode, DriverId, VehicleId, RouteDescription, Progress, StartTime, Status)
VALUES
('RT001',1,1,'Johannesburg → Durban',60,DATE_SUB(NOW(),INTERVAL 4 HOUR),'Active'),
('RT002',2,2,'Cape Town → Port Elizabeth',40,DATE_SUB(NOW(),INTERVAL 2 HOUR),'Active');

INSERT INTO panicevents (RoutePlanId, Severity, Location, Status)
VALUES
(1,'Critical','N3 Highway, KZN','Active');

INSERT INTO deviationalerts (RoutePlanId, Reason, Location, Severity, Status)
VALUES
(2,'Deviation detected','N2 Eastern Cape','High','Investigating');

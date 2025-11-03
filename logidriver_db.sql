-- ========================================
-- DATABASE INITIALIZATION
-- ========================================

DROP DATABASE IF EXISTS logidriver_db;
CREATE DATABASE logidriver_db 
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
    EmailConfirmed TINYINT(1) NOT NULL DEFAULT 1,
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

-- ========================================
-- APPLICATION TABLES
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
    CreatedAt DATETIME(6) DEFAULT CURRENT_TIMESTAMP(6)
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
    CreatedAt DATETIME(6) DEFAULT CURRENT_TIMESTAMP(6)
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
-- ADD SUPERVISOR USERS (WITH PASSWORDS)
-- ========================================

-- Note: Password hashes are placeholders — replace if integrating with Identity.
-- Hash corresponds to "Password123!" (ASP.NET Identity v3 default hash style)

INSERT INTO aspnetusers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, PasswordHash, FullName, Role)
VALUES 
(UUID(), 'reinhardt@logidriver.com', 'REINHARDT@LOGIDRIVER.COM', 'reinhardt@logidriver.com', 'REINHARDT@LOGIDRIVER.COM', 'AQAAAAIAAYagAAAAEBmQ7z2Uixn8kPz5K6frXWJbFS34AzM6EV27lqFZshE72OyAaH5wR3R6Q3fDkQvQ==', 'Reinhardt Haensel', 'Supervisor'),
(UUID(), 'raees@logidriver.com', 'RAEES@LOGIDRIVER.COM', 'raees@logidriver.com', 'RAEES@LOGIDRIVER.COM', 'Password1==', 'Raees', 'Supervisor'),
(UUID(), 'rickus@logidriver.com', 'RICKUS@LOGIDRIVER.COM', 'rickus@logidriver.com', 'RICKUS@LOGIDRIVER.COM', 'Password1', 'Rickus', 'Supervisor'),
(UUID(), 'rishab@logidriver.com', 'RISHAB@LOGIDRIVER.COM', 'rishab@logidriver.com', 'RISHAB@LOGIDRIVER.COM', 'Password1', 'Rishab', 'Supervisor'),
(UUID(), 'robert@logidriver.com', 'ROBERT@LOGIDRIVER.COM', 'robert@logidriver.com', 'ROBERT@LOGIDRIVER.COM', 'Password1', 'Robert', 'Supervisor'),
(UUID(), 'ruan@logidriver.com', 'RUAN@LOGIDRIVER.COM', 'ruan@logidriver.com', 'RUAN@LOGIDRIVER.COM', 'Password1', 'Ruan', 'Supervisor');

-- ========================================
-- ADD 20 SAMPLE DRIVERS
-- ========================================

INSERT INTO drivers (FullName, DriverCode, Phone, FatigueLevel, Status, AssignedVehicle, CurrentLocation)
VALUES
('Thabo Mthembu', 'DRV001', '+27 82 111 1111', 35, 'Active', 'VH-001', 'JHB-PTA-001'),
('Sarah van der Merwe', 'DRV002', '+27 83 222 2222', 42, 'Active', 'VH-002', 'JHB-DBN-001'),
('Lerato Ndlovu', 'DRV003', '+27 84 333 3333', 50, 'Active', 'VH-003', 'CPT-DBN-001'),
('Pieter Botha', 'DRV004', '+27 85 444 4444', 60, 'Active', 'VH-004', 'BLO-CPT-001'),
('Zanele Khumalo', 'DRV005', '+27 86 555 5555', 27, 'Active', 'VH-005', 'JHB-RST-001'),
('Sipho Dlamini', 'DRV006', '+27 87 666 6666', 33, 'Active', 'VH-006', 'PTA-KZN-001'),
('Nomvula Khumalo', 'DRV007', '+27 88 777 7777', 40, 'Active', 'VH-007', 'EL-CPT-001'),
('Johan van Rensburg', 'DRV008', '+27 89 888 8888', 45, 'Active', 'VH-008', 'PTA-JHB-001'),
('Lungi Maseko', 'DRV009', '+27 71 999 9999', 48, 'Active', 'VH-009', 'JHB-KIM-001'),
('Teboho Mokoena', 'DRV010', '+27 72 000 0000', 36, 'Active', 'VH-010', 'JHB-ELS-001'),
('Ayanda Dube', 'DRV011', '+27 73 101 0101', 25, 'Active', 'VH-011', 'CPT-KZN-002'),
('Neo Sithole', 'DRV012', '+27 74 202 0202', 55, 'Active', 'VH-012', 'DBN-PTA-002'),
('Boitumelo Molefe', 'DRV013', '+27 75 303 0303', 64, 'Active', 'VH-013', 'EL-RST-002'),
('Kagiso Nkosi', 'DRV014', '+27 76 404 0404', 31, 'Active', 'VH-014', 'JHB-MID-001'),
('Tshepo Mahlangu', 'DRV015', '+27 77 505 0505', 53, 'Active', 'VH-015', 'PTA-RSA-002'),
('Katlego Zulu', 'DRV016', '+27 78 606 0606', 38, 'Active', 'VH-016', 'DBN-RSA-001'),
('Simphiwe Nkuna', 'DRV017', '+27 79 707 0707', 29, 'Active', 'VH-017', 'JHB-ELS-002'),
('Thandi Ngcobo', 'DRV018', '+27 80 808 0808', 47, 'Active', 'VH-018', 'PTA-MID-002'),
('Mpho Molewa', 'DRV019', '+27 81 909 0909', 44, 'Active', 'VH-019', 'KZN-CPT-002'),
('Dineo Ramaphosa', 'DRV020', '+27 82 111 0000', 58, 'Active', 'VH-020', 'JHB-KZN-003');

-- ========================================
-- SIMPLE VEHICLE LIST FOR DRIVERS
-- ========================================

INSERT INTO vehicles (RegistrationNumber, MakeModel, Year, Mileage, Status)
VALUES
('VH-001', 'Volvo FH16', 2023, 102000, 'In-Transit'),
('VH-002', 'Mercedes-Benz Actros', 2022, 98500, 'In-Transit'),
('VH-003', 'MAN TGX', 2023, 120500, 'Available'),
('VH-004', 'Scania R500', 2021, 203145, 'Available'),
('VH-005', 'Iveco Stralis', 2023, 52341, 'Available'),
('VH-006', 'Volvo FH16', 2021, 312456, 'Maintenance'),
('VH-007', 'Scania R450', 2022, 178234, 'Available'),
('VH-008', 'Mercedes-Benz Arocs', 2021, 234567, 'Available'),
('VH-009', 'Volvo FH13', 2022, 110000, 'Available'),
('VH-010', 'MAN TGS', 2023, 70000, 'Available'),
('VH-011', 'Volvo FMX', 2022, 95000, 'Available'),
('VH-012', 'Mercedes Actros', 2023, 103000, 'Available'),
('VH-013', 'Scania P410', 2020, 204000, 'Maintenance'),
('VH-014', 'Iveco Stralis', 2022, 153000, 'Available'),
('VH-015', 'MAN TGM', 2021, 187000, 'Available'),
('VH-016', 'Volvo FH500', 2023, 91000, 'Available'),
('VH-017', 'Mercedes-Benz Arocs', 2021, 220000, 'Available'),
('VH-018', 'MAN TGX', 2022, 184000, 'Available'),
('VH-019', 'Volvo FH16', 2023, 115000, 'Available'),
('VH-020', 'Scania R410', 2023, 100000, 'Available');

-- ========================================
-- END OF SCRIPT
-- ========================================

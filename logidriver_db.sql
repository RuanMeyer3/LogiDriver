-- ========================================
-- Add Delivery Locations Table
-- ========================================
CREATE TABLE IF NOT EXISTS deliverylocations (
    LocationId INT AUTO_INCREMENT PRIMARY KEY,
    LocationName VARCHAR(150) NOT NULL,
    Address VARCHAR(255) NOT NULL,
    City VARCHAR(100) NOT NULL,
    Province VARCHAR(100) NOT NULL,
    PostalCode VARCHAR(10) NOT NULL,
    Latitude DECIMAL(10,6) NULL,
    Longitude DECIMAL(10,6) NULL,
    CreatedAt DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)
) ENGINE=InnoDB;
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
(UUID(), 'rickus@logidriver.com', 'RICKUS@LOGIDRIVER.COM', 'rickus@logidriver.com', 'RICKUS@LOGIDRIVER.COM', 'Password1!', 'Rickus', 'Supervisor'),
(UUID(), 'rishab@logidriver.com', 'RISHAB@LOGIDRIVER.COM', 'rishab@logidriver.com', 'RISHAB@LOGIDRIVER.COM', 'Password1!', 'Rishab', 'Supervisor'),
(UUID(), 'robert@logidriver.com', 'ROBERT@LOGIDRIVER.COM', 'robert@logidriver.com', 'ROBERT@LOGIDRIVER.COM', 'Password1!', 'Robert', 'Supervisor'),
(UUID(), 'ruan@logidriver.com', 'RUAN@LOGIDRIVER.COM', 'ruan@logidriver.com', 'RUAN@LOGIDRIVER.COM', 'Password1!', 'Ruan', 'Supervisor');

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

--  50 delivery locations
INSERT INTO deliverylocations (LocationName, Address, City, Province, PostalCode, Latitude, Longitude) VALUES
('Richfield Distribution Hub', '12 Main Street', 'Johannesburg', 'Gauteng', '2000', -26.2041, 28.0473),
('Durban Port Terminal', '45 Dock Road', 'Durban', 'KwaZulu-Natal', '4001', -29.8579, 31.0292),
('Cape Town Freight Depot', '8 Foreshore Way', 'Cape Town', 'Western Cape', '8000', -33.9249, 18.4241),
('Pretoria North Depot', '67 Church Street', 'Pretoria', 'Gauteng', '0001', -25.7461, 28.1881),
('Bloemfontein Central Station', '23 Union Ave', 'Bloemfontein', 'Free State', '9301', -29.0852, 26.1596),
('Port Elizabeth Cargo Park', '4 Marine Drive', 'Gqeberha', 'Eastern Cape', '6001', -33.9608, 25.6022),
('East London Industrial Park', '19 Cambridge Road', 'East London', 'Eastern Cape', '5201', -32.9700, 27.8700),
('Polokwane Logistics Yard', '77 Landros Mare St', 'Polokwane', 'Limpopo', '0700', -23.9045, 29.4689),
('Nelspruit Distribution Center', '15 Madiba Drive', 'Mbombela', 'Mpumalanga', '1200', -25.4745, 30.9703),
('Kimberley Freight Zone', '18 Du Toitspan Rd', 'Kimberley', 'Northern Cape', '8301', -28.7282, 24.7499),
('George Industrial Area', '10 York Street', 'George', 'Western Cape', '6530', -33.9640, 22.4598),
('Rustenburg Depot', '9 Bethlehem St', 'Rustenburg', 'North West', '0299', -25.6676, 27.2421),
('Vereeniging Yard', '21 Voortrekker Rd', 'Vereeniging', 'Gauteng', '1930', -26.6731, 27.9261),
('Welkom Central Logistics', '13 Station Rd', 'Welkom', 'Free State', '9459', -27.9774, 26.7350),
('Mthatha Cargo Terminal', '6 Nelson Mandela Dr', 'Mthatha', 'Eastern Cape', '5100', -31.5889, 28.7844),
('Pietermaritzburg Depot', '25 Chief Albert Luthuli St', 'Pietermaritzburg', 'KZN', '3201', -29.6006, 30.3794),
('Tzaneen Warehouse', '34 Agatha St', 'Tzaneen', 'Limpopo', '0850', -23.8331, 30.1632),
('Upington Cargo Base', '3 Le Roux St', 'Upington', 'Northern Cape', '8800', -28.4478, 21.2561),
('Klerksdorp Hub', '18 Nelson Mandela Dr', 'Klerksdorp', 'North West', '2571', -26.8521, 26.6667),
('Mafikeng Depot', '20 Carrington St', 'Mahikeng', 'North West', '2745', -25.8652, 25.6441),
('Springs Distribution Point', '11 Second Ave', 'Springs', 'Gauteng', '1559', -26.2582, 28.4630),
('Benoni Transport Hub', '19 Ampthill Ave', 'Benoni', 'Gauteng', '1500', -26.1909, 28.3111),
('Randburg Dispatch', '8 Hill Street', 'Randburg', 'Gauteng', '2194', -26.0950, 28.0068),
('Centurion Logistics Center', '14 Jean Avenue', 'Centurion', 'Gauteng', '0157', -25.8744, 28.1700),
('Soweto Delivery Hub', '5 Vilakazi St', 'Soweto', 'Gauteng', '1804', -26.2560, 27.8540),
('Midrand Freight Park', '2 Old Pretoria Rd', 'Midrand', 'Gauteng', '1685', -25.9895, 28.1284),
('Sandton Drop-Off Point', '175 Rivonia Rd', 'Sandton', 'Gauteng', '2196', -26.1076, 28.0567),
('Krugersdorp Industrial Park', '7 Paardekraal Dr', 'Krugersdorp', 'Gauteng', '1739', -26.1040, 27.7700),
('Heidelberg Distribution Yard', '3 Voortrekker St', 'Heidelberg', 'Gauteng', '1441', -26.5059, 28.3592),
('Roodepoort South Depot', '9 Main Reef Rd', 'Roodepoort', 'Gauteng', '1724', -26.1663, 27.8725),
('Boksburg Industrial Area', '12 Commissioner St', 'Boksburg', 'Gauteng', '1459', -26.2135, 28.2596),
('Alberton Logistics Park', '6 Ring Road', 'Alberton', 'Gauteng', '1449', -26.2683, 28.1228),
('Springs East Yard', '4 Nigel Rd', 'Springs', 'Gauteng', '1560', -26.2645, 28.4520),
('Carletonville Freight Center', '9 Annan Rd', 'Carletonville', 'Gauteng', '2499', -26.3586, 27.3989),
('Vanderbijlpark Delivery Point', '18 Frikkie Meyer Blvd', 'Vanderbijlpark', 'Gauteng', '1900', -26.7088, 27.8310),
('Randfontein Hub', '10 Main St', 'Randfontein', 'Gauteng', '1759', -26.1771, 27.7020),
('Kempton Park Cargo Area', '22 Monument Rd', 'Kempton Park', 'Gauteng', '1619', -26.0979, 28.2305),
('Germiston Central Yard', '11 Meyer St', 'Germiston', 'Gauteng', '1401', -26.2145, 28.1706),
('Spruitview Drop Zone', '2 Khumalo St', 'Spruitview', 'Gauteng', '1425', -26.3134, 28.1815),
('Tembisa North Depot', '19 Andrew Mapheto Dr', 'Tembisa', 'Gauteng', '1632', -25.9820, 28.2269),
('Katlehong Industrial Park', '13 Kgotso St', 'Katlehong', 'Gauteng', '1431', -26.3662, 28.1642),
('Ekurhuleni Yard', '1 Civic Rd', 'Ekurhuleni', 'Gauteng', '1506', -26.1350, 28.2100),
('Modimolle Dispatch', '8 Nelson Dr', 'Modimolle', 'Limpopo', '0510', -24.7000, 28.4100),
('Giyani Logistics', '4 Freedom St', 'Giyani', 'Limpopo', '0826', -23.3025, 30.7183),
('Mokopane Yard', '7 Thabo Mbeki St', 'Mokopane', 'Limpopo', '0600', -24.1945, 29.0091),
('Lephalale Hub', '10 Ellis St', 'Lephalale', 'Limpopo', '0555', -23.6733, 27.7348),
('Phalaborwa Mine Depot', '3 Mine Rd', 'Phalaborwa', 'Limpopo', '1390', -23.9430, 31.1411),
('Musina Border Point', '2 Beit Bridge Rd', 'Musina', 'Limpopo', '0900', -22.3511, 30.0387),
('Louis Trichardt Drop Point', '9 Krogh St', 'Louis Trichardt', 'Limpopo', '0920', -23.0430, 29.9044),
('Thohoyandou Warehouse', '17 University Rd', 'Thohoyandou', 'Limpopo', '0950', -22.9480, 30.4846);

USE logidriver_db;

-- ===================================================
--  ADD 20 DRIVER LOGIN USERS (Role: Driver)
--  Default password: "Password123" (pre-hashed)
-- ===================================================

-- Hash below is a valid ASP.NET Identity hash for "Password123"
SET @PasswordHash = 'Passwprd1!';

INSERT INTO aspnetusers (
    Id, UserName, NormalizedUserName, Email, NormalizedEmail,
    EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp,
    PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount,
    FullName, Role, Status, CreatedAt
)
VALUES
(UUID(), 'driver1@logidriver.com', 'DRIVER1@LOGIDRIVER.COM', 'driver1@logidriver.com', 'DRIVER1@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'John Mokoena', 'Driver', 'Active', NOW()),
(UUID(), 'driver2@logidriver.com', 'DRIVER2@LOGIDRIVER.COM', 'driver2@logidriver.com', 'DRIVER2@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Sarah Molefe', 'Driver', 'Active', NOW()),
(UUID(), 'driver3@logidriver.com', 'DRIVER3@LOGIDRIVER.COM', 'driver3@logidriver.com', 'DRIVER3@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Michael Dlamini', 'Driver', 'Active', NOW()),
(UUID(), 'driver4@logidriver.com', 'DRIVER4@LOGIDRIVER.COM', 'driver4@logidriver.com', 'DRIVER4@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Zanele Khumalo', 'Driver', 'Active', NOW()),
(UUID(), 'driver5@logidriver.com', 'DRIVER5@LOGIDRIVER.COM', 'driver5@logidriver.com', 'DRIVER5@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Thabo Sithole', 'Driver', 'Active', NOW()),
(UUID(), 'driver6@logidriver.com', 'DRIVER6@LOGIDRIVER.COM', 'driver6@logidriver.com', 'DRIVER6@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Nomvula Nkosi', 'Driver', 'Active', NOW()),
(UUID(), 'driver7@logidriver.com', 'DRIVER7@LOGIDRIVER.COM', 'driver7@logidriver.com', 'DRIVER7@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Lerato Ndlovu', 'Driver', 'Active', NOW()),
(UUID(), 'driver8@logidriver.com', 'DRIVER8@LOGIDRIVER.COM', 'driver8@logidriver.com', 'DRIVER8@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Johan van Rensburg', 'Driver', 'Active', NOW()),
(UUID(), 'driver9@logidriver.com', 'DRIVER9@LOGIDRIVER.COM', 'driver9@logidriver.com', 'DRIVER9@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Bongani Mthembu', 'Driver', 'Active', NOW()),
(UUID(), 'driver10@logidriver.com', 'DRIVER10@LOGIDRIVER.COM', 'driver10@logidriver.com', 'DRIVER10@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Sibusiso Dube', 'Driver', 'Active', NOW()),
(UUID(), 'driver11@logidriver.com', 'DRIVER11@LOGIDRIVER.COM', 'driver11@logidriver.com', 'DRIVER11@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Daniel Radebe', 'Driver', 'Active', NOW()),
(UUID(), 'driver12@logidriver.com', 'DRIVER12@LOGIDRIVER.COM', 'driver12@logidriver.com', 'DRIVER12@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Peter Khosa', 'Driver', 'Active', NOW()),
(UUID(), 'driver13@logidriver.com', 'DRIVER13@LOGIDRIVER.COM', 'driver13@logidriver.com', 'DRIVER13@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Amogelang Mokoena', 'Driver', 'Active', NOW()),
(UUID(), 'driver14@logidriver.com', 'DRIVER14@LOGIDRIVER.COM', 'driver14@logidriver.com', 'DRIVER14@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Lucky Phiri', 'Driver', 'Active', NOW()),
(UUID(), 'driver15@logidriver.com', 'DRIVER15@LOGIDRIVER.COM', 'driver15@logidriver.com', 'DRIVER15@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Kabelo Molekwa', 'Driver', 'Active', NOW()),
(UUID(), 'driver16@logidriver.com', 'DRIVER16@LOGIDRIVER.COM', 'driver16@logidriver.com', 'DRIVER16@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Tumi Maduna', 'Driver', 'Active', NOW()),
(UUID(), 'driver17@logidriver.com', 'DRIVER17@LOGIDRIVER.COM', 'driver17@logidriver.com', 'DRIVER17@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Phindile Mahlangu', 'Driver', 'Active', NOW()),
(UUID(), 'driver18@logidriver.com', 'DRIVER18@LOGIDRIVER.COM', 'driver18@logidriver.com', 'DRIVER18@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Nathi Zondo', 'Driver', 'Active', NOW()),
(UUID(), 'driver19@logidriver.com', 'DRIVER19@LOGIDRIVER.COM', 'driver19@logidriver.com', 'DRIVER19@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Prince Baloyi', 'Driver', 'Active', NOW()),
(UUID(), 'driver20@logidriver.com', 'DRIVER20@LOGIDRIVER.COM', 'driver20@logidriver.com', 'DRIVER20@LOGIDRIVER.COM', 1, @PasswordHash, UPPER(UUID()), UPPER(UUID()), 0, 0, 1, 0, 'Reneilwe Kganyago', 'Driver', 'Active', NOW());



-- ========================================
-- END OF SCRIPT
-- ========================================

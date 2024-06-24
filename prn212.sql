CREATE DATABASE BadmintonCourtDB;
GO

USE BadmintonCourtDB;
GO

-- Create User table
CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Role VARCHAR(20), -- 'Admin' or 'Customer'
    Name VARCHAR(50),
    Gmail VARCHAR(50),
    Username VARCHAR(50),
    Password VARCHAR(200),
    PhoneNumber VARCHAR(50)
);

-- Create Location table
CREATE TABLE Location (
    LocationID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50),
    City VARCHAR(20),
    Province VARCHAR(20),
    Street VARCHAR(50)
);

-- Create TimeSlot table
CREATE TABLE TimeSlot (
    TimeSlotID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(20),
    Value VARCHAR(50)
);

-- Create BadmintonCourt table
CREATE TABLE BadmintonCourt (
    CourtID INT IDENTITY(1,1) PRIMARY KEY,
    LocationID INT,
    CourtName VARCHAR(50),
    Capacity INT,
    Description VARCHAR(100),
    Price INT,
    FOREIGN KEY (LocationID) REFERENCES Location(LocationID)
);

-- Create VenueServiceTime table
CREATE TABLE VenueServiceTime (
    VSTID INT IDENTITY(1,1) PRIMARY KEY,
    TimeSlotID INT,
    CourtID INT,
    FOREIGN KEY (TimeSlotID) REFERENCES TimeSlot(TimeSlotID),
    FOREIGN KEY (CourtID) REFERENCES BadmintonCourt(CourtID)
);

-- Create Booking table
CREATE TABLE Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    CourtID INT,
    NumberOfGuest INT,
    SpecialNote VARCHAR(200),
    TotalPrice INT,
    FOREIGN KEY (UserID) REFERENCES [User](UserID),
    FOREIGN KEY (CourtID) REFERENCES BadmintonCourt(CourtID)
);

-- Create BookingSlot table
CREATE TABLE BookingSlot (
    BookingID INT,
    VSTID INT,
    BookDate DATE,
    PRIMARY KEY (BookingID, VSTID),
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID),
    FOREIGN KEY (VSTID) REFERENCES VenueServiceTime(VSTID)
);

-- Create ItemType table
CREATE TABLE ItemType (
    ItemTypeID INT IDENTITY(1,1) PRIMARY KEY,
    Type VARCHAR(50)
);

-- Create Item table
CREATE TABLE Item (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    ItemTypeID INT,
    Name VARCHAR(20),
    Price INT,
    FOREIGN KEY (ItemTypeID) REFERENCES ItemType(ItemTypeID)
);

-- Create BookingItem table
CREATE TABLE BookingItem (
    BookingID INT,
    ItemID INT,
    UnitQuantity INT,
    SumPrice INT,
    PRIMARY KEY (BookingID, ItemID),
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);
go
INSERT INTO TimeSlot (Name, Value)
VALUES
('Morning', '07:00 - 07:30'),
('Morning', '07:30 - 08:00'),
('Morning', '08:00 - 08:30'),
('Morning', '08:30 - 09:00'),
('Morning', '09:00 - 09:30'),
('Morning', '09:30 - 10:00'),
('Morning', '10:00 - 10:30'),
('Morning', '10:30 - 11:00'),
('Morning', '11:00 - 11:30'),
('Morning', '11:30 - 12:00');

-- Insert afternoon time slots
INSERT INTO TimeSlot (Name, Value)
VALUES
('Afternoon', '12:00 - 12:30'),
('Afternoon', '12:30 - 13:00'),
('Afternoon', '13:00 - 13:30'),
('Afternoon', '13:30 - 14:00'),
('Afternoon', '14:00 - 14:30'),
('Afternoon', '14:30 - 15:00'),
('Afternoon', '15:00 - 15:30'),
('Afternoon', '15:30 - 16:00'),
('Afternoon', '16:00 - 16:30'),
('Afternoon', '16:30 - 17:00');

-- Insert evening time slots
INSERT INTO TimeSlot (Name, Value)
VALUES
('Evening', '17:00 - 17:30'),
('Evening', '17:30 - 18:00'),
('Evening', '18:00 - 18:30'),
('Evening', '18:30 - 19:00'),
('Evening', '19:00 - 19:30'),
('Evening', '19:30 - 20:00'),
('Evening', '20:00 - 20:30'),
('Evening', '20:30 - 21:00'),
('Evening', '21:00 - 21:30'),
('Evening', '21:30 - 22:00');

GO
-- Insert sample data into User table
INSERT INTO [User] (Role, Name, Gmail, Username, Password, PhoneNumber)
VALUES
('Admin', 'John Doe', 'johndoe@gmail.com', 'johndoe', 'password123', '123-456-7890'),
('Customer', 'Jane Smith', 'janesmith@gmail.com', 'janesmith', 'password123', '098-765-4321');

-- Insert sample data into Location table
INSERT INTO Location (Name, City, Province, Street)
VALUES
('Downtown Sports Center', 'Toronto', 'Ontario', '123 Queen St'),
('Uptown Sports Complex', 'Toronto', 'Ontario', '456 King St');

-- Insert sample data into TimeSlot table
INSERT INTO TimeSlot (Name, Value)
VALUES
('Morning', '09:00 - 12:00'),
('Afternoon', '13:00 - 16:00'),
('Evening', '17:00 - 20:00');

-- Insert sample data into BadmintonCourt table
INSERT INTO BadmintonCourt (LocationID, CourtName, Capacity, Description, Price)
VALUES
(1, 'Court A', 4, 'Standard court with wooden flooring', 50),
(1, 'Court B', 4, 'Standard court with synthetic flooring', 60),
(2, 'Court C', 6, 'Premium court with advanced lighting', 80);

-- Morning TimeSlots for Court 1, 2, and 3
INSERT INTO VenueServiceTime (TimeSlotID, CourtID)
VALUES
(4, 1), (5, 1), (6, 1), (7, 1), (8, 1), (9, 1), (10, 1), (11, 1), (12, 1), (13, 1),
(4, 2), (5, 2), (6, 2), (7, 2), (8, 2), (9, 2), (10, 2), (11, 2), (12, 2), (13, 2),
(4, 3), (5, 3), (6, 3), (7, 3), (8, 3), (9, 3), (10, 3), (11, 3), (12, 3), (13, 3);

-- Afternoon TimeSlots for Court 1, 2, and 3
INSERT INTO VenueServiceTime (TimeSlotID, CourtID)
VALUES
(14, 1), (15, 1), (16, 1), (17, 1), (18, 1), (19, 1), (20, 1), (21, 1), (22, 1), (23, 1),
(14, 2), (15, 2), (16, 2), (17, 2), (18, 2), (19, 2), (20, 2), (21, 2), (22, 2), (23, 2),
(14, 3), (15, 3), (16, 3), (17, 3), (18, 3), (19, 3), (20, 3), (21, 3), (22, 3), (23, 3);

-- Evening TimeSlots for Court 1, 2, and 3
INSERT INTO VenueServiceTime (TimeSlotID, CourtID)
VALUES
(24, 1), (25, 1), (26, 1), (27, 1), (28, 1), (29, 1), (30, 1), (31, 1), (32, 1), (33, 1),
(24, 2), (25, 2), (26, 2), (27, 2), (28, 2), (29, 2), (30, 2), (31, 2), (32, 2), (33, 2),
(24, 3), (25, 3), (26, 3), (27, 3), (28, 3), (29, 3), (30, 3), (31, 3), (32, 3), (33, 3);

GO
-- Insert sample data into VenueServiceTime table

-- Insert sample data into Booking table
INSERT INTO Booking (UserID, CourtID, NumberOfGuest, SpecialNote, TotalPrice)
VALUES
(2, 1, 4, 'No special requirements', 200);

-- Insert sample data into BookingSlot table
INSERT INTO BookingSlot (BookingID, VSTID, BookDate)
VALUES
(1, 1, '2024-07-01'),
(1, 2, '2024-07-01');

-- Insert sample data into ItemType table
INSERT INTO ItemType (Type)
VALUES
('Racket'),
('Shuttlecock'),
('Water Bottle');

-- Insert sample data into Item table
INSERT INTO Item (ItemTypeID, Name, Price)
VALUES
(1, 'Yonex Racket', 100),
(2, 'Feather Shuttlecock', 15),
(3, 'Sports Water Bottle', 10);

-- Insert sample data into BookingItem table
INSERT INTO BookingItem (BookingID, ItemID, UnitQuantity, SumPrice)
VALUES
(1, 1, 2, 200),
(1, 2, 5, 75);

GO

-- Assuming the TimeSlotIDs are sequential starting from 4 for new entries (as the previous ones ended at 3)


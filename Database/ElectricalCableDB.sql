CREATE DATABASE ElectricalCableDB;
GO

USE ElectricalCableDB;
GO

CREATE TABLE Users
(
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL
);

INSERT INTO Users(UserName,Password)
VALUES('admin','admin123');

CREATE TABLE RoadLengths
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    SpaceName NVARCHAR(200),
    FromLocation NVARCHAR(200),
    ToLocation NVARCHAR(200),
    StartPoint DECIMAL(18,2),
    EndPoint DECIMAL(18,2),
    TotalLength DECIMAL(18,2),
    DrumNo NVARCHAR(100),
    DrumSerial NVARCHAR(100),
    RoadName NVARCHAR(200),
    MV NVARCHAR(100),
    WorkDate DATE,
    Remarks NVARCHAR(MAX)
);
GO

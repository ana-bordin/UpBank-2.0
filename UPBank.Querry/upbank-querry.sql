CREATE DATABASE UpBankApiAddress;
GO

USE UpBankApiAddress;
GO

CREATE TABLE Address (
    ZipCode NVARCHAR(9) NOT NULL,
    Street NVARCHAR(255) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    State NVARCHAR(2) NOT NULL,
    Neighborhood NVARCHAR(100) NOT NULL,

	CONSTRAINT Address_PrimaryKey PRIMARY KEY (ZipCode)
);
GO

CREATE TABLE CompleteAddress (
	Id UNIQUEIDENTIFIER NOT NULL,
    ZipCode NVARCHAR(9) NOT NULL,
	Complement NVARCHAR(255),
	Number NVARCHAR(10) NOT NULL,

	CONSTRAINT Complete_Address_PrimaryKey PRIMARY KEY (Id),
	CONSTRAINT Complete_Address_ForeignKey FOREIGN KEY (ZipCode) REFERENCES Address(ZipCode)
);
GO
Select * From Address
Select * From CompleteAddress

CREATE DATABASE UpBankApiPerson;
GO

USE UpBankApiPerson;
GO

CREATE TABLE Person (
    CPF CHAR(14),
    Name VARCHAR(255) NOT NULL,
    BirthDate DATE NOT NULL,
    Gender CHAR(1) NOT NULL,
    Salary FLOAT NOT NULL,
    Email VARCHAR(255),
    Phone VARCHAR(20),
    AddressId UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT Person_PrimaryKey PRIMARY KEY (CPF)
);
GO

SELECT * FROM Person

CREATE DATABASE UpBankApiCustomer;
GO

USE UpBankApiCustomer;
GO

CREATE TABLE Customer (
    CPF CHAR(11) PRIMARY KEY,
    Restriction BIT NOT NULL DEFAULT 0,
	Active BIT NOT NULL DEFAULT 1
);
GO

--Delete FROM Customer
--Where CPF IN('11005669987') 

SELECT * FROM Customer

CREATE DATABASE UpBankApiEmployee;
GO

USE UpBankApiEmployee;
GO

CREATE TABLE Employee (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    CPF CHAR(11) NOT NULL,
    Manager BIT NOT NULL,
	Active BIT NOT NULL DEFAULT 0
);
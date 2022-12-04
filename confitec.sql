create database confictec;

use confitec;

CREATE TABLE Users
(
	UserId int primary key identity(1,1),
	UserName nvarchar(255),
	SurName nvarchar(255),
	Email nvarchar(255),
	BirthDate DateTime,
	Scholarity int
)

CREATE TABLE Scholarity
(
	ScholarityId int primary key identity(1,1),
	ScholarityName nvarchar(255)
)

INSERT INTO Scholarity VALUES 
('Infantil'), ('Fundamental'),
('Médio'), ('Superior')
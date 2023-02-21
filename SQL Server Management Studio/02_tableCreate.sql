USE SuperheroesDb;

CREATE TABLE Superhero (
	Id int IDENTITY(1,1) PRIMARY KEY, 
	[Name] nvarchar(40),
	Alias nvarchar(15),
	Origin nvarchar(40)
);

CREATE TABLE Assistant (
	Id int IDENTITY(1,1) PRIMARY KEY, 
	[Name] nvarchar(40)
);

CREATE TABLE [Power] (
	Id int IDENTITY(1,1) PRIMARY KEY, 
	[Name] nvarchar(40),
	[Description] nvarchar(40)
);
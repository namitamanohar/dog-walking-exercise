CREATE TABLE Neighborhood (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(55) NOT NULL, 
);
CREATE TABLE [Owner] (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(55) NOT NULL, 
    [Address] VARCHAR(55) NOT NULL, 
    NeighborhoodId Integer NOT NULL, 
    Phone VARCHAR(55) NOT NULL, 
    CONSTRAINT FK_Owner_Neighborhood Foreign Key(NeighborhoodId) References Neighborhood(Id)                    

);
CREATE TABLE Walker (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(55) NOT NULL, 
    NeighborhoodId Integer NOT NULL, 
    CONSTRAINT FK_Walker_Neighborhood Foreign Key(NeighborhoodId) References Neighborhood(Id)
);
CREATE TABLE Dog (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(55) NOT NULL, 
    OwnerId INTEGER NOT NULL, 
    Breed VARCHAR(55) NOT NULL, 
    Notes VARCHAR(255) NOT NULL, 
    CONSTRAINT FK_Dog_Owner Foreign Key(OwnerId) References [Owner](Id)                    
);
CREATE TABLE Walks (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [Date] datetime NOT NULL, 
    Duration Integer NOT NULL, 
    WalkerId Integer NOT NULL, 
    DogId Integer NOT NULL, 
    CONSTRAINT FK_Walks_Dog Foreign Key(DogId) References Dog(Id), 
    CONSTRAINT FK_Walks_Walker Foreign Key(WalkerId) References Walker(Id), 
);









INSERT INTO Neighborhood ([Name]) VALUES ('North Nashville');
INSERT INTO Neighborhood ([Name]) VALUES ('Germantown');


INSERT INTO Dog([Name], OwnerId, Breed, Notes) VALUES ('Izzy', 1, 'Rat Terrier', 'Loves Walks');
INSERT INTO Dog([Name], OwnerId, Breed, Notes) VALUES ('Sterling', 2, 'Golden Retriever', 'Enjoys retrieving balls');
INSERT INTO Dog([Name], OwnerId, Breed, Notes) VALUES ('Jabari', 3, 'Rotweiler', 'Has lots of energy and loves sticsk');
INSERT INTO Dog([Name], OwnerId, Breed, Notes) VALUES ('Abby', 4, 'Boston Terrier', 'Enjoys tennis balls');
INSERT INTO Dog([Name], OwnerId, Breed, Notes) VALUES ('Sandy', 2, 'Terrier', 'Does not like the rain');
INSERT INTO Dog([Name], OwnerId, Breed, Notes) VALUES ('Mandy', 2, 'Terrier', 'Loves puddles');



INSERT INTO Owner([Name], Address,NeighborhoodId, Phone) VALUES('Hannah', 'Burns Street', 1, '645372'); 
INSERT INTO Owner([Name], Address,NeighborhoodId, Phone) VALUES('Steve', 'Kellow Street', 2, '235372'); 
INSERT INTO Owner([Name], Address,NeighborhoodId, Phone) VALUES('Cindy', 'Ann Street', 1, '825372'); 
INSERT INTO Owner([Name], Address,NeighborhoodId, Phone) VALUES('Reenie', 'Jefferson Street', 2, '8374372'); 

INSERT INTO Walker([Name], NeighborhoodId) VALUES('Jansen',1); 
INSERT INTO Walker([Name], NeighborhoodId) VALUES('Kevin',2); 


INSERT INTO Walks([Date],Duration,WalkerId,DogId) VALUES('02/12/20', 40, 1, 2)
INSERT INTO Walks([Date],Duration,WalkerId,DogId) VALUES('04/15/20', 30, 2, 4);
INSERT INTO Walks([Date],Duration,WalkerId,DogId) VALUES('03/17/20', 45, 1, 1);

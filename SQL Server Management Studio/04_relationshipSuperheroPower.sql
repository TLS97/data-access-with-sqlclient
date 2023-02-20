USE SuperheroesDb;

CREATE TABLE SuperheroPower(
SuperheroId int FOREIGN KEY REFERENCES Superhero(Id), 
PowerId int FOREIGN KEY REFERENCES [Power](Id),
PRIMARY KEY(SuperheroId, PowerId)
);
USE SuperheroesDb;

CREATE TABLE SuperheroPower(
SuperheroId int, 
PowerId int, 
PRIMARY KEY(SuperheroId, PowerId)
);

ALTER TABLE SuperheroPower
ADD FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id);

ALTER TABLE SuperheroPower
ADD FOREIGN KEY (PowerId) REFERENCES [Power](Id);

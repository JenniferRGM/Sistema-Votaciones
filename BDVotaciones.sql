USE BDVotaciones;

-- Crear la clave for�nea FK_Votante
ALTER TABLE Votos
ADD CONSTRAINT FK_Votante FOREIGN KEY (IDVotante) REFERENCES Votantes(IDVotante);

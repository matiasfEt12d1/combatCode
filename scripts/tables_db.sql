CREATE DATABASE IF NOT EXISTS combatCode;
USE combatCode;

CREATE TABLE Personajes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    TipoPersonaje VARCHAR(20) NOT NULL,
    VidaMaxima DOUBLE NOT NULL,
    VidaActual DOUBLE NOT NULL,
    FuerzaBase DOUBLE NOT NULL,
    Armadura DOUBLE NULL,
    ManaMaximo INT NULL,
    ManaActual INT NULL,
    CantidadFlechas INT NULL,
    ProbabilidadCritico DOUBLE NULL
);

CREATE TABLE Habilidades (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    CostoRecurso INT NOT NULL,
    PotenciaBase DOUBLE NOT NULL
);

CREATE TABLE PersonajeHabilidades (
    PersonajeId INT NOT NULL,
    HabilidadId INT NOT NULL,
    PRIMARY KEY (PersonajeId, HabilidadId),
    FOREIGN KEY (PersonajeId) REFERENCES Personajes(Id) ON DELETE CASCADE,
    FOREIGN KEY (HabilidadId) REFERENCES Habilidades(Id) ON DELETE CASCADE
);

CREATE TABLE Batallas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FechaInicio DATETIME DEFAULT CURRENT_TIMESTAMP,
    GanadorId INT NULL,
    FOREIGN KEY (GanadorId) REFERENCES Personajes(Id)
);

CREATE TABLE HistorialAcciones (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    BatallaId INT NOT NULL,
    NumeroRonda INT NOT NULL,
    AtacanteId INT NOT NULL,
    DefensorId INT NOT NULL,
    DanioEmitido DOUBLE NOT NULL,
    FOREIGN KEY (BatallaId) REFERENCES Batallas(Id) ON DELETE CASCADE,
    FOREIGN KEY (AtacanteId) REFERENCES Personajes(Id),
    FOREIGN KEY (DefensorId) REFERENCES Personajes(Id)
);
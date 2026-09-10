USE JuegoBatallasDB;

DELIMITER //

CREATE PROCEDURE sp_RegistrarPersonaje (
    p_Nombre VARCHAR(100),
    p_TipoPersonaje VARCHAR(20),
    p_VidaMaxima DOUBLE,
    p_FuerzaBase DOUBLE,
    p_Armadura DOUBLE,
    p_ManaMaximo INT,
    p_CantidadFlechas INT,
    p_ProbabilidadCritico DOUBLE
)
BEGIN
    INSERT INTO Personajes (
        Nombre, TipoPersonaje, VidaMaxima, VidaActual, FuerzaBase,
        Armadura, ManaMaximo, ManaActual, CantidadFlechas, ProbabilidadCritico
    ) 
    VALUES (
        p_Nombre, p_TipoPersonaje, p_VidaMaxima, p_VidaMaxima, p_FuerzaBase,
        p_Armadura, p_ManaMaximo, p_ManaMaximo, p_CantidadFlechas, p_ProbabilidadCritico
    );
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE sp_FinalizarBatalla (
    p_BatallaId INT,
    p_GanadorId INT,
    p_PerdedorId INT,
    p_VidaRestanteGanador DOUBLE
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
    END;

    START TRANSACTION;

    UPDATE Batallas 
    SET GanadorId = p_GanadorId 
    WHERE Id = p_BatallaId;

    UPDATE Personajes 
    SET VidaActual = p_VidaRestanteGanador 
    WHERE Id = p_GanadorId;

    UPDATE Personajes 
    SET VidaActual = 0 
    WHERE Id = p_PerdedorId;

    COMMIT;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE sp_RegistrarAccion (
    p_BatallaId INT,
    p_NumeroRonda INT,
    p_AtacanteId INT,
    p_DefensorId INT,
    p_DanioEmitido DOUBLE
)
BEGIN
    INSERT INTO HistorialAcciones (
        BatallaId, NumeroRonda, AtacanteId, DefensorId, DanioEmitido
    )
    VALUES (
        p_BatallaId, p_NumeroRonda, p_AtacanteId, p_DefensorId, p_DanioEmitido
    );
END //

DELIMITER ;
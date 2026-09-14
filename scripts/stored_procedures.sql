USE db_combatCode;

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


-- TRANSACCIONES


DELIMITER //

DROP PROCEDURE IF EXISTS sp_RegistrarBatallaTransaccional //

CREATE PROCEDURE sp_RegistrarBatallaTransaccional (
    p_Participante1Id INT,
    p_Participante2Id INT,
    p_AtacanteInicialId INT
)
BEGIN

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    INSERT INTO Batallas (FechaInicio, GanadorId) 
    VALUES (NOW(), NULL);
    
    SET @v_BatallaId = LAST_INSERT_ID();

    INSERT INTO BatallaParticipantes (BatallaId, PersonajeId, EsAtacanteInicial)
    VALUES (@v_BatallaId, p_Participante1Id, (p_Participante1Id = p_AtacanteInicialId));

    INSERT INTO BatallaParticipantes (BatallaId, PersonajeId, EsAtacanteInicial)
    VALUES (@v_BatallaId, p_Participante2Id, (p_Participante2Id = p_AtacanteInicialId));

    COMMIT;
END //

DELIMITER ;
USE db_combatCode;

DELIMITER //

DROP PROCEDURE IF EXISTS sp_IniciarBatalla //
CREATE PROCEDURE sp_IniciarBatalla (
    p_AtacanteId INT,
    p_DefensorId INT,
    OUT p_BatallaId INT
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

    SET p_BatallaId = LAST_INSERT_ID();

    INSERT INTO HistorialAcciones (BatallaId, NumeroRonda, AtacanteId, DefensorId, DanioEmitido)
    VALUES (p_BatallaId, 0, p_AtacanteId, p_DefensorId, 0);

    COMMIT;
END //

DROP PROCEDURE IF EXISTS sp_ObtenerRankingPorTipoPersonaje //
CREATE PROCEDURE sp_ObtenerRankingPorTipoPersonaje ()
BEGIN
    SELECT 
        p.TipoPersonaje,
        COUNT(DISTINCT p.Id) AS CantidadPersonajes,
        COUNT(b.Id) AS TotalVictorias,
        ROUND(IFNULL(AVG(h.DanioEmitido), 0), 2) AS PromedioDanioPorGolpe
    FROM Personajes p
    LEFT JOIN Batallas b ON p.Id = b.GanadorId
    LEFT JOIN HistorialAcciones h ON p.Id = h.AtacanteId AND h.NumeroRonda > 0
    GROUP BY p.TipoPersonaje
    ORDER BY TotalVictorias DESC, PromedioDanioPorGolpe DESC;
END //

DROP PROCEDURE IF EXISTS sp_GenerarReporteBatalla //
CREATE PROCEDURE sp_GenerarReporteBatalla (
    p_BatallaId INT
)
BEGIN
    SELECT 
        b.Id AS BatallaId,
        b.FechaInicio,
        pGanador.Nombre AS Ganador,
        MAX(h.NumeroRonda) AS TotalRondas,
        SUM(h.DanioEmitido) AS DanioTotalCausado
    FROM Batallas b
    INNER JOIN Personajes pGanador ON b.GanadorId = pGanador.Id
    INNER JOIN HistorialAcciones h ON b.Id = h.BatallaId
    WHERE b.Id = p_BatallaId
    GROUP BY b.Id, b.FechaInicio, pGanador.Nombre;
END //

DELIMITER ;


-- TRANSACCIONES


DELIMITER //

DROP PROCEDURE IF EXISTS sp_CerrarBatallaTransaccional //

CREATE PROCEDURE sp_CerrarBatallaTransaccional (
    p_BatallaId INT,
    p_GanadorId INT,
    p_PerdedorId INT,
    p_VidaRestanteGanador DOUBLE,
    p_RondaFinal INT,
    p_DanioUltimoGolpe DOUBLE
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    UPDATE Batallas 
    SET GanadorId = p_GanadorId 
    WHERE Id = p_BatallaId;

    INSERT INTO HistorialAcciones (BatallaId, NumeroRonda, AtacanteId, DefensorId, DanioEmitido)
    VALUES (p_BatallaId, p_RondaFinal, p_GanadorId, p_PerdedorId, p_DanioUltimoGolpe);

    UPDATE Personajes 
    SET VidaActual = p_VidaRestanteGanador 
    WHERE Id = p_GanadorId;

    UPDATE Personajes 
    SET VidaActual = 0 
    WHERE Id = p_PerdedorId;

    COMMIT;
END //

DELIMITER ;
DELIMITER //

CREATE FUNCTION fn_ObtenerVictorias (
    p_PersonajeId INT
) 
RETURNS INT
READS SQL DATA
BEGIN
    DECLARE v_TotalVictorias INT;

    SELECT COUNT(*) 
    INTO v_TotalVictorias
    FROM Batallas 
    WHERE GanadorId = p_PersonajeId;

    RETURN v_TotalVictorias;
END //

DELIMITER ;


DELIMITER //

CREATE FUNCTION fn_CalcularPorcentajeVida (
    p_PersonajeId INT
) 
RETURNS DOUBLE
READS SQL DATA
BEGIN
    DECLARE v_Porcentaje DOUBLE;

    SELECT (VidaActual / VidaMaxima) * 100 
    INTO v_Porcentaje
    FROM Personajes 
    WHERE Id = p_PersonajeId;

    RETURN IFNULL(v_Porcentaje, 0);
END //

DELIMITER ;


DELIMITER //

CREATE FUNCTION fn_ObtenerTotalBatallas (
    p_PersonajeId INT
) 
RETURNS INT
READS SQL DATA
BEGIN
    DECLARE v_Total INT;

    SELECT COUNT(*) 
    INTO v_Total
    FROM BatallaParticipantes 
    WHERE PersonajeId = p_PersonajeId;

    RETURN v_Total;
END //

DELIMITER ;

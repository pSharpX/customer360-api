ALTER PROC sganet_sps_ultima_fecha_sistema
AS
BEGIN
	SELECT CONVERT(CHAR(10),GETDATE(),112) as fecha_actual
END
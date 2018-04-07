/*****************************************************************************
Objetivo		: Obtiene la ultima fecha del sistema
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (01/02/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(Jedion Mellbin) 01/02/2018 Creacion
****************************************************************************/

ALTER PROC sganet_sps_ultima_fecha_sistema
AS
BEGIN
	SELECT CONVERT(CHAR(10),GETDATE(),112) as fecha_actual
END



ALTER PROCEDURE [dbo].[sganet_sps_estadosincronizaciondetalle]

/*****************************************************************************
Objetivo		: Listado de Estado Proceso Detalle.
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (19/01/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 19/01/2018 Creacion
****************************************************************************/
AS
BEGIN TRY

	--Declaración de Variables
	DECLARE @vl_nu_error INT
	DECLARE @vl_fl_transaccion CHAR(1)
	DECLARE @vi_co_tablaestadosincronizaciondetalle INT = (234);
	
	--Inicialización de Variables
	SET @vl_fl_transaccion = '0'

	--Transacción
	BEGIN TRANSACTION

	SELECT 
		DT.no_valor2 AS Codigo,DT.no_valor1 AS Descripcion
	FROM maestro.tabla_detalle DT 
	INNER JOIN maestro.tabla_gen MG ON DT.nid_tabla_gen = MG.nid_tabla_gen AND DT.fl_inactivo = 0 AND MG.fl_inactivo = 0
	WHERE MG.nid_tabla_gen = @vi_co_tablaestadosincronizaciondetalle
		
	SET @vl_fl_transaccion = '1'

	COMMIT TRANSACTION

	SET @vl_fl_transaccion = '0'

END TRY
BEGIN CATCH
	
	SET @vl_nu_error = ERROR_NUMBER()
	--IF @vl_nu_error = 2627 SET @vo_id_usuario = -2
	--ELSE IF @vl_nu_error = 515 SET @vo_id_usuario = -3
	--ELSE IF @vl_nu_error = 547 SET @vo_id_usuario = -4
	--ELSE SET @vo_id_usuario = -1

	IF (@vl_fl_transaccion = '1') ROLLBACK TRANSACTION

END CATCH
GO
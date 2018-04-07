--exec [dbo].[sganet_sps_procesosincronizacion] '2018-01-01','2018-01-31',NULL

ALTER PROCEDURE [dbo].[sganet_sps_procesosincronizacion]
(
	@vi_tx_fechainicio VARCHAR(10)=null,	
	@vi_tx_fechafin VARCHAR(10)=null,
	@vi_tx_estadosindetalle VARCHAR(2)=null
)
/*****************************************************************************
Objetivo		: Busqueda de Visor de Sincronizacion
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (22/01/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 22/01/2018 Creacion
****************************************************************************/
AS
BEGIN TRY

	--Declaración de Variables
	DECLARE @vl_nu_error INT
	DECLARE @vl_fl_transaccion CHAR(1)
	DECLARE @vi_co_tablaestadosincronizaciondetalle INT = (234);
	DECLARE @vi_co_tablatiposincronizacion INT = (235);
	
	--Inicialización de Variables
	SET @vl_fl_transaccion = '0'

	IF @vi_tx_fechainicio IS NULL  
	BEGIN
		SET @vi_tx_fechainicio = '1900-01-01'
		SET @vi_tx_fechafin = '2999-12-31'
	END
	ELSE
	BEGIN
		SET @vi_tx_fechafin =  ISNULL(@vi_tx_fechafin,@vi_tx_fechainicio)
	END

	--Transacción
	BEGIN TRANSACTION

	SELECT 
		(CASE WHEN PED.fe_fin IS NULL THEN '' ELSE (CONVERT(nvarchar(10), PED.fe_fin, 103) + ' ' + CONVERT(nvarchar(10), PED.fe_fin, 108)) END) AS Fecha,
		K.Descripcion AS TipoSincronizacion,
		PE.nid_proceso_ejecucion AS IdProceso,	
		PED.nid_proceso_ejecucion_detalle AS IdDetalleProceso,
		TP.no_tipo_proceso AS TipoProceso,
		AR.no_repositorio AS Aplicacion,
		M.Descripcion AS Estado,
		PED.tx_observacion AS Observacion,
		PE.data AS [Data]
	FROM proceso_ejecucion PE
	INNER JOIN proceso_ejecucion_detalle PED ON PE.nid_proceso_ejecucion = PED.nid_proceso_ejecucion AND 
	PE.fl_inactivo = 0 AND PED.fl_inactivo = 0
	INNER JOIN aplicacion_repositorio AR ON PED.nid_aplicacion_repositorio = AR.nid_aplicacion_repositorio AND AR.fl_inactivo = 0
	INNER JOIN tipo_proceso TP ON PE.nid_tipo_proceso = TP.nid_tipo_proceso AND TP.fl_inactivo = 0
	LEFT JOIN
	(
		SELECT 
			DT.no_valor2 AS Codigo,DT.no_valor1 AS Descripcion
		FROM maestro.tabla_detalle DT 
		INNER JOIN maestro.tabla_gen MG ON DT.nid_tabla_gen = MG.nid_tabla_gen AND DT.fl_inactivo = 0 AND MG.fl_inactivo = 0
		WHERE MG.nid_tabla_gen = @vi_co_tablaestadosincronizaciondetalle
	) AS M ON M.Codigo = PED.co_estado
	LEFT JOIN
	(
		SELECT 
			DT.no_valor2 AS Codigo,DT.no_valor1 AS Descripcion
		FROM maestro.tabla_detalle DT 
		INNER JOIN maestro.tabla_gen MG ON DT.nid_tabla_gen = MG.nid_tabla_gen AND DT.fl_inactivo = 0 AND MG.fl_inactivo = 0
		WHERE MG.nid_tabla_gen = @vi_co_tablatiposincronizacion
	) AS K ON K.Codigo = PED.co_estado
	WHERE (
		((@vi_tx_estadosindetalle IS NULL) OR (M.Codigo = ISNULL(@vi_tx_estadosindetalle,M.Codigo))) AND 
		(CONVERT(VARCHAR(10),PED.fe_fin,120) BETWEEN @vi_tx_fechainicio AND @vi_tx_fechafin)
	)
	ORDER BY PED.fe_fin DESC

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

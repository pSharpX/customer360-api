

ALTER PROCEDURE sganet_sps_puntoventa
/*****************************************************************************
Objetivo		: Busqueda de Punto de Venta por los filtros ingresados.
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (16/01/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 16/01/2018 Creacion
****************************************************************************/
(	
	@vi_tx_sucursal VARCHAR(200)=null	
)
AS
BEGIN TRY

	--Declaración de Variables
	DECLARE @vl_nu_error INT
	DECLARE @vl_fl_transaccion CHAR(1)
	
	--Inicialización de Variables
	SET @vl_fl_transaccion = '0'

	--Transacción
	BEGIN TRANSACTION
	
	--Busqueda de Clientes
	
	SELECT nid_punto_venta AS IdPuntoVenta FROM mae_punto_venta		
	WHERE fl_inactivo = 0 and no_punto_venta LIKE '%' + LTRIM(RTRIM(@vi_tx_sucursal)) + '%'
		
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

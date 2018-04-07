--EXEC dw_sps_clienteadvanced NULL,NULL,'KMHD741CAHU364552',NULL

ALTER PROCEDURE dw_sps_clienteadvanced
/*****************************************************************************
Objetivo		: Busqueda Avanzada de Clientes por los filtros ingresados.
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (15/01/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 15/01/2018 Creacion
****************************************************************************/
(
	@vi_tx_aniofab VARCHAR(200)=NULL,
	@vi_tx_aniomodelo VARCHAR(200)=NULL,
	@vi_tx_sucursal VARCHAR(200)=NULL,
	@vi_tx_asesorcomercial VARCHAR(200)=NULL,
	@vi_tx_fechaentregaDe VARCHAR(200)=NULL,
	@vi_tx_fechaentregaHasta VARCHAR(200)=NULL,
	@vi_tx_asesorservicio VARCHAR(200)=NULL,
	@vi_tx_fechaservicioDe VARCHAR(200)=NULL,
	@vi_tx_fechaservicioHasta VARCHAR(200)=NULL,
	@vi_tx_asesorvendedor VARCHAR(200)=NULL,
	@vi_tx_fechaventaDe VARCHAR(200)=NULL,
	@vi_tx_fechaventaHasta VARCHAR(200)=NULL,
	@vi_tx_vin VARCHAR(200)=NULL
)
AS
BEGIN TRY

	--Declaración de Variables
	DECLARE @vl_nu_error INT
	DECLARE @vl_fl_transaccion CHAR(1)
	DECLARE @TblClientes AS Table(NumeroDocumento VARCHAR(20) NULL,TipoDocumento CHAR(4) NULL)

	--Inicialización de Variables
	SET @vl_fl_transaccion = '0'

	--Transacción
	BEGIN TRANSACTION

	--Busqueda de Clientes
	IF @vi_tx_aniofab IS NOT NULL
	BEGIN

		INSERT INTO @TblClientes
		SELECT 
			DISTINCT nu_documento_cliente,NULL 
		FROM tbl_dw_unidades_facturadas WHERE fl_inactivo = 0 AND an_fabricacion = @vi_tx_aniofab			

	END 

	IF @vi_tx_aniomodelo IS NOT NULL
	BEGIN

		IF (SELECT COUNT(*) FROM @TblClientes) = 0 
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT nu_documento_cliente,NULL 
			FROM tbl_dw_unidades_facturadas WHERE fl_inactivo = 0 AND an_modelo = @vi_tx_aniomodelo

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT TW.nu_documento_cliente,NULL
			FROM tbl_dw_unidades_facturadas TW
			INNER JOIN @TblClientes T ON TW.nu_documento_cliente = T.NumeroDocumento --AND TW.co_tipo_cliente = T.TipoPersona
			WHERE TW.fl_inactivo = 0 AND TW.an_modelo = @vi_tx_aniomodelo

		END
	END

	IF @vi_tx_sucursal IS NOT NULL
	BEGIN
	
		IF (SELECT COUNT(*) FROM @TblClientes) = 0 
		BEGIN

			INSERT INTO @TblClientes		
			SELECT 
				DISTINCT nu_documento_cliente,NULL 
			FROM tbl_dw_unidades_facturadas WHERE nid_punto_venta = @vi_tx_sucursal AND fl_inactivo = 0

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT UF.nu_documento_cliente,NULL
			FROM tbl_dw_unidades_facturadas UF 			
			INNER JOIN @TblClientes T ON UF.nu_documento_cliente = T.NumeroDocumento --AND UF.co_tipo_cliente = T.TipoPersona
			WHERE UF.fl_inactivo = 0 AND UF.nid_punto_venta = @vi_tx_sucursal

		END
	END

	IF @vi_tx_asesorcomercial IS NOT NULL
	BEGIN

		IF (SELECT COUNT(*) FROM @TblClientes) = 0 
		BEGIN

			INSERT INTO @TblClientes		
			SELECT 
				DISTINCT nu_documento_cliente,NULL 
			FROM tbl_dw_unidades_facturadas WHERE no_asesor_comercial = @vi_tx_asesorcomercial AND fl_inactivo = 0

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT UF.nu_documento_cliente,NULL
			FROM tbl_dw_unidades_facturadas UF 			
			INNER JOIN @TblClientes T ON UF.nu_documento_cliente = T.NumeroDocumento --AND UF.co_tipo_cliente = T.TipoPersona
			WHERE UF.fl_inactivo = 0 AND UF.no_asesor_comercial = @vi_tx_asesorcomercial

		END
	END 

	IF @vi_tx_vin IS NOT NULL
	BEGIN
		
		IF (SELECT COUNT(*) FROM @TblClientes) = 0
		BEGIN
			
			INSERT INTO @TblClientes		
			SELECT 
				DISTINCT nu_documento_cliente,NULL 
			FROM tbl_dw_unidades_facturadas WHERE nu_vin LIKE '%' + @vi_tx_vin + '%' AND fl_inactivo = 0

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT UF.nu_documento_cliente,NULL
			FROM tbl_dw_unidades_facturadas UF 			
			INNER JOIN @TblClientes T ON UF.nu_documento_cliente = T.NumeroDocumento --AND UF.co_tipo_cliente = T.TipoPersona
			WHERE UF.fl_inactivo = 0 AND UF.nu_vin LIKE '%' + @vi_tx_vin + '%' AND fl_inactivo = 0

		END
	END

	IF @vi_tx_fechaentregaDe IS NOT NULL
	BEGIN

		SET @vi_tx_fechaentregaHasta = ISNULL(@vi_tx_fechaentregaHasta,@vi_tx_fechaentregaDe)

		IF (SELECT COUNT(*) FROM @TblClientes) = 0 
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT nu_documento_cliente,NULL 
			FROM tbl_dw_unidades_facturadas 
			WHERE fl_inactivo = 0 AND fe_entrega BETWEEN @vi_tx_fechaentregaDe AND @vi_tx_fechaentregaHasta

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT TW.nu_documento_cliente,NULL
			FROM tbl_dw_unidades_facturadas TW
			INNER JOIN @TblClientes T ON TW.nu_documento_cliente = T.NumeroDocumento --AND TW.co_tipo_cliente = T.TipoPersona
			WHERE TW.fl_inactivo = 0 AND TW.fe_entrega BETWEEN @vi_tx_fechaentregaDe AND @vi_tx_fechaentregaHasta

		END
	END 

	IF @vi_tx_asesorservicio IS NOT NULL
	BEGIN
	
		IF (SELECT COUNT(*) FROM @TblClientes) = 0
		BEGIN
			
			INSERT INTO @TblClientes		
			SELECT 
				DISTINCT cli_nu_doc,NULL 
			FROM tbl_dw_ot_pv WHERE Asesor = @vi_tx_asesorservicio

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT UF.cli_nu_doc,NULL
			FROM tbl_dw_ot_pv UF 			
			INNER JOIN @TblClientes T ON UF.cli_nu_doc = T.NumeroDocumento --AND UF.co_tipo_cliente = T.TipoPersona
			WHERE Asesor = @vi_tx_asesorservicio

		END
	END 


	IF @vi_tx_fechaservicioDe IS NOT NULL
	BEGIN

		SET @vi_tx_fechaservicioHasta = ISNULL(@vi_tx_fechaservicioHasta,@vi_tx_fechaservicioDe)

		IF (SELECT COUNT(*) FROM @TblClientes) = 0 
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT cli_nu_doc,NULL 
			FROM tbl_dw_ot_pv 
			WHERE FechaApertura BETWEEN @vi_tx_fechaservicioDe AND @vi_tx_fechaservicioHasta

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT TW.cli_nu_doc,NULL
			FROM tbl_dw_ot_pv TW
			INNER JOIN @TblClientes T ON TW.cli_nu_doc = T.NumeroDocumento --AND TW.co_tipo_cliente = T.TipoPersona
			WHERE TW.FechaApertura BETWEEN @vi_tx_fechaservicioDe AND @vi_tx_fechaservicioHasta

		END
	END 

	IF @vi_tx_asesorvendedor IS NOT NULL
	BEGIN

		IF (SELECT COUNT(*) FROM @TblClientes) = 0
		BEGIN
			
			INSERT INTO @TblClientes		
			SELECT 
				DISTINCT ClienteRUC,NULL 
			FROM tbl_detalle_cierre_mensual_servicios_repuesto WHERE no_asesor = @vi_tx_asesorvendedor 

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT UF.ClienteRUC,NULL
			FROM tbl_detalle_cierre_mensual_servicios_repuesto UF 			
			INNER JOIN @TblClientes T ON UF.ClienteRUC = T.NumeroDocumento --AND UF.co_tipo_cliente = T.TipoPersona
			WHERE no_asesor = @vi_tx_asesorvendedor 

		END
	END

	IF @vi_tx_fechaventaDe IS NOT NULL
	BEGIN

		SET @vi_tx_fechaventaHasta = ISNULL(@vi_tx_fechaventaHasta,@vi_tx_fechaventaDe)

		IF (SELECT COUNT(*) FROM @TblClientes) = 0 
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT ClienteRUC,NULL 
			FROM tbl_detalle_cierre_mensual_servicios_repuesto 
			WHERE FechaDocumento BETWEEN @vi_tx_fechaventaDe AND @vi_tx_fechaventaHasta

		END
		ELSE
		BEGIN

			INSERT INTO @TblClientes
			SELECT 
				DISTINCT TW.ClienteRUC,NULL
			FROM tbl_detalle_cierre_mensual_servicios_repuesto TW
			INNER JOIN @TblClientes T ON TW.ClienteRUC = T.NumeroDocumento --AND TW.co_tipo_cliente = T.TipoPersona
			WHERE TW.FechaDocumento BETWEEN @vi_tx_fechaventaDe AND @vi_tx_fechaventaHasta

		END
	END 


	SELECT DISTINCT NumeroDocumento,NULL FROM @TblClientes

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
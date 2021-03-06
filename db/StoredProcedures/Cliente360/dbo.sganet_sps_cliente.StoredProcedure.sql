
ALTER PROCEDURE [dbo].[sganet_sps_cliente]
/*****************************************************************************
Objetivo		: Buscar Clientes por los filtros ingresados.
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (07/12/2017)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 07/12/2017 Creacion
****************************************************************************/

(
	@vi_qt_tipofiltro INT,
	@vi_tx_desfiltro VARCHAR(200)
)
AS
BEGIN TRY

	--Declaración de Variables
	DECLARE @vl_nu_error INT
	DECLARE @vl_fl_transaccion CHAR(1)
	DECLARE @vi_co_tablatipodocumento INT = (40);
	DECLARE @vi_co_tablatipoclientesexo INT = (94);
	DECLARE @vi_co_tablatipopersona INT = (54);
	DECLARE @vi_co_tablatipoultimocontacto INT = (232);
	DECLARE @vi_co_razonsocialruc VARCHAR(4) = '0002';

	--Inicialización de Variables
	SET @vl_fl_transaccion = '0'

	--Transacción
	BEGIN TRANSACTION

	--Busqueda de Clientes
	SELECT 
		CLI.nid_cliente AS IdCliente,
		LTRIM(RTRIM(PE.nu_documento)) AS NumeroDocumento,
		LTRIM(RTRIM(
			CASE 
				WHEN ISNULL(PE.co_tipo_persona,'') = @vi_co_razonsocialruc THEN PE.no_razon_social
				ELSE PE.no_persona
			END 	
		)) AS NombreCompleto,
		LTRIM(RTRIM(
			CASE 
				WHEN ISNULL(PE.co_tipo_persona,'') = @vi_co_razonsocialruc THEN ''
				ELSE PE.no_apellido_paterno
			END 
		)) AS ApellidoPaterno,
		LTRIM(RTRIM(
			CASE 
				WHEN ISNULL(PE.co_tipo_persona,'') = @vi_co_razonsocialruc THEN ''
				ELSE PE.no_apellido_materno
			END
		)) AS ApellidoMaterno,
		LTRIM(RTRIM(M.DescripcionMaestro)) AS TipoDocumento,
		CLI.fl_venta_vehiculo AS VentaVehiculo,
		CLI.fl_vservicios AS Servicio,
		CLI.fl_venta_repuestos AS VentaRepuesto,
		CLI.fe_ultimo_contacto AS FechaUltimoContacto,
		LTRIM(RTRIM(U.DescripcionMaestro)) AS CodigoUltimoContacto,
		LTRIM(RTRIM(Z.DescripcionMaestro)) AS Genero,
		PE.fe_nacimiento AS FechaNacimiento,
		LTRIM(RTRIM(PE.nu_telefono)) AS Telefono,
		LTRIM(RTRIM(PE.nu_celular)) AS Celular,
		LTRIM(RTRIM(PE.no_correo)) AS Correo,
		LTRIM(RTRIM(PDR.no_direccion)) AS Direccion,
		LTRIM(RTRIM(DEP.nombre)) AS Departamento,
		LTRIM(RTRIM(PROV.nombre)) AS Provincia,
		LTRIM(RTRIM(DIST.nombre)) AS Distrito,
		LTRIM(RTRIM(T.DescripcionMaestro)) AS TipoPersona
	FROM persona PE
	INNER JOIN cliente CLI ON PE.nid_persona = CLI.nid_cliente AND PE.fl_inactivo = 0 AND CLI.fl_inactivo = 0
	LEFT JOIN persona_direccion PDR ON CLI.nid_direccion_facturacion = PDR.nid_direccion AND PDR.fl_Inactivo = 0
	LEFT JOIN maestro.ubigeo DEP ON PDR.coddpto = DEP.coddpto AND DEP.codprov = 00 AND DEP.coddist = 00
	LEFT JOIN maestro.ubigeo PROV ON PDR.codprov = PROV.codprov AND PDR.coddpto = PROV.coddpto AND PROV.coddist = 00
	LEFT JOIN maestro.ubigeo DIST ON PDR.coddist = DIST.coddist AND PDR.codprov = DIST.codprov AND PDR.coddpto = DIST.coddpto
	LEFT JOIN 
	(
		SELECT 
			DT.no_valor2 AS CodigoMaestro,DT.no_valor1 AS DescripcionMaestro
		FROM maestro.tabla_detalle DT 
		INNER JOIN maestro.tabla_gen MG ON DT.nid_tabla_gen = MG.nid_tabla_gen AND DT.fl_inactivo = 0 AND MG.fl_inactivo = 0
		WHERE MG.nid_tabla_gen = @vi_co_tablatipodocumento
	)AS M ON PE.co_tipo_documento = M.CodigoMaestro
	LEFT JOIN 
	(
		SELECT 
			DT.no_valor2 AS CodigoMaestro,DT.no_valor1 AS DescripcionMaestro
		FROM maestro.tabla_detalle DT 
		INNER JOIN maestro.tabla_gen MG ON DT.nid_tabla_gen = MG.nid_tabla_gen AND DT.fl_inactivo = 0 AND MG.fl_inactivo = 0
		WHERE MG.nid_tabla_gen = @vi_co_tablatipoclientesexo
	)AS Z ON PE.co_sexo = Z.CodigoMaestro
	LEFT JOIN 
	(
		SELECT 
			DT.no_valor2 AS CodigoMaestro,DT.no_valor1 AS DescripcionMaestro
		FROM maestro.tabla_detalle DT 
		INNER JOIN maestro.tabla_gen MG ON DT.nid_tabla_gen = MG.nid_tabla_gen AND DT.fl_inactivo = 0 AND MG.fl_inactivo = 0
		WHERE MG.nid_tabla_gen = @vi_co_tablatipopersona
	)AS T ON PE.co_tipo_persona = T.CodigoMaestro
	LEFT JOIN 
	(
		SELECT 
			DT.no_valor2 AS CodigoMaestro,DT.no_valor1 AS DescripcionMaestro
		FROM maestro.tabla_detalle DT 
		INNER JOIN maestro.tabla_gen MG ON DT.nid_tabla_gen = MG.nid_tabla_gen AND DT.fl_inactivo = 0 AND MG.fl_inactivo = 0
		WHERE MG.nid_tabla_gen = @vi_co_tablatipoultimocontacto
	)AS U ON CLI.co_tipo_ultimo_contacto = U.CodigoMaestro
	WHERE (
		( @vi_qt_tipofiltro = 1 AND (
			(UPPER(PE.no_persona) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%' ) OR
			(UPPER(PE.no_apellido_paterno) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%' ) OR
			(UPPER(PE.no_apellido_materno) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%' ) OR
			(UPPER(PE.no_razon_social) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%') OR
			(UPPER(PE.no_apellido_paterno + ' ' + PE.no_apellido_materno + ' ' + PE.no_persona) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%') OR 
			(UPPER(PE.no_persona + ' ' + PE.no_apellido_paterno + ' ' + PE.no_apellido_materno) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%') 
		)) OR
		( @vi_qt_tipofiltro = 2 AND (PE.nu_documento LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%' )) OR
		( @vi_qt_tipofiltro = 3 AND (EXISTS(			
			SELECT CLICO.nid_cliente
			FROM cliente_contacto CLICO
			INNER JOIN contacto CO ON CLICO.nid_contacto = CO.nid_contacto AND CO.fl_inactivo = 0 AND CLICO.fl_inactivo = 0
			INNER JOIN persona PR ON CO.nid_contacto = PR.nid_persona AND PR.fl_inactivo = 0
			AND CLICO.nid_cliente = CLI.nid_cliente
			WHERE 
				(UPPER(PR.no_persona) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%' ) OR
				(UPPER(PR.no_apellido_paterno) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%' ) OR
				(UPPER(PR.no_apellido_materno) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%' ) OR				
				(UPPER(PR.no_apellido_paterno + ' ' + PR.no_apellido_materno + ' ' + PR.no_persona) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%') OR 
				(UPPER(PR.no_persona + ' ' + PR.no_apellido_paterno + ' ' + PR.no_apellido_materno) LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%') 

		))) OR
		( @vi_qt_tipofiltro = 4 AND (
			EXISTS(
				SELECT  VEH.nid_vehiculo
				FROM cliente_vehiculo CLIVEH 
				INNER JOIN vehiculo VEH ON CLIVEH.nid_vehiculo = VEH.nid_vehiculo
				AND CLIVEH.nid_cliente = CLI.nid_cliente
				WHERE VEH.nu_placa LIKE '%' + RTRIM(LTRIM(@vi_tx_desfiltro)) + '%'
			)			
		))
	)
	ORDER BY NumeroDocumento,NombreCompleto,FechaUltimoContacto,CodigoUltimoContacto ASC

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

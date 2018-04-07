--DECLARE @p  dbo.TRowCliente 
--EXEC [dbo].[sganet_sps_clienteadvanced] @p,NULL,NULL,'10',NULL,NULL,0,0,0,0

ALTER PROCEDURE [dbo].[sganet_sps_clienteadvanced]
/*****************************************************************************
Objetivo		: Busqueda Avanzada de Clientes por los filtros ingresados.
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (10/01/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 10/01/2018 Creacion
****************************************************************************/
(	
	@vi_tb_clientes dbo.TRowCliente READONLY,
	@vi_tx_marca VARCHAR(200)=null,
	@vi_tx_modelo VARCHAR(200)=null,
	@vi_tx_departamento VARCHAR(200)=null,
	@vi_tx_provincia VARCHAR(200)=null,
	@vi_tx_distrito VARCHAR(200)=null,
	@vi_tx_usefiltro1 BIT,
	@vi_tx_useventavehiculo BIT=NULL,
	@vi_tx_useservicio BIT=NULL,
	@vi_tx_repuesto BIT=NULL
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

	DECLARE @vt_clientes TABLE(
		IdCliente INT NOT NULL,
		NumeroDocumento CHAR(20) NULL,
		NombreCompleto VARCHAR(260) NULL,
		ApellidoPaterno VARCHAR(60) NULL,
		ApellidoMaterno VARCHAR(60) NULL,
		TipoDocumento VARCHAR(255) NULL,
		VentaVehiculo BIT NULL,
		Servicio BIT NULL,
		VentaRepuesto BIT NULL,
		FechaUltimoContacto DATETIME NULL,
		CodigoUltimoContacto VARCHAR(255) NULL,
		Genero VARCHAR(255) NULL,
		FechaNacimiento DATETIME NULL,
		Telefono VARCHAR(20) NULL,
		Celular VARCHAR(20) NULL,
		Correo VARCHAR(260) NULL,
		Direccion VARCHAR(260) NULL,
		Departamento VARCHAR(50) NULL,
		Provincia VARCHAR(50) NULL,
		Distrito VARCHAR(50) NULL,
		TipoPersona VARCHAR(255) NULL,
		CodTipoPersona VARCHAR(10) NULL
	)

	--Inicialización de Variables
	SET @vl_fl_transaccion = '0'

	--Transacción
	BEGIN TRANSACTION
	
	--Busqueda de Clientes
	INSERT INTO @vt_clientes
	SELECT DISTINCT 
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
		LTRIM(RTRIM(T.DescripcionMaestro)) AS TipoPersona,
		LTRIM(RTRIM(T.CodigoMaestro)) AS CodTipoPersona
	FROM persona PE
	INNER JOIN cliente CLI ON PE.nid_persona = CLI.nid_cliente AND PE.fl_inactivo = 0 AND CLI.fl_inactivo = 0
	LEFT JOIN persona_direccion PDR ON CLI.nid_direccion_facturacion = PDR.nid_direccion AND PDR.fl_Inactivo = 0
	LEFT JOIN maestro.ubigeo DEP ON PDR.coddpto = DEP.coddpto AND DEP.codprov = 00 AND DEP.coddist = 00
	LEFT JOIN maestro.ubigeo PROV ON PDR.codprov = PROV.codprov AND PDR.coddpto = PROV.coddpto AND PROV.coddist = 00
	LEFT JOIN maestro.ubigeo DIST ON PDR.coddist = DIST.coddist AND PDR.codprov = DIST.codprov AND PDR.coddpto = DIST.coddpto
	LEFT JOIN cliente_vehiculo CLVH ON CLI.nid_cliente = CLVH.nid_cliente AND CLVH.fl_inactivo = 0
	LEFT JOIN vehiculo VE ON CLVH.nid_vehiculo = VE.nid_vehiculo AND VE.fl_inactivo = 0
	LEFT JOIN marca MA ON VE.nid_marca = MA.nid_marca AND MA.fl_inactivo = 0
	LEFT JOIN modelo MO ON VE.nid_modelo = MO.nid_modelo AND MO.fl_inactivo = 0	
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
		--(
		--	((@vi_tx_useventavehiculo IS NULL) OR (CLI.fl_venta_vehiculo = ISNULL(@vi_tx_useventavehiculo,CLI.fl_venta_vehiculo))) AND
		--	((@vi_tx_useservicio IS NULL) OR (CLI.fl_vservicios = ISNULL(@vi_tx_useservicio,CLI.fl_vservicios))) AND
		--	((@vi_tx_repuesto IS NULL) OR (CLI.fl_venta_repuestos = ISNULL(@vi_tx_repuesto,CLI.fl_venta_repuestos)))
		--) AND
		(
			((LTRIM(RTRIM(@vi_tx_departamento)) IS NULL) OR (DEP.coddpto = ISNULL(LTRIM(RTRIM(@vi_tx_departamento)),DEP.coddpto))) AND
			((LTRIM(RTRIM(@vi_tx_provincia)) IS NULL) OR (PROV.codprov = ISNULL(LTRIM(RTRIM(@vi_tx_provincia)),PROV.codprov))) AND
			((LTRIM(RTRIM(@vi_tx_distrito)) IS NULL) OR (DIST.coddist = ISNULL(LTRIM(RTRIM(@vi_tx_distrito)),DIST.coddist)))
		) AND 
		(
			(LTRIM(RTRIM(@vi_tx_marca)) IS NULL) OR (MA.nid_marca = ISNULL(LTRIM(RTRIM(@vi_tx_marca)),MA.nid_marca)) 
		) AND
		(
			(LTRIM(RTRIM(@vi_tx_modelo)) IS NULL) OR (MO.nid_modelo = ISNULL(LTRIM(RTRIM(@vi_tx_modelo)),MO.nid_modelo)) 
		)
	)

	IF @vi_tx_usefiltro1 = 0
	BEGIN

		SELECT 
			T1.IdCliente,T1.NumeroDocumento,T1.NombreCompleto,T1.ApellidoPaterno,T1.ApellidoMaterno,
			T1.TipoDocumento,T1.VentaVehiculo,T1.Servicio,T1.VentaRepuesto,T1.FechaUltimoContacto,
			T1.CodigoUltimoContacto,T1.Genero,T1.FechaNacimiento,T1.Telefono,T1.Celular,T1.Correo,
			T1.Direccion,T1.Departamento,T1.Provincia,T1.Distrito,T1.TipoPersona
		FROM @vt_clientes T1
		ORDER BY NumeroDocumento,NombreCompleto,FechaUltimoContacto,CodigoUltimoContacto ASC

	END
	ELSE
	BEGIN

		SELECT 
			T1.IdCliente,T1.NumeroDocumento,T1.NombreCompleto,T1.ApellidoPaterno,T1.ApellidoMaterno,
			T1.TipoDocumento,T1.VentaVehiculo,T1.Servicio,T1.VentaRepuesto,T1.FechaUltimoContacto,
			T1.CodigoUltimoContacto,T1.Genero,T1.FechaNacimiento,T1.Telefono,T1.Celular,T1.Correo,
			T1.Direccion,T1.Departamento,T1.Provincia,T1.Distrito,T1.TipoPersona
		FROM @vt_clientes T1
		INNER JOIN @vi_tb_clientes T2 ON LTRIM(RTRIM(T1.NumeroDocumento)) = LTRIM(RTRIM(T2.NumeroDocumento)) --AND LTRIM(RTRIM(T1.CodTipoPersona)) = LTRIM(RTRIM(T2.TipoPersona))
		ORDER BY NumeroDocumento,NombreCompleto,FechaUltimoContacto,CodigoUltimoContacto ASC

	END
		
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
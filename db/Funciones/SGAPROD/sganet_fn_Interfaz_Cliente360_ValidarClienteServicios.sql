

CREATE FUNCTION sganet_fn_Interfaz_Cliente360_ValidarClienteServicios(	@vi_nu_documento VARCHAR(25))
RETURNS BIT 
AS 
BEGIN

    BEGIN TRY

		DECLARE @fl_cliente_servicios BIT= 0;
		
		--------------------------------------------------------------------------------------------------------------------------
		--Valida Si Cliente Servicios
		--------------------------------------------------------------------------------------------------------------------------
		--AGP
		IF(EXISTS
		  (
			SELECT Id_OT
			FROM 
			(
			SELECT TOP 1 ot.Id_OT
			FROM Tllr_OT ot WITH(NOLOCK)
			INNER JOIN Glbl_Cliente_Proveedor cli WITH(NOLOCK)
			ON ot.Id_Cliente_Proveedor=cli.Id_Cliente_Proveedor AND cli.Rut = @vi_nu_documento
			INNER JOIN Tllr_Tipo_Cargo ttc WITH(NOLOCK)
			ON ot.CargoDeducible=ttc.Id_Tipo_Cargo AND ttc.Facturable='S' AND ot.Estado IN ('V', 'L', 'F', 'B') AND ot.Seccion_OT='C'

			UNION ALL

			SELECT TOP 1 ot.Id_OT
			FROM Tllr_Mecanica_OT otm WITH(NOLOCK)
			INNER JOIN Tllr_OT ot WITH(NOLOCK)
			ON otm.Id_Empresa=ot.Id_Empresa AND otm.Id_Sucursal=ot.Id_Sucursal AND otm.Id_OT=ot.Id_OT AND otm.Seccion_OT=ot.Seccion_OT
			INNER JOIN Glbl_Cliente_Proveedor cli WITH(NOLOCK)
			ON ot.Id_Cliente_Proveedor=cli.Id_Cliente_Proveedor AND cli.Rut = @vi_nu_documento
			INNER JOIN Tllr_Tipo_Cargo ttc WITH(NOLOCK)
			ON otm.Id_Tipo_Cargo=ttc.Id_Tipo_Cargo AND ttc.Facturable='S' AND ot.Estado IN('V', 'L', 'F', 'B')

			UNION ALL

			SELECT TOP 1 ot.Id_OT
			FROM Tllr_Carroceria_OT otm WITH(NOLOCK)
			INNER JOIN Tllr_OT ot WITH(NOLOCK)
			ON otm.Id_Empresa=ot.Id_Empresa AND otm.Id_Sucursal=ot.Id_Sucursal AND otm.Id_OT=ot.Id_OT AND otm.Seccion_OT=ot.Seccion_OT
			INNER JOIN Glbl_Cliente_Proveedor cli WITH(NOLOCK)
			ON ot.Id_Cliente_Proveedor=cli.Id_Cliente_Proveedor AND cli.Rut = @vi_nu_documento
			INNER JOIN Tllr_Tipo_Cargo ttc WITH(NOLOCK)
			ON otm.Id_Tipo_Cargo=ttc.Id_Tipo_Cargo AND ttc.Facturable='S' AND ot.Estado IN('V', 'L', 'F', 'B')

			UNION ALL

			SELECT TOP 1 ot.Id_OT
			FROM Tllr_Otro_OT otm WITH(NOLOCK)
			INNER JOIN Tllr_OT ot WITH(NOLOCK)
			ON otm.Id_Empresa=ot.Id_Empresa AND otm.Id_Sucursal=ot.Id_Sucursal AND otm.Id_OT=ot.Id_OT AND otm.Seccion_OT=ot.Seccion_OT
			INNER JOIN Glbl_Cliente_Proveedor cli WITH(NOLOCK)
			ON ot.Id_Cliente_Proveedor=cli.Id_Cliente_Proveedor AND cli.Rut = @vi_nu_documento
			INNER JOIN Tllr_Tipo_Cargo ttc WITH(NOLOCK)
			ON otm.Id_Tipo_Cargo=ttc.Id_Tipo_Cargo AND ttc.Facturable='S' AND ot.Estado IN('V', 'L', 'F', 'B')

			UNION ALL

			SELECT TOP 1 ot.Id_OT
			FROM Tllr_Terceros_OT otm WITH(NOLOCK)
			INNER JOIN Tllr_OT ot WITH(NOLOCK)
			ON otm.Id_Empresa=ot.Id_Empresa AND otm.Id_Sucursal=ot.Id_Sucursal AND otm.Id_OT=ot.Id_OT AND otm.Seccion_OT=ot.Seccion_OT
			INNER JOIN Glbl_Cliente_Proveedor cli WITH(NOLOCK)
			ON ot.Id_Cliente_Proveedor=cli.Id_Cliente_Proveedor AND cli.Rut = @vi_nu_documento
			INNER JOIN Tllr_Tipo_Cargo ttc WITH(NOLOCK)
			ON otm.Id_Tipo_Cargo=ttc.Id_Tipo_Cargo AND ttc.Facturable='S' AND ot.Estado IN('V', 'L', 'F', 'B')

			UNION ALL

			SELECT TOP 1 ot.Id_OT
			FROM Tllr_Repuestos_OT otm WITH(NOLOCK)
			INNER JOIN Tllr_OT ot WITH(NOLOCK)
			ON otm.Id_Empresa=ot.Id_Empresa AND otm.Id_Sucursal=ot.Id_Sucursal AND otm.Id_OT=ot.Id_OT AND otm.Seccion_OT=ot.Seccion_OT
			INNER JOIN Glbl_Cliente_Proveedor cli WITH(NOLOCK)
			ON ot.Id_Cliente_Proveedor=cli.Id_Cliente_Proveedor AND cli.Rut = @vi_nu_documento
			INNER JOIN Tllr_Tipo_Cargo ttc WITH(NOLOCK)
			ON otm.Id_Tipo_Cargo=ttc.Id_Tipo_Cargo AND ttc.Facturable='S' AND ot.Estado IN('V', 'L', 'F', 'B')
			) AS Data
		  ))
			BEGIN
				SET @fl_cliente_servicios = 1;
		
				PRINT GETDATE()

			END;

		SELECT @fl_cliente_servicios AS ClienteServicios;

    END TRY  
    BEGIN CATCH  
        SELECT ERROR_NUMBER() AS ERROR;  
    END CATCH;


END




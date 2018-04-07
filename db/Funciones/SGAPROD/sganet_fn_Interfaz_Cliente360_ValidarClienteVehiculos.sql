USE [SGAPROD]
GO
/****** Object:  UserDefinedFunction [dbo].[sganet_fn_Interfaz_Cliente360_ValidarClienteVehiculos]    Script Date: 5/01/2018 15:58:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[sganet_fn_Interfaz_Cliente360_ValidarClienteVehiculos](@vi_nu_documento VARCHAR(25))
RETURNS BIT 
AS 
BEGIN

	DECLARE @fl_cliente_vehiculos BIT= 0;
		--------------------------------------------------------------------------------------------------------------------------
		--Valida Si Cliente Vehiculos
		--------------------------------------------------------------------------------------------------------------------------
		IF EXISTS
		(
			SELECT TOP 1 np.nid_nota_pedido
			FROM mae_cliente c
				 INNER JOIN mae_cliente_direccion d ON d.nid_cliente = c.nid_cliente AND d.fl_inactivo = '0'
				 INNER JOIN tbl_nota_pedido np ON np.nid_cliente = d.nid_cliente_direccion
												  AND np.fl_inactivo = '0'
												  AND np.co_estado IN('0010', '0011', '0006')--Validar
										  AND np.co_canal_venta IN('CP', 'SU')--Validar
			WHERE c.nu_documento = @vi_nu_documento
				  AND c.fl_inactivo = '0'
		)
			BEGIN
				SET @fl_cliente_vehiculos = 1;
			END;
		ELSE
		BEGIN

			IF EXISTS
			(
				SELECT TOP 1 np.nid_nota_pedido
				FROM mae_cliente c
					 INNER JOIN mae_cliente_direccion d ON d.nid_cliente = c.nid_cliente AND d.fl_inactivo = '0'
					 INNER JOIN tbl_nota_pedido np ON np.nid_propietario = d.nid_cliente_direccion
													  AND np.fl_inactivo = '0'
													  AND np.co_estado IN('0010', '0011', '0006')--Validar
											  AND np.co_canal_venta IN('CP', 'SU')--Validar
				WHERE c.nu_documento = @vi_nu_documento
					  AND c.fl_inactivo = '0'
			)
				BEGIN
					SET @fl_cliente_vehiculos = 1;
				END;


		END

		RETURN @fl_cliente_vehiculos
END;
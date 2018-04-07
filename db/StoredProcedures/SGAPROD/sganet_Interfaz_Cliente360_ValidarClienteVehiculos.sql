
-- sganet_Interfaz_Cliente360_ValidarClienteVehiculos '20512170120'
CREATE PROCEDURE sganet_Interfaz_Cliente360_ValidarClienteVehiculos 
/***************************************************************************  
Objetivo  : Validar si el cliente existe transacciones de vehiculos
Autor   : ABL               
Fecha Creación : 21/12/2017
Autor Modifica :               
Fecha Modifica :               
Notas   : 
****************************************************************************/    
	@vi_nu_documento VARCHAR(25)
AS
BEGIN

    BEGIN TRY

		DECLARE @fl_cliente_vehiculos BIT= 0;
		--------------------------------------------------------------------------------------------------------------------------
		--Valida Si Cliente Vehiculos
		--------------------------------------------------------------------------------------------------------------------------
		IF EXISTS
		(
			SELECT TOP 1 np.nid_nota_pedido
			FROM mae_cliente c
				 INNER JOIN mae_cliente_direccion d ON d.nid_cliente = c.nid_cliente
													   AND d.fl_inactivo = '0'
				 INNER JOIN tbl_nota_pedido np ON ISNULL(np.nid_cliente, np.nid_propietario) = d.nid_cliente_direccion
												  AND np.fl_inactivo = '0'
												  AND np.co_estado IN('0010', '0011', '0006')--Validar
										  AND np.co_canal_venta IN('CP', 'SU')--Validar
			WHERE c.nu_documento = @vi_nu_documento
				  AND c.fl_inactivo = '0'
		)
			BEGIN
				SET @fl_cliente_vehiculos = 1;
			END;

		SELECT @fl_cliente_vehiculos AS ClienteVehiculos

    END TRY  
    BEGIN CATCH  
        SELECT ERROR_NUMBER() AS ERROR;  
    END CATCH;


END
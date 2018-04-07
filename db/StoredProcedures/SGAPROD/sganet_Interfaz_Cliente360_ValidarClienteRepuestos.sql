
-- sganet_Interfaz_Cliente360_ValidarClienteRepuestos '20512170120'
CREATE PROCEDURE sganet_Interfaz_Cliente360_ValidarClienteRepuestos
/***************************************************************************  
Objetivo  : Validar si el cliente existe transacciones de repuestos
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

		DECLARE @fl_cliente_repuestos BIT= 0;
		--------------------------------------------------------------------------------------------------------------------------
		--Valida Si Cliente Repuestos
		--------------------------------------------------------------------------------------------------------------------------
		IF(EXISTS
		  (
			  SELECT TOP 1 g.nid_pedido
			  FROM mae_cliente c
				   INNER JOIN mae_cliente_direccion d ON d.nid_cliente = c.nid_cliente
														 AND d.fl_inactivo = '0'
				   INNER JOIN gnet_pedido g ON g.nid_cliente_direccion = d.nid_cliente_direccion
											   AND g.fl_inactivo = '0'
											   AND g.co_estado IN('006')--Validar
										AND g.co_canal_venta NOT IN('CO')--Validar
			  WHERE c.nu_documento = @vi_nu_documento
					AND c.fl_inactivo = '0'
		  ))
			BEGIN
				SET @fl_cliente_repuestos = 1;
			END;

		SELECT @fl_cliente_repuestos AS ClienteRepuestos

    END TRY  
    BEGIN CATCH  
        SELECT ERROR_NUMBER() AS ERROR;  
    END CATCH;


END
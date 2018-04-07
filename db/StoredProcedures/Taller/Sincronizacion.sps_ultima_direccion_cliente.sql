CREATE PROC  Sincronizacion.sps_ultima_direccion_cliente
@tipo_documento VARCHAR(20),
@numero_documento VARCHAR(20)
AS
BEGIN
	SELECT Id_Cliente_Proveedor
			FROM Glbl_Cliente_Proveedor  
			WHERE Tipo_Docto_Identidad = @tipo_documento AND Rut = @numero_documento;
END

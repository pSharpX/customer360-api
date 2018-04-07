USE Cliente360_Dev;
go

CREATE PROC sganet_sps_documento_identidad_facturacion
  @fecha_cliente CHAR(20)
AS
BEGIN
	SELECT cliente_numero_documento as nu_documento
	FROM interfaz_facturacion 
	WHERE CONVERT(CHAR(20),fecha_registro,112) = @fecha_cliente
END



use autopro_agp;
go



CREATE TYPE  dbo.list_tabla_servicios
AS TABLE
(
  numero_documento VARCHAR(20)
);
GO

CREATE PROC sganet_sps_interfaz_flag_servicios_cliente
  @list AS list_tabla_servicios READONLY
AS
BEGIN
  SET NOCOUNT ON;
 SELECT DISTINCT RTRIM(Rut) as numero_documento,
 dbo.sganet_fn_Interfaz_Cliente360_ValidarClienteServicios(Rut) as Servicio 
 FROM Glbl_Cliente_Proveedor WHERE Rut in (SELECT numero_documento FROM @list)
END


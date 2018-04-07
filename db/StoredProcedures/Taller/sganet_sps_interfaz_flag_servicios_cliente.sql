/*****************************************************************************
Objetivo		: Obtiene los ultimos servicios de clientes
Autor			: Hundred(Jedion Melbin).
Fecha Creación	: Fecha de creación del procedure. (01/02/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(Jedion Melbin) 01/02/2018 Creacion
****************************************************************************/
 

ALTER PROC sganet_sps_interfaz_flag_servicios_cliente
  @list AS list_tabla_servicios READONLY
AS
BEGIN
  SET NOCOUNT ON;
 SELECT DISTINCT RTRIM(Rut) as numero_documento,
 dbo.sganet_fn_Interfaz_Cliente360_ValidarClienteServicios(Rut) as Servicio 
 FROM Glbl_Cliente_Proveedor WHERE Rut in (SELECT numero_documento FROM @list)
END


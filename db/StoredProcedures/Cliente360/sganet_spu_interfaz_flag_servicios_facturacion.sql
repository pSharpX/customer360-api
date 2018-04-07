/*****************************************************************************
Objetivo		: Actualiza el campo servicio en la tabla temporal interfaz facturacion.
Autor			: Hundred(Jedion Melbin).
Fecha Creación	: Fecha de creación del procedure. (16/01/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(Jedion Melbin) 16/01/2018 Creacion
****************************************************************************/

ALTER PROC sganet_spu_interfaz_flag_servicios_facturacion
@num_documento VARCHAR(20),
@servicio int
AS
BEGIN
	UPDATE   interfaz_facturacion 
		SET  flag_servicios = @servicio
		WHERE cliente_numero_documento  = @num_documento;
END



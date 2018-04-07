USE Cliente360_Dev;
go


CREATE PROC sganet_spu_interfaz_flag_servicios_facturacion
@num_documento VARCHAR(20),
@servicio int
AS
BEGIN
	UPDATE   interfaz_facturacion 
		SET  flag_servicios = @servicio
		WHERE cliente_numero_documento  = @num_documento;
END

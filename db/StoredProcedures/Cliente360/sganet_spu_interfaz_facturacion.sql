USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sganet_spu_interfaz_facturacion]    Script Date: 7/02/2018 11:02:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[sganet_spu_interfaz_facturacion]
@num_documento VARCHAR(20),
@servicio int
AS
BEGIN
	UPDATE   interfaz_facturacion 
		SET  flag_servicios= @servicio
		WHERE cliente_numero_documento  = @num_documento;
END


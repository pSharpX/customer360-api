USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sganet_sps_facturacion_servicios_clientes]    Script Date: 7/02/2018 11:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[sganet_sps_facturacion_servicios_clientes]
  @fecha_cliente VARCHAR(20)
AS
BEGIN
	SELECT 
	 rtrim(cliente_numero_documento) as nu_documento
	 FROM interfaz_facturacion WHERE CONVERT(VARCHAR(20),fecha_registro,112)= @fecha_cliente
END

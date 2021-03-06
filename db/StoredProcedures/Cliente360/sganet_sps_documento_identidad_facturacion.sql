USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sganet_sps_documento_identidad_facturacion]    Script Date: 7/02/2018 10:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[sganet_sps_documento_identidad_facturacion]
  @fecha_cliente CHAR(20)
AS
BEGIN
	SELECT cliente_numero_documento as nu_documento
	FROM interfaz_facturacion 
	WHERE CONVERT(CHAR(20),fecha_registro,112) = @fecha_cliente
END


USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sgsnet_sps_estado_genero]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sgsnet_sps_estado_genero]
AS
BEGIN
	SELECT  
	nid_tabla_gen_det as TablaId,
	UPPER(no_valor1) as Valor1,
	no_valor2 as Valor2,
	no_valor3 as Valor3,
	no_valor4 as Valor4,
	no_valor5 as Valor5
	 FROM maestro.tabla_detalle where nid_tabla_gen = 94
END
GO

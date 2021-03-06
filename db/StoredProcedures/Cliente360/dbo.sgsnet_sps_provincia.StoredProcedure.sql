USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sgsnet_sps_provincia]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[sgsnet_sps_provincia]
 @departamento char(5)
AS
BEGIN
SELECT 
 RTRIM(codprov) as Codigo,
 RTRIM(nombre) as Nombre 
 FROM maestro.ubigeo 
 WHERE coddpto = @departamento AND coddist='00' AND codprov <> '00' order by nombre
END;
GO

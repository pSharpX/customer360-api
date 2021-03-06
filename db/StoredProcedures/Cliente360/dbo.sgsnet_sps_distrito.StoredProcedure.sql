USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sgsnet_sps_distrito]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sgsnet_sps_distrito]
 @departamento char(5),
 @provincia char(5)
AS
BEGIN
SELECT 
 ltrim(coddist) as Codigo,
 ltrim(nombre)  as Nombre 
 FROM maestro.ubigeo 
 WHERE coddpto = @departamento AND codprov= @provincia AND coddist <> '00' order by nombre;
END;
GO

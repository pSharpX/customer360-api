USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sgsnet_sps_departamento]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sgsnet_sps_departamento]
AS
BEGIN
SELECT 
 RTRIM(coddpto) as Codigo,
 RTRIM(nombre) as Nombre 
 FROM maestro.ubigeo 
 WHERE codprov = '00' AND coddist='00' order by nombre;
END;
GO

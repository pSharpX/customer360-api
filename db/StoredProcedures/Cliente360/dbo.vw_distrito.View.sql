USE [Cliente360_Dev]
GO
/****** Object:  View [dbo].[vw_distrito]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_distrito] AS
SELECT 
 coddpto as Departamento,
 codprov as Provincia,
 coddist as Distrito,
 nombre as Nombre 
 FROM maestro.ubigeo 
 WHERE  coddist <> '00'
GO

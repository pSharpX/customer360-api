USE [Cliente360_Dev]
GO
/****** Object:  View [dbo].[vw_provincia]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_provincia] AS
SELECT 
 coddpto as Departamento,
 codprov as Provincia,
 nombre as Nombre 
 FROM maestro.ubigeo 
 WHERE coddist='00' AND codprov <> '00'
GO

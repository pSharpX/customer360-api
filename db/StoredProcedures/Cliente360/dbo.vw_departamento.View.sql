USE [Cliente360_Dev]
GO
/****** Object:  View [dbo].[vw_departamento]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_departamento] AS
SELECT 
 coddpto as Codigo,
 nombre as Nombre 
 FROM maestro.ubigeo 
 WHERE codprov = '00' AND coddist='00';
GO

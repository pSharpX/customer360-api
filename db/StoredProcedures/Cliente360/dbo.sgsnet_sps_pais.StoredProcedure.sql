USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sgsnet_sps_pais]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sgsnet_sps_pais] 
AS
BEGIN
	SELECT  nid_pais as IdPais, rtrim(no_pais) as Nombre, rtrim(co_pais_spring) as CodigoPais FROM mae_pais order by no_pais
END
GO

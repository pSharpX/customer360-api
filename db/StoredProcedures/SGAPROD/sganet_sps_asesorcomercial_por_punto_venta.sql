/****** Object:  StoredProcedure [dbo].[sganet_sps_asesorcomercial_por_punto_venta]    Script Date: 16/02/2018 15:24:33 ******/
IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'sganet_sps_asesorcomercial_por_punto_venta')
BEGIN
    DROP PROCEDURE [dbo].[sganet_sps_asesorcomercial_por_punto_venta]
END
GO 

/****** Object:  StoredProcedure [dbo].[sganet_sps_asesorcomercial_por_punto_venta]    Script Date: 16/02/2018 15:24:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sganet_sps_asesorcomercial_por_punto_venta]
/*****************************************************************************
Objetivo		: Listar Asesor Comercial por punto de Venta.
Autor			: Hundred(Christian Rivera).
Fecha Creación	: Fecha de creación del procedure. (16/02/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(Christian Rivera) 16/02/2018 Creacion
****************************************************************************/
(
	@nid_punto_venta int
)
AS
BEGIN 

	SELECT DISTINCT
		 u.nid_usuario AS codAsesor, 
		 UPPER((LTRIM(RTRIM(u.VNOMUSR)) + ' ' + LTRIM(RTRIM(u.no_ape_paterno)) + ' ' + LTRIM(RTRIM(u.no_ape_paterno)))) AS nombreAsesor
	from SGAPROD.dbo.usr u 
	INNER JOIN SGAPROD.dbo.prfusr pu on pu.nid_usuario = u.nid_usuario and pu.fl_inactivo='0'
	INNER JOIN SGAPROD.dbo.PRF p on p.nid_perfil = pu.nid_perfil
	WHERE u.fl_inactivo='0' and p.nid_perfil = 9 and (coalesce(nid_punto_venta, '') != '' and nid_punto_venta = @nid_punto_venta)
	ORDER BY nombreAsesor ASC

END 

GO


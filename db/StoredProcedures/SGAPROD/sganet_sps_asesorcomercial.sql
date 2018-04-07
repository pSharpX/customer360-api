

ALTER PROCEDURE sganet_sps_asesorcomercial
/*****************************************************************************
Objetivo		: Listar Asesor Comercial.
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (01/02/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 01/02/2018 Creacion
****************************************************************************/
AS
BEGIN 

	SELECT DISTINCT
		 u.nid_usuario AS codAsesor, 
		 UPPER((LTRIM(RTRIM(u.VNOMUSR)) + ' ' + LTRIM(RTRIM(u.no_ape_paterno)) + ' ' + LTRIM(RTRIM(u.no_ape_paterno)))) AS nombreAsesor
	from SGAPROD.dbo.usr u 
	INNER JOIN SGAPROD.dbo.prfusr pu on pu.nid_usuario = u.nid_usuario and pu.fl_inactivo='0'
	INNER JOIN SGAPROD.dbo.PRF p on p.nid_perfil = pu.nid_perfil
	WHERE u.fl_inactivo='0' and p.nid_perfil = 9
	ORDER BY nombreAsesor ASC

END 
GO
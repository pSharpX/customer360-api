
CREATE PROC [dbo].[sgsnet_sps_marca]
/*****************************************************************************
Objetivo		: Listado de Marca.
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (01/02/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 01/02/2018 Creacion
****************************************************************************/
 
AS
BEGIN
	SELECT 
		nid_marca AS IdMarca,
		co_marca AS CodigoMarca,
		no_marca AS NombreMarca
	FROM marca WHERE fl_inactivo = 0
END
GO

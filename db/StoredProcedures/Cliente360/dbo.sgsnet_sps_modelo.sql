
CREATE PROC [dbo].[sgsnet_sps_modelo]
/*****************************************************************************
Objetivo		: Listado de Modelo.
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (01/02/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 01/02/2018 Creacion
****************************************************************************/
 
AS
BEGIN
	SELECT 
		nid_modelo AS IdModelo,
		co_modelo AS CodigoModelo,
		no_modelo AS NombreModelo
	FROM modelo WHERE fl_inactivo = 0
END
GO

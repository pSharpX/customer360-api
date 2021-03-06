
ALTER PROC [dbo].[sgsnet_sps_puntoventa]
/*****************************************************************************
Objetivo		: Listado de Punto Venta(Sucursal).
Autor			: Hundred(David Cruz).
Fecha Creación	: Fecha de creación del procedure. (01/02/2018)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HUNDRED(David Cruz) 01/02/2018 Creacion
****************************************************************************/
 
AS
BEGIN
	SELECT 
		nid_punto_venta AS IdPuntoVenta,
		UPPER(no_punto_venta) AS NombrePuntoVenta
	FROM mae_punto_venta WHERE fl_inactivo = 0
END
GO

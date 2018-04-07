

CREATE PROCEDURE sganet_Interfaz_Cliente360_CamposCalculados
AS
BEGIN

--DECLARE @fecha DATETIME = CONVERT(CHAR(8),GETDATE() ,112)

DECLARE @fecha DATETIME = '20170801'

--SELECT np.nid_nota_pedido, np.nid_cliente AS IdClienteDireccion,nid_contacto AS IdContactoDireccion,nid_propietario AS IdPropietarioDireccion
--,co_estado AS CodigoEstado,fe_estado AS  FechaEstado, nid_asesor_comercial AS IdAsesorComercial
--,nid_contacto AS nid_ultimo_contacto
--,cd.nid_cliente
--FROM [dbo].[tbl_nota_pedido] np
--	INNER JOIN mae_cliente_direccion cd ON np.nid_propietario=cd.nid_cliente_direccion
--	INNER JOIN mae_cliente_direccion contactoDireccion ON np.nid_cliente=contactoDireccion.nid_cliente_direccion
--	INNER JOIN mae_cliente cliente on cd.nid_cliente=cliente.nid_cliente
--	INNER JOIN mae_cliente asesor on np.nid_asesor_comercial=asesor.nid_cliente
--	INNER JOIN mae_cliente contacto on contactoDireccion.nid_cliente=contacto.nid_cliente

--WHERE fe_registro >= @fecha AND fe_registro<@fecha+1

SELECT DISTINCT cliente.co_tipo_documento AS  ClienteTipoDocumento, cliente.nu_documento AS ClienteNumeroDocumento
	,asesor.co_tipo_documento AS AsesorTipoDocumento, asesor.nu_documento AS AsesorNumeroDocumento
	,contacto.co_tipo_documento  AS ContactoTipoDocumento, contacto.nu_documento AS ContactoNumeroDocumento,
	cd.nid_cliente_direccion AS IdDireccionFacturacionSGA,
	CAST(1 AS BIT) AS FlagVentaVehiculos,CAST(0 AS BIT) AS FlagRepuestos,CAST(0 AS BIT) AS FlagServicios
FROM [dbo].[tbl_nota_pedido] np
	INNER JOIN mae_cliente_direccion cd ON np.nid_propietario=cd.nid_cliente_direccion
	INNER JOIN mae_cliente_direccion contactoDireccion ON np.nid_cliente=contactoDireccion.nid_cliente_direccion
	INNER JOIN mae_cliente cliente on cd.nid_cliente=cliente.nid_cliente
	INNER JOIN mae_cliente asesor on np.nid_asesor_comercial=asesor.nid_cliente
	INNER JOIN mae_cliente contacto on contactoDireccion.nid_cliente=contacto.nid_cliente

WHERE fe_registro >= @fecha AND fe_registro<@fecha+1
ORDER BY 1, 2 ASC

END
GO
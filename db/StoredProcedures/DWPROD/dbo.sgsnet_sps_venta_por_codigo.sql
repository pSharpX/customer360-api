USE [DWPROD]
GO

/****** Object:  StoredProcedure [dbo].[sgsnet_sps_venta_por_codigo]    Script Date: 19/01/2018 17:20:37 ******/
DROP PROCEDURE [dbo].[sgsnet_sps_venta_por_codigo]
GO

/****** Object:  StoredProcedure [dbo].[sgsnet_sps_venta_por_codigo]    Script Date: 19/01/2018 17:20:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sgsnet_sps_venta_por_codigo]
/*****************************************************************************
Objetivo		: Búsqueda de venta por Cliente y codigo(Numero Nota Pedido).
Autor			: Hundred(Christian Rivera).
Fecha Creación	: (22/01/2018 16:28)
Historial		: El procedimiento almacenado busca una determinado venta por Cliente y codigo.
****************************************************************************
@001 HUNDRED(Christian Rivera) 22/01/2018 Creacion
****************************************************************************/
(
    @numeroDocumento varchar(14),
	@numeroNotaPedido varchar(50)
)
AS
BEGIN
------------------------------------------------------------------------------
 -- Ventas
 ------------------------------------------------------------------------------
 select
    --nid_dw_unidades_facturadas,
    fe_emisionR as 'FechaFacturacion',
    --spec_code,
    no_color as 'NombreColor',
    --co_color,
    --nid_nota_pedido_conciliacion,
    --nid_nota_pedido,
    nu_nota_pedido as 'NumeroNotaPedido',
    --nid_vin,
    nu_vin as 'VIN',
    --nid_marca,
    --co_marca,
    no_marca as 'NombreMarca',
    co_familia as 'CodigoFamiliaCorto',
    no_familia as 'NombreFamiliaCorto',
    --nid_modelo,
    --co_modelo,
    no_modelo as 'NombreModelo',
    --nid_spec_code_comercial,
    no_comercial as 'NombreComercial',
    co_canal_venta as 'CodigoCanalVenta',
    no_canal_venta as 'NombreCanalVenta',
    --nid_punto_venta,
    no_punto_venta as 'NombrePuntoVenta',
    --nid_usuario,
    no_asesor_comercial as 'NombreAsesor',
    co_tipo_comprobante as 'CodigoTipoComprobante',
    dsc_tipo_comprobante as 'NombreTipoComprobante',
    nu_serie as 'ComprobanteSerie',
    nu_comprobante as 'ComprobanteNumero',
    --nid_cliente,
    co_tipo_cliente as 'CodigoTipoCliente',
    dsc_tipo_cliente as 'NombreTipoCliente',
    nu_documento_cliente as 'NumeroDocumento',
    no_cliente as 'NombreCliente',
    fe_emision as 'FechaEmision',
    --nid_moneda,
    no_moneda as 'NombreMoneda',
    mt_importe as 'ImporteVenta',
    qt_cantidad as 'Cantidad',
    --fl_procesado,
    --fe_proceso_adv,
    --co_usu_proceso_adv,
    --fe_proceso_supervision,
    --co_usu_proceso_supervision,
    --fe_proceso_envio_mat,
    --co_usu_proceso_envio_mat,
    --in_dia_proceso,
    --in_mes_proceso,
    --in_anho_proceso,
    --co_usuario_crea,
    --fe_crea,
    --co_usuario_cambio,
    --fe_cambio,
    --no_usuario_red,
    --no_estacion_red,
    --fl_inactivo,
    --co_estado,
    --fl_demo,
    --fl_estado,
    --co_estado_comprobante,
    --co_tipo_venta,
    --fl_tipo_retail,
    --nid_empresa,
    no_forma_pago as 'NombreFormaPago',
    --no_dpto_pv,
    an_fabricacion as 'AñoFabricacion',
    an_modelo as 'AñoModelo',
    --no_departamento as 'Departamento',
    mt_precio_cierre as 'MontoPrecioCierre',
    mt_precio_lista as 'MontoPrecioLista',
    mt_precio_venta as 'MontoPrecioVenta',
    co_pedido as 'CodigoPedido',
    no_tipo_venta as 'NombreTipoVenta',
    --no_provincia as 'Provincia',
    --no_distrito as 'Distrito',
    no_estado_comercial as 'NombreEstadoComercial',
    fe_cancelado_ult as 'FechaCancelacion',
    --nu_telefono as 'Telefono',
    --nu_celular as 'Celular',
    --no_direccion as 'Direccion',
    --no_correo as 'E-mail',
    --no_tipo_cliente_garantia as 'TipoClienteGarantia',
    --no_documento_garantia as 'NumeroDocumentoClienteGarantia',
    --no_cliente_garantia as 'NombreClienteGarantia',
    --nu_telefono_garantia as 'TelefonoGarantia',
    --nu_celular_garantia as 'CelularGatantia',
    --no_direccion_garantia,
    --no_correo_garantia as 'E-mail_Garantia',
    --fe_activacion_garantia as 'Fecha_Activa_Garantia',
    --no_dpto_cliente as 'Departamento_Cliente',
    --no_prov_cliente as 'Provincia_Cliente',
    --no_dist_cliente as 'Distrito_Cliente',
    --no_dpto_cliente_garantia as 'Departamento_Garantia', 
    --no_prov_cliente_garantia as 'Provincia_Garantia',
    --no_dist_cliente_garantia as 'Distrito_Garantia',
    --qt_antiguedad,
    --fe_nacimiento as 'Fecha_Nacimiento',
    --no_sexo as 'Sexo',
    no_estado_np as 'EstadoNotaPedido',
    --fl_retoma as 'Es_Retoma',
    no_empresa as 'NombreEmpresa',
    nu_placa as 'NumeroPlaca',
    co_periodo as 'Periodo',
    --co_compania_spring,
    --co_sucursal_spring,
    --no_sucursal_spring,
    --co_marca_spring,
    --no_marca_spring,
    --co_centro_costo_spring,
    --no_centro_costo_spring,
    --co_canal_spring,
    --no_canal_spring,
    --no_concepto,
    dsc_clasifica_venta as 'NombreClasificacionVenta',
    co_marca_gerencial as 'MarcaGerencial',
    fl_entrega_inmediata as 'EntregaInmediata',
    fe_sol_placa as 'FechaSolicitudPlaca',
    fe_asigna_placa as 'FechaAsignacionPlaca',
    no_cita as 'NombreCita',
    no_estado_pdi as 'EstadoPDI',
    fe_sol_movimiento_pdi as 'FechaSolicitudPDI',
    fe_fin_pdi as 'FechaFinPDI',
    no_estado_despacho as 'EstadoDespacho',
    fl_libre_warrant as 'LibreWarrant',
    no_ubica as 'NombreUbicacion',
    fe_reserva as 'FechaReserva',
    fe_eta as 'FechaETA',
    fe_arribo as 'FechaArribo',
    fl_desaduanada as 'Desaduanada',
    fe_liberacion_warrant as 'FechaLiberacionWarrant',
    fe_estima_inicio_pdi as 'FechaEstimacionInicioPDI',
    fe_estima_fin_pdi as 'FechaEstimacionFinPDI',
    fe_sol_despacho as 'FechaSolicitudDespacho',
    fe_estima_despacho as 'FechaEstimacionDespacho',
    fe_despacho as 'FechaDespacho',
    fe_cita as 'FechaCita',
    fe_entrega as 'FechaEntrega'
    --fl_inactivo_dw,
    --fe_crea_dw,
    --co_usuario_crea_dw,
    --fe_cambio_dw,
    --co_usuario_cambio_dw,
    --no_usuario_red_dw,
    --no_estacion_red_dw
 from tbl_dw_unidades_facturadas
 where fl_inactivo_dw='0' and fl_inactivo='0'
   and (nu_documento_cliente = @numeroDocumento and nu_nota_pedido = @numeroNotaPedido)
 
  END
GO
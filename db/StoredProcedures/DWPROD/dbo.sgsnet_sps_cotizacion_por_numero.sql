USE [DWPROD]
GO

/****** Object:  StoredProcedure [dbo].[sgsnet_sps_cotizacion_por_numero]    Script Date: 11/01/2018 10:33:04 ******/
DROP PROCEDURE [dbo].[sgsnet_sps_cotizacion_por_numero]
GO

/****** Object:  StoredProcedure [dbo].[sgsnet_sps_cotizacion_por_numero]    Script Date: 11/01/2018 10:33:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sgsnet_sps_cotizacion_por_numero]
/*****************************************************************************
Objetivo		: Búsqueda de Cotizacion por Cliente y Codigo(Numero de cotizacion).
Autor			: Hundred(Christian Rivera).
Fecha Creación	: (11/01/2018 18:42)
Historial		: El procedimiento almacenado busca una determinada cotizaciones por cliente y codigo.
****************************************************************************
@001 HUNDRED(Christian Rivera) 11/01/2018 Creacion
****************************************************************************/
(
	@numeroCliente varchar(14),
    @numeroCotizacion varchar(22)    
)
AS
BEGIN
 ------------------------------------------------------------------------------
 -- Cotizaciones
 ------------------------------------------------------------------------------
 select    
	nid_cotizacion as 'IdCotizacion',
    fe_registro as 'FechaRegistro',    
    nu_cotizacion as 'NumeroCotizacion',
    no_estado as 'NombreEstado',
    no_vendedor as 'NombreEmpleado',
    --fe_registro_str,
    --ho_registro,
    dsc_tipo_cliente as 'TipoCliente',
    nu_documento_cliente as 'NumeroDocumento',
    --nu_documento,
    no_cliente as 'NombreCliente',
    --no_empresa,
    --nu_telefono as 'Telefono',
    --no_direccion as 'Direccion',
    --no_ubigeo as 'Ubigeo',
    --no_distrito as 'Distrito',
    --no_correo as 'E-mail',
    no_comercial as 'NombreComercial',
    no_carroceria as 'NombreCarroceria',
    co_familia as 'CodigoFamilia',
    no_familia as 'NombreFamilia',
    an_modelo as 'AñoModelo',
    an_fabricacion as 'AñoFabricacion',
    no_color_exterior as 'NombreColor',
    no_forma_pago as 'NombreFormaPago',
    mt_precio_lista as 'MontoPrecioLista',
    mt_precio_cierre as 'MontoPrecioCierre',
    mt_precio_venta as 'MontoPrecioVenta',
	
    no_marca as 'NombreMarca',
    no_modelo as 'NombreModelo',
    --qt_dia_registro,
    --no_dia_registro,
    qt_cantidad as 'Cantidad',
    co_modelo as 'CodigoModelo',
    co_marca as 'CodigoMarca',
    no_modo_captacion as 'NombreModoCaptacion',
    --qt_dias_trans,
    fe_ultimo_contacto as 'FechaUltimoContacto',
    fe_proximo_contacto as 'FechaProximoContacto',
    nid_punto_venta as 'IdPuntoVenta',
    nid_ubica as 'IdUbica',

    no_jefe_ventas as 'NombreJefeVentas',
    co_canal_venta as 'CodigoCanalVenta',
    no_canal_venta as 'NombreCanalVenta',
    no_punto_venta as 'NombrePuntoVenta',
    --qt_test_drive,
    --no_sexo as 'Sexo',
    --fe_nacimiento as 'FechaNacimiento',
    --nu_telefono_dir_cont as 'Telefono',
    --no_correo_cont as 'E-mail',
    --no_direccion_2,
    --no_dpto_cliente as 'Departamento',
    --no_prov_cliente as 'Provincia',
    --no_dist_cliente as 'Distrito',
    no_empresa_coti as 'Empresa',
    --nid_spec_code_comercial,
	co_tipo_Venta as 'CodigoTipoVenta',
    case co_tipo_Venta 
	   when '0001' then 'Venta Normal'
	   when '0002' then 'Venta Liberada'
	   when '0003' then 'Venta Demo (Activo Fijo)'
	   when '0004' then 'Venta a Empleado'
	   when '0007' then 'Venta AMICAR con Bono'
	   when '0011' then 'Precios Especiales'
	   when '0012' then 'Venta AMICAR sin Bono'
	   when '0013' then 'Venta Amicar 50/50' else co_tipo_Venta end as 'NombreTipoVenta',
    fe_ultimo_lead as 'FechaUltimoLead'
   
 from tbl_dw_cotizaciones
 where fl_inactivo_dw = '0' and (nu_documento_cliente = @numeroCliente and nu_cotizacion = @numeroCotizacion)
   
  END
GO
USE [DWPROD]
GO

/****** Object:  StoredProcedure [dbo].[sgsnet_sps_testdrive_por_cliente]    Script Date: 15/01/2018 11:02:52 ******/
DROP PROCEDURE [dbo].[sgsnet_sps_testdrive_por_cliente]
GO

/****** Object:  StoredProcedure [dbo].[sgsnet_sps_testdrive_por_cliente]    Script Date: 15/01/2018 11:02:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sgsnet_sps_testdrive_por_cliente]
/*****************************************************************************
Objetivo		: Listar de TestDrive de un determinado Cliente.
Autor			: Hundred(Christian Rivera).
Fecha Creación	: (17/01/2018 11:53)
Historial		: El procedimiento almacenado lista los testdrive de un determinado Cliente.
****************************************************************************
@001 HUNDRED(Christian Rivera) 17/01/2018 Creacion
****************************************************************************/
(
    @numeroDocumento varchar(14)
)
AS
BEGIN

 ------------------------------------------------------------------------------
--Test Drive
------------------------------------------------------------------------------
select
    nid_solicitud as 'IdSolicitud',
    co_periodo as 'Periodo',
    cantidad as 'Cantidad',
    nu_solicitud as 'NumeroSolicitud',
    no_marca as 'NombreMarca',
    no_modelo as 'NombreModelo',
    co_familia as 'CodigoFamiliaCorto',
    nu_vin as 'VIN',
    no_ubicacion_fisica as 'NombreUbicacion',
    no_canal_venta as 'NombreCanalVenta',
    no_punto_venta_responsable as 'AsesorPuntoVenta',
    no_vendedor as 'NombreAsesor',
    --nid_usuario_vendedor,
    --nid_vendedor,
    nu_dni_cliente as 'NumeroDocumento',
    no_cliente as 'NombreCliente',
    --nu_celular_vendedor as 'Celular_Vendedor',
    co_estado as 'CodigoEstado',
    no_estado as 'NombreEstado',
    fe_prueba_manejo as 'FechaPruebaManejo',
    fe_inicio_prueba as 'FechaInicio',
    fe_fin_prueba as 'FechaFin',
    --fl_aprobacion,
    --no_aprobador,
    hr_inicio_prueba as 'HoraInicio',
    hr_fin_prueba as 'HoraFin',
    --nu_telefono_cliente as 'Telefono_Cliente',
    --nid_empresa_cliente,
    nu_ruc_cliente as 'RUCCliente',
    no_razon_social_cliente as 'RazonSocial'
    --fl_inactivo_dw,
    --fe_crea_dw,
    --co_usuario_crea_dw,
    --fe_cambio_dw,
    --co_usuario_cambio_dw,
    --no_usuario_red_dw,
    --no_estacion_red_dw 
from tbl_dw_test_drive
where fl_inactivo_dw='0' and (nu_dni_cliente = @numeroDocumento)
order by nid_solicitud desc
 
  END
GO

-- Testing SP
--exec [sgsnet_sps_testdrive_por_cliente] '40467417'
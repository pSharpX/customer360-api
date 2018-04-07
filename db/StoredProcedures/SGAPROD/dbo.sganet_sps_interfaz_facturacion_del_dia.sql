ALTER PROC [dbo].[sganet_sps_interfaz_facturacion_del_dia]
@fecha_facturacion  VARCHAR(10)
AS
BEGIN
SELECT 
       --Datos Cliente
       isnull(c_td.no_valor2, '') as cliente_tipo_documento,
       c_cli.nu_documento as cliente_numero_documento,
	   c_dir.nid_cliente_direccion as id_direccion_cliente_facturacion,
       --datos del asesor
       asesor.nu_tipo_documento as asesor_numero_documento,
       --datos del contacto
       isnull(co_td.no_valor2, '') as contacto_tipo_documento,
       isnull(co_cli.nu_documento, '') as contacto_numero_documento,
	   co_dir.nid_cliente_direccion as id_direccion_contacto,
	   np.fe_facturado as fecha_facturacion,
	   dbo.sganet_fn_Interfaz_Cliente360_ValidarClienteVehiculos(c_cli.nu_documento) as flag_venta_vehiculos,
	   dbo.sganet_fn_Interfaz_Cliente360_ValidarClienteRepuestos(c_cli.nu_documento) as flag_repuestos,
		0 as flag_servicios,
	   getdate() as fecha_registro
FROM  tbl_nota_pedido np
     INNER JOIN mae_marca ma ON ma.nid_marca = np.nid_marca
     INNER JOIN tbl_nota_pedido_movimiento mov ON np.nid_nota_pedido = mov.nid_nota_pedido AND mov.fl_inactivo = '0'
     INNER JOIN mae_punto_venta pv ON pv.nid_punto_venta = np.nid_punto_venta --
     --Datos Cliente/Propietario
     INNER JOIN mae_cliente_direccion c_dir ON c_dir.nid_cliente_direccion = ISNULL(np.nid_cliente, np.nid_propietario)
     INNER JOIN mae_cliente c_cli ON c_cli.nid_cliente = c_dir.nid_cliente
     LEFT JOIN  mae_tabla_detalle c_td ON c_cli.co_tipo_documento = c_td.no_valor2 AND c_td.nid_tabla_gen = 40
     LEFT JOIN  mae_ubigeo c_mu ON c_dir.coddpto = c_mu.coddpto    AND c_mu.codprov = '00'  AND c_mu.coddist = '00'
     LEFT JOIN  mae_ubigeo c_mu1 ON c_dir.coddpto = c_mu1.coddpto AND c_dir.codprov = c_mu1.codprov  AND c_mu1.coddist = '00'
     LEFT JOIN  mae_ubigeo c_mu2 ON c_dir.coddpto = c_mu2.coddpto AND c_dir.codprov = c_mu2.codprov  AND c_dir.coddist = c_mu2.coddist --
     --Datos del Asesor
     INNER JOIN usr Asesor ON Asesor.nid_usuario = np.nid_asesor_comercial --
     --Datos del Contacto
     LEFT JOIN mae_cliente_direccion co_dir ON co_dir.nid_cliente_direccion = np.nid_contacto
     LEFT JOIN mae_cliente co_cli ON co_cli.nid_cliente = co_dir.nid_cliente
     LEFT JOIN mae_tabla_detalle co_td ON co_cli.co_tipo_documento = co_td.no_valor2 AND co_td.nid_tabla_gen = 40
     LEFT JOIN mae_ubigeo co_mu ON co_dir.coddpto = co_mu.coddpto AND co_mu.codprov = '00'  AND co_mu.coddist = '00'
     LEFT JOIN mae_ubigeo co_mu1 ON co_dir.coddpto = co_mu1.coddpto AND co_dir.codprov = co_mu1.codprov  AND co_mu1.coddist = '00'
     LEFT JOIN mae_ubigeo co_mu2 ON co_dir.coddpto = co_mu2.coddpto AND co_dir.codprov = co_mu2.codprov AND co_dir.coddist = co_mu2.coddist 
	 WHERE np.fl_inactivo = '0' AND np.co_estado IN('0010', '0011', '0006')  AND np.co_canal_venta IN('CP', 'SU')
						  AND convert(date,np.fe_facturado) = @fecha_facturacion;
END

USE Cliente360_Dev;
GO

create PROC sgsnet_spu_cliente_facturacion_ultimos_datos
AS
BEGIN
	DECLARE  @fecha_facturacion  DATE
	SET @fecha_facturacion =  CONVERT (date, GETDATE());
 --Actualiza flag servicio,respuesto , vehiculo y ultima  direción de factura
  UPDATE c 
   SET c.nid_direccion_facturacion = i.id_direccion_cliente_facturacion,
  	c.fl_vservicios = i.flag_servicios,
    c.fl_venta_repuestos = i.flag_repuestos,
	c.fl_venta_vehiculo = i.flag_venta_vehiculos
   FROM cliente c 
   INNER JOIN persona p ON c.nid_cliente = p.nid_persona
   INNER JOIN interfaz_facturacion i ON i.cliente_numero_documento 
   collate SQL_Latin1_General_CP1_CI_AS  = p.nu_documento AND p.co_tipo_documento = i.cliente_tipo_documento
   AND rtrim(i.cliente_tipo_documento) collate SQL_Latin1_General_CP1_CI_AS =rtrim(p.co_tipo_documento)
			WHERE convert(date,i.fecha_facturacion) = @fecha_facturacion;

--Actualiza la tabla persona
  UPDATE p 
   SET p.nid_direccion_facturacion = i.id_direccion_cliente_facturacion
   FROM cliente c 
   INNER JOIN persona p ON c.nid_cliente = p.nid_persona
   INNER JOIN interfaz_facturacion i ON i.cliente_numero_documento 
   collate SQL_Latin1_General_CP1_CI_AS  = p.nu_documento AND p.co_tipo_documento = i.cliente_tipo_documento
   AND rtrim(i.cliente_tipo_documento) collate SQL_Latin1_General_CP1_CI_AS =rtrim(p.co_tipo_documento)
			WHERE convert(date,i.fecha_facturacion) = @fecha_facturacion;

--ACTUALIZA EL ULTIMO  ASESOR DE CLIENTE
UPDATE c
  SET c.nid_ultimo_asesor = sor.nid_persona
  FROM cliente c  
				INNER JOIN persona p ON c.nid_cliente = p.nid_persona --Clientes
				INNER JOIN empleado em ON em.nid_empleado = c.nid_ultimo_asesor --Asesor
				INNER JOIN persona ase on ase.nid_persona = em.nid_empleado --Asesor
			    INNER JOIN interfaz_facturacion i ON i.cliente_numero_documento = p.nu_documento AND i.cliente_tipo_documento= p.co_tipo_documento
				INNER JOIN  ( ---Obtiene el  DNI  del ultimo asesor cliente
				       SELECT DISTINCT 
					   i.cliente_numero_documento as nu_documento,
					   asesor_numero_documento as asesor_documento
					   FROM interfaz_facturacion i INNER JOIN persona p on p.nu_documento = i.cliente_numero_documento
				) inte ON inte.nu_documento = p.nu_documento
				INNER JOIN ( --Obtenemos el ultimo asesor de cliente
					SELECT DISTINCT p.nid_persona,p.nu_documento,p.no_persona FROM interfaz_facturacion i 
					INNER JOIN persona p ON p.nu_documento = i.asesor_numero_documento
				) sor ON sor.nu_documento = inte.asesor_documento 
					WHERE convert(date,i.fecha_facturacion) = @fecha_facturacion;


UPDATE c
    SET c.nid_contacto_principal = ult.nid_persona
 	FROM cliente c 
				INNER JOIN persona p ON p.nid_persona = c.nid_cliente
				INNER JOIN cliente_contacto cc ON cc.nid_cliente =  c.nid_cliente 
			    INNER JOIN interfaz_facturacion i ON i.cliente_numero_documento = p.nu_documento AND p.co_tipo_documento = i.cliente_tipo_documento
				INNER JOIN contacto cn ON cn.nid_contacto = cc.nid_contacto
				INNER JOIN persona nc ON nc.nid_persona = cn.nid_contacto AND c.nid_contacto_principal = nc.nid_persona
				INNER JOIN (
					SELECT i.cliente_numero_documento as nu_documento,
					      i.contacto_tipo_documento as contact_tipo_doc,
						  i.contacto_numero_documento as nu_doc_contacto 
						FROM cliente c INNER JOIN persona p ON p.nid_persona = c.nid_cliente
						INNER JOIN interfaz_facturacion i ON i.cliente_numero_documento = p.nu_documento
				) ase ON ase.nu_documento = p.nu_documento
				INNER JOIN persona ult ON ult.nu_documento = ase.nu_doc_contacto AND ult.co_tipo_documento = ase.contact_tipo_doc
					WHERE convert(date,i.fecha_facturacion) = @fecha_facturacion;
 
END

USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sgsnet_sps_cliente_por_codigo]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[sgsnet_sps_cliente_por_codigo]
 @clienteId INT
AS
BEGIN
SELECT 
	   c.nid_cliente as IdCliente,
	   CONCAT(p.no_persona, ' ' , p.no_apellido_paterno ,' ',p.no_apellido_materno) as NombreCompleto,
	   LTRIM(p.no_persona) as Nombre,
	   p.no_apellido_paterno as ApellidoPaterno,
	   p.no_apellido_materno as ApellidoMaterno,
	   p.no_razon_social as RazonSocial,
	   p.fe_nacimiento as  FechaNacimiento,
	   p.no_correo as Correo,
	   RTRIM(p.nu_documento) as NumeroDocumento,
	   RTRIM(p.nu_documento) as Ruc,
	   p.nu_celular as Celular,
	   p.nu_telefono as Telefono,

	   es.no_valor2 as CodigoEstadoCivil,
	   es.no_valor1 as EstadoCivil,
	   gn.no_valor2 as CodigoGenero,
	   gn.no_valor1 as Genero,
	   pd.nid_direccion as IdDireccion,
	   pd.no_direccion as Direccion,
	   tp.no_valor2 as TipoPersona,
	   RTRIM(p.co_tipo_documento) as TipoDocumento,
	   tp.no_valor1 as TipoPersonaNombre,

	   RTRIM(pd.co_postal) as CodigoPostal,
	   ds.Distrito as CodigoDistrito,
	   RTRIM(ds.Nombre) as Distrito,
	   pv.Provincia as CodigoProvincia,
	   RTRIM(pv.Nombre) as Provincia,
	   dp.Codigo as  CodigoDepartamento,
	   RTRIM(dp.Nombre) as Departamento,
	   ps.nid_pais as IdPais,
	   RTRIM(ps.no_pais) as Pais,
	   RTRIM(ps.co_pais_spring) as CodigoPais,
	   CONCAT(ase.no_persona, ' ' , ase.no_apellido_paterno ,' ',ase.no_apellido_materno) as Asesor,
	   cont.nid_persona as IdContacto,
	   cont.co_sexo as SexoContacto,
	   gc.no_valor1 as SexoContactoNombre,
	   RTRIM(cont.nu_documento) as ContactoDocumento,
	   RTRIM(cont.no_persona) as NombreContacto,
	   RTRIM(cont.no_apellido_paterno) as ApellidoPaternoContacto,
	   RTRIM(cont.no_apellido_materno) as ApellidoMaternoContacto,
	   RTRIM(cont.co_tipo_documento) as TipoDocumentoContacto,
	   cont.nu_telefono as TelefonoContacto,
	   cont.nu_celular as CelularContacto,
	   cont.no_correo as CorreoContacto
		 FROM cliente c   
						  INNER JOIN  persona p ON p.nid_persona = c.nid_cliente 
						  LEFT JOIN   cliente_contacto cn ON cn.nid_cliente = c.nid_cliente
						  LEFT JOIN   contacto ct ON ct.nid_contacto = cn.nid_contacto
						  INNER JOIN  persona cont on cont.nid_persona = ct.nid_contacto AND c.nid_contacto_principal = cn.nid_contacto --Contacto
						  LEFT JOIN   persona_direccion pd on pd.nid_persona = c.nid_cliente  AND c.nid_direccion_facturacion = pd.nid_direccion  --Direccion
						  LEFT JOIN   mae_pais ps on ps.nid_pais = pd.nid_pais
						  LEFT JOIN   empleado emp ON emp.nid_empleado = c.nid_ultimo_asesor --asesor
						  LEFT JOIN   persona ase ON ase.nid_persona = emp.nid_empleado --asesor
						  LEFT JOIN   maestro.tabla_detalle es on  es.no_valor2 = p.co_estado_civil AND es.nid_tabla_gen =  137 --estado civil
						  LEFT JOIN   maestro.tabla_detalle tp on  tp.no_valor2 = p.co_tipo_persona AND tp.nid_tabla_gen =  54	 --maestro detalle
						  LEFT JOIN   maestro.tabla_detalle gn on  gn.no_valor2 = p.co_sexo AND gn.nid_tabla_gen =  94 --genero
						  LEFT JOIN   maestro.tabla_detalle gc on  gc.no_valor2 = cont.co_sexo and gc.nid_tabla_gen = 94
						  LEFT JOIN   vw_distrito ds on  ds.Departamento = pd.coddpto  AND ds.Provincia = pd.codprov AND ds.Distrito = pd.coddist  --distrito
						  LEFT JOIN   vw_provincia pv on pv.Departamento = pd.coddpto AND pv.Provincia = pd.codprov
					      LEFT JOIN   vw_departamento dp on dp.Codigo = pd.coddpto
			WHERE c.nid_cliente = @clienteId
END

GO

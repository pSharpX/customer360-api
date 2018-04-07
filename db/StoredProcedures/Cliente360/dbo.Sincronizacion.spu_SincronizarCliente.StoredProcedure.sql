
/****** Object:  StoredProcedure [Sincronizacion].[spu_SincronizarCliente]    Script Date: 20/02/2018 09:14:05 ******/


IF EXISTS(SELECT * FROM sys.procedures  WHERE Name = 'spu_SincronizarCliente')
BEGIN
    DROP PROCEDURE [Sincronizacion].[spu_SincronizarCliente]
END
GO

/****** Object:  StoredProcedure [Sincronizacion].[spu_SincronizarCliente]    Script Date: 20/02/2018 09:14:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Sincronizacion].[spu_SincronizarCliente]
/*****************************************************************************
Objetivo		: Sincronizar clientes en BD Cliente360
Autor			: Código de usuario que crea elprocedimiento almacenado.
Fecha Creación	: Fecha de creación del procedure. (dd/mm/yyyy)
Historial		: El listado de abajo indica las modificaciones que se pueden haber realizado sobre el procedure.
****************************************************************************
@001 HND(ABL) 14/12/2017 Creación
@002 HND(CRT) 19/02/2017 Actualización
****************************************************************************/
(
	@no_persona varchar(80),
	@no_apellido_paterno varchar(60),
	@no_apellido_materno varchar(60),
	@no_razon_social varchar(260),
	@fe_nacimiento datetime,
	@co_tipo_persona char(4),
	@co_tipo_documento char(4),
	@nu_documento char(20),
	@no_correo varchar(260),
	@nu_telefono varchar(20),
	@nu_celular varchar(20),
	@co_sexo char(4),
	@co_estado_civil char(10),

	@nid_cliente_direccion INT = NULL,
	@no_direccion VARCHAR(255),
	@nu_fax VARCHAR(20),
	@coddist CHAR(2),
	@codprov CHAR(2),
	@coddpto CHAR(2),
	@nid_pais INT,
	@no_correo_contacto VARCHAR(255),  
	@nu_telefono_contacto VARCHAR(20),
	@nu_celular_contacto VARCHAR(20),
	@co_sexo_contacto CHAR(4),
	@co_tipo_documento_contacto CHAR(4),
	@nu_documento_contacto VARCHAR(20),

	@co_usuario_crea char(20),
	@fe_crea datetime,
	@co_usuario_cambio char(20),
	@fe_cambio datetime,
	@no_estacion_red char(20),
	@no_usuario_red char(20),
	@fl_inactivo bit,
    @direccionIdOut INT OUTPUT
)
as
begin try
	--Declaración de Variables
	declare @vl_nu_error int;
	declare @vl_fl_transaccion char(1);
	declare @idDireccion INT;
	

	--Inicialización de Variables
	set @vl_fl_transaccion = '0';

	SET @co_usuario_crea = 'admin';
	SET @co_usuario_cambio = 'admin';
	SET @no_estacion_red = 'admin';
	SET @no_usuario_red = 'admin';

	--Transacción
	begin transaction

	set @vl_fl_transaccion = '1';

	DECLARE @Output TABLE(IdPersona INT)
    DECLARE @OutputDirecion TABLE(idDireccion INT)
	DECLARE @IdPersona INT

	SELECT @idDireccion = (MAX(ISNULL(nid_direccion,0)) + 1) FROM persona_direccion

	IF NOT EXISTS(SELECT nid_persona
		FROM persona WHERE co_tipo_documento= @co_tipo_documento AND nu_documento= @nu_documento)
	BEGIN
		--Insertar Persona
		INSERT INTO persona(no_persona,no_apellido_paterno,no_apellido_materno,no_razon_social,fe_nacimiento,co_tipo_persona,co_tipo_documento,nu_documento,no_correo,nu_telefono,nu_celular,co_sexo,co_estado_civil,
			co_usuario_crea,fe_crea,co_usuario_cambio,fe_cambio,no_estacion_red,no_usuario_red,fl_inactivo)
		OUTPUT inserted.nid_persona INTO @Output(IdPersona)
		VALUES(@no_persona,@no_apellido_paterno,@no_apellido_materno,@no_razon_social,@fe_nacimiento,@co_tipo_persona,@co_tipo_documento,@nu_documento,@no_correo,@nu_telefono,@nu_celular,@co_sexo,@co_estado_civil,
			@co_usuario_crea,@fe_crea,@co_usuario_cambio,@fe_cambio,@no_estacion_red,@no_usuario_red,@fl_inactivo)
		
		SELECT @IdPersona =  IdPersona FROM @Output		
		INSERT INTO cliente(nid_cliente, co_tipo_ultimo_contacto,fe_crea, co_usuario_crea, fe_cambio, co_usuario_cambio,no_usuario_red, no_estacion_red, fl_inactivo)
		VALUES(@IdPersona, 0, @fe_crea, @co_usuario_crea, @fe_cambio, @co_usuario_cambio, @no_usuario_red, @no_estacion_red, @fl_inactivo)

		IF (@nid_cliente_direccion <> -1)
		BEGIN
		--Insertar direccion
			INSERT INTO persona_direccion (nid_direccion,no_direccion,nu_telefono,nu_fax,co_postal,fe_crea,co_usuario_crea,fe_cambio,co_usuario_cambio,
					no_estacion_red,no_usuario_red,fl_Inactivo,nid_persona,coddpto,codprov,coddist,nid_pais)
					OUTPUT inserted.nid_direccion INTO @OutputDirecion(idDireccion)
				VALUES(@idDireccion,@no_direccion,@nu_telefono,@nu_fax,NULL, @fe_crea,@co_usuario_crea,@fe_cambio,@co_usuario_cambio,@no_estacion_red,
					@no_usuario_red,0,@IdPersona,@coddpto,@codprov,@coddist,@nid_pais)

         SELECT @direccionIdOut = (idDireccion) FROM @OutputDirecion
		END
	END
	ELSE
	BEGIN

		SET @IdPersona = (SELECT TOP 1 nid_persona  FROM persona WHERE co_tipo_documento= @co_tipo_documento AND nu_documento= @nu_documento)
		--Actualizar Persona
		UPDATE persona
		SET no_persona= @no_persona,
			no_apellido_paterno= @no_apellido_paterno,
			no_apellido_materno=@no_apellido_materno,
			no_razon_social= @no_razon_social,
			fe_nacimiento=@fe_nacimiento,
			co_tipo_persona=@co_tipo_persona,
			no_correo=@no_correo,
			nu_telefono=@nu_telefono,
			nu_celular=@nu_celular,
			co_sexo=@co_sexo,
			co_estado_civil=@co_estado_civil,
			co_usuario_cambio=@co_usuario_cambio,
			fe_cambio=@fe_cambio,
			no_estacion_red=@no_estacion_red,
			no_usuario_red=@no_usuario_red,
			fl_inactivo=@fl_inactivo
		WHERE co_tipo_documento= @co_tipo_documento AND nu_documento= @nu_documento

		--Actualizar dirección
		
		IF NOT EXISTS(SELECT nid_persona
			FROM persona_direccion WHERE nid_direccion= @nid_cliente_direccion) and @nid_cliente_direccion <> -1
		BEGIN
			
			INSERT INTO persona_direccion (nid_direccion,no_direccion,nu_telefono,nu_fax,co_postal,fe_crea,co_usuario_crea,fe_cambio,co_usuario_cambio,
				no_estacion_red,no_usuario_red,fl_Inactivo,nid_persona,coddpto,codprov,coddist,nid_pais)
				OUTPUT inserted.nid_direccion INTO @OutputDirecion(idDireccion)
			VALUES(@idDireccion,@no_direccion,@nu_telefono,@nu_fax,NULL, @fe_crea,@co_usuario_crea,@fe_cambio,@co_usuario_cambio,@no_estacion_red,
				@no_usuario_red,0,@IdPersona,@coddpto,@codprov,@coddist,@nid_pais)

			 SELECT @direccionIdOut = (idDireccion) FROM @OutputDirecion
		END
		ELSE IF EXISTS(SELECT nid_persona
			FROM persona_direccion WHERE nid_direccion= @nid_cliente_direccion)		
		BEGIN
			UPDATE persona_direccion
			SET 
				no_direccion=@no_direccion,
				nu_telefono=@nu_fax,
				fe_cambio=@fe_cambio,
				co_usuario_cambio=@co_usuario_cambio,
				no_estacion_red=@no_estacion_red,
				no_usuario_red=@no_usuario_red,
				coddpto=@coddpto,
				codprov=@codprov,
				coddist=@coddist,
				nid_pais=@nid_pais
			WHERE nid_direccion=@nid_cliente_direccion
		END

		--Actualizar Contacto
		IF (SELECT COUNT(*) FROM persona WHERE co_tipo_documento = @co_tipo_documento_contacto AND nu_documento = @nu_documento_contacto) > 0
		BEGIN

			UPDATE persona SET
				no_correo = @no_correo_contacto,
				nu_telefono = @nu_telefono_contacto,
				nu_celular = @nu_celular_contacto,
				co_sexo = @co_sexo_contacto,
				fe_cambio = @fe_cambio,
				co_usuario_cambio = @co_usuario_cambio
			WHERE co_tipo_documento = @co_tipo_documento_contacto AND nu_documento = @nu_documento_contacto

		END
	END


	commit transaction
	--RETURN  
	set @vl_fl_transaccion = '0';

end try
begin catch
	select ERROR_MESSAGE(), ERROR_NUMBER()
	---- 2627 ERROR DE UNIQUE
	---- 515 ERROR DE INSERTAR VALOR NULL NO PERMITIDO
	---- 547 ERROR CONFLICTO DE LLAVE FORANEA
	--set @vl_nu_error = ERROR_NUMBER()
	--if @vl_nu_error = 2627 set @vo_id_usuario = -2
	--else if @vl_nu_error = 515 set @vo_id_usuario = -3
	--else if @vl_nu_error = 547 set @vo_id_usuario = -4
	--else set @vo_id_usuario = -1

	if (@vl_fl_transaccion = '1') ROLLBACK TRANSACTION
end catch




go
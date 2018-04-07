/****** Object:  StoredProcedure [Sincronizacion].[sganet_spu_sincronizarclientes]    Script Date: 20/02/2018 10:36:29 ******/
DROP PROCEDURE [Sincronizacion].[sganet_spu_sincronizarclientes]
GO

/****** Object:  StoredProcedure [Sincronizacion].[sganet_spu_sincronizarclientes]    Script Date: 20/02/2018 10:36:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [Sincronizacion].[sganet_spu_sincronizarclientes]
/***************************************************************************  
Objetivo  : Sincronizar clientes 
Autor   : DCH               
Fecha Creación : 21/12/2017
Autor Modifica :               
Fecha Modifica :               
Notas   : 
****************************************************************************/    
  @no_cliente VARCHAR(50),
  @no_ape_pat VARCHAR(50),
  @no_ape_mat VARCHAR(50),
  @no_razon_social VARCHAR(255),
  @fe_nacimiento DATETIME,
  @co_tipo_documento CHAR(4),
  @nu_documento VARCHAR(20),
  @nu_documento_original VARCHAR(20),
  @nu_telefono VARCHAR(20),
  @nu_celular VARCHAR(20),
  @no_correo VARCHAR(255),
  @co_sexo CHAR(4),
  @co_estado_civil CHAR(2), 
  @fl_inactivo CHAR(1),
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
  @nid_cliente_direccion INT = NULL,
  @fe_crea DATETIME,
  @co_usuario_crea VARCHAR(20),
  @fe_cambio DATETIME,
  @co_usuario_cambio VARCHAR(20),
  @no_usuario_red VARCHAR(20),
  @no_estacion_red VARCHAR(20)
AS  
    BEGIN TRY  

	DECLARE @Output TABLE(idCliente INT)
	DECLARE @idCliente INT

	DECLARE @co_tipo_cliente CHAR(4) = '0001'
		
	IF (SELECT COUNT(*) FROM mae_cliente WHERE co_tipo_documento = @co_tipo_documento AND nu_documento = @nu_documento) = 0
	BEGIN 
		
			INSERT INTO mae_cliente(no_cliente,no_ape_pat,no_ape_mat,no_razon_social,fe_nacimiento,
			co_tipo_documento,nu_documento,nu_telefono,nu_celular,co_sexo,co_estado_civil,fl_inactivo,
			fe_crea,co_usuario_crea,no_usuario_red,no_estacion_red,co_tipo_cliente,no_correo)
			OUTPUT inserted.nid_cliente INTO @Output(idCliente)
			VALUES(@no_cliente,@no_ape_pat,@no_ape_mat,@no_razon_social,@fe_nacimiento,
			@co_tipo_documento,@nu_documento,@nu_telefono,@nu_celular,@co_sexo,@co_estado_civil,@fl_inactivo,
			@fe_crea,ISNULL(@co_usuario_crea,''),@no_usuario_red,@no_estacion_red,@co_tipo_cliente,@no_correo)		

			SELECT @idCliente = (idCliente) FROM @Output

			IF @nid_cliente_direccion IS NOT NULL AND @nid_cliente_direccion <> -1
			BEGIN
				INSERT INTO mae_cliente_direccion(nid_cliente,no_direccion,nu_fax,coddpto,codprov,coddist,nid_pais,
				fe_crea,co_usuario_crea,no_usuario_red,no_estacion_red,no_correo,nu_telefono)
				VALUES(@idCliente,@no_direccion,@nu_fax,@coddpto,@codprov,@coddist,@nid_pais,
				@fe_crea,@co_usuario_crea,@no_usuario_red,@no_estacion_red,@no_correo,@nu_telefono)
			END

	END
	ELSE 
	BEGIN

		UPDATE mae_cliente SET
			no_cliente = @no_cliente,
			no_ape_pat = @no_ape_pat,
			no_ape_mat = @no_ape_mat,
			no_razon_social = @no_razon_social,
			fe_nacimiento = @fe_nacimiento,			
			nu_telefono = @nu_telefono,
			nu_celular = @nu_celular,
			co_sexo = @co_sexo,
			co_estado_civil = @co_estado_civil,
			fl_inactivo = @fl_inactivo,
			fe_cambio = @fe_cambio,
			co_usuario_cambio = @co_usuario_cambio,
			co_tipo_cliente = @co_tipo_cliente,
			no_correo = @no_correo
		WHERE co_tipo_documento = @co_tipo_documento AND
			  nu_documento = @nu_documento

		SELECT  @idCliente = (nid_cliente) FROM mae_cliente WHERE co_tipo_documento = @co_tipo_documento AND nu_documento = @nu_documento		

		IF NOT EXISTS(SELECT nid_cliente
			FROM mae_cliente_direccion WHERE nid_cliente_direccion = @nid_cliente_direccion) and @nid_cliente_direccion <> -1
		BEGIN

			INSERT INTO mae_cliente_direccion(nid_cliente,no_direccion,nu_fax,coddpto,codprov,coddist,nid_pais,
			fe_crea,co_usuario_crea,no_usuario_red,no_estacion_red,no_correo,nu_telefono)
			VALUES(@idCliente,@no_direccion,@nu_fax,@coddpto,@codprov,@coddist,@nid_pais,
			@fe_crea,@co_usuario_crea,@no_usuario_red,@no_estacion_red,@no_correo,@nu_telefono)

		END
		ELSE IF EXISTS(SELECT nid_cliente
			FROM mae_cliente_direccion WHERE nid_cliente_direccion = @nid_cliente_direccion)
		BEGIN

			UPDATE mae_cliente_direccion SET
				no_direccion = @no_direccion,
				nu_fax = @nu_fax,
				coddpto = @coddpto,
				codprov = @codprov,
				coddist = @coddist,
				nid_pais = @nid_pais,
				fe_cambio = @fe_cambio,
				co_usuario_cambio = @co_usuario_cambio,
				no_correo = @no_correo,
				nu_telefono = @nu_telefono
			WHERE nid_cliente_direccion = @nid_cliente_direccion 

		END

	END
	
	IF (SELECT COUNT(*) FROM mae_cliente WHERE co_tipo_documento = @co_tipo_documento_contacto AND nu_documento = @nu_documento_contacto) > 0
	BEGIN

		UPDATE mae_cliente SET
			no_correo = @no_correo_contacto,
			nu_telefono = @nu_telefono_contacto,
			nu_celular = @nu_celular_contacto,
			co_sexo = @co_sexo_contacto,
			fe_cambio = @fe_cambio,
			co_usuario_cambio = @co_usuario_cambio
		WHERE co_tipo_documento = @co_tipo_documento_contacto AND nu_documento = @nu_documento_contacto

	END

    END TRY  
    BEGIN CATCH  
        SELECT ERROR_NUMBER() AS ERROR;  
    END CATCH;

GO



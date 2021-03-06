USE [Cliente360_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sgsnet_spu_cliente_actualizar]    Script Date: 4/01/2018 18:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sgsnet_spu_cliente_actualizar]
(
@nid_persona INT,
@no_persona varchar(80),
@no_apellido_paterno varchar(60),
@no_apellido_materno varchar(60),
@no_razon_social varchar(260),
@fe_nacimiento datetime,
@nu_documento char(20),
@no_correo varchar(260),
@nu_telefono varchar(20),
@nu_celular varchar(20),
@co_sexo char(4),
@co_estado_civil char(10),

--contacto
@idContacto int,
@nombreContacto varchar(80),
@apellidoPaternoContacto varchar(60),
@apellidoMaternoContacto varchar(60),
@sexoContacto char(4) = NULL,
@nuDocumentoContacto char(20),
@noCorreoContacto varchar(260),
@nuTelefonoContacto varchar(20) = null,
@nuCelularContacto varchar(20) = null,


--Direccion
@nid_direccion int,
@no_direccion varchar(260),
@co_postal char(12),
@coddpto char(2),
@codprov char(2),
@coddist char(2),
@nid_pais int,

@co_usuario_crea char(20),
@fe_crea datetime,
@co_usuario_cambio char(20),
@fe_cambio datetime,
@no_estacion_red char(20),
@no_usuario_red char(20),
@fl_inactivo bit
)
AS
BEGIN TRY
	--DECLARACIÓN DE VARIABLES
	DECLARE @VL_NU_ERROR INT;

--UPDATE CLIENTE
SET NOCOUNT ON;  
UPDATE      [dbo].[persona] SET
            [nid_persona] = @nid_persona
           ,[no_persona] = @no_persona
           ,[no_apellido_paterno] =  @no_apellido_paterno
           ,[no_apellido_materno] =   @no_apellido_materno
           ,[no_razon_social] =  @no_razon_social
           ,[fe_nacimiento] =   @fe_nacimiento
           ,[nu_documento] = @nu_documento
           ,[no_correo] = @no_correo
           ,[nu_telefono] = @nu_telefono
           ,[nu_celular] = @nu_celular
           ,[co_sexo] = @co_sexo
           ,[co_estado_civil] = @co_estado_civil
           ,[co_usuario_crea] = @co_usuario_crea
           ,[fe_crea] = @fe_crea
           ,[co_usuario_cambio] = @co_usuario_cambio
           ,[fe_cambio] = @fe_cambio
           ,[no_estacion_red]=  @no_estacion_red
           ,[no_usuario_red] = @no_usuario_red
           ,[fl_inactivo] = @fl_inactivo 
		   WHERE nid_persona = @nid_persona

--UPDATE DIRECCION CLIENTE
SET NOCOUNT ON;  
UPDATE dbo.persona_direccion 
	  SET no_direccion = @no_direccion
      ,co_postal = @co_postal
      ,coddpto = @coddpto
      ,codprov = @codprov
      ,coddist = @coddist
	  ,nid_pais = @nid_pais
 WHERE nid_direccion = @nid_direccion AND nid_persona = @nid_persona
 
 --UPDATE CONTACTO CLIENTE
 SET NOCOUNT ON;

 UPDATE     [dbo].[persona] SET
            [no_persona] = @NombreContacto
           ,[no_apellido_paterno] =  @apellidoPaternoContacto
           ,[no_apellido_materno] =   @apellidoMaternoContacto
		   ,[co_sexo] = @sexoContacto
           ,[nu_documento] = @nuDocumentoContacto
           ,[no_correo] = @noCorreoContacto
           ,[nu_telefono] = @nuTelefonoContacto,
            [nu_celular] = @nuCelularContacto
		   WHERE nid_persona = @IdContacto
END TRY
BEGIN CATCH
	SET @VL_NU_ERROR = ERROR_NUMBER()
END CATCH
GO

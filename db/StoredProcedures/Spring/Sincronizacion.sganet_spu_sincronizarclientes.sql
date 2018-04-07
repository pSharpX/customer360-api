IF EXISTS(SELECT * FROM sys.procedures  WHERE Name = 'sganet_spu_sincronizarclientes')
BEGIN
    DROP PROCEDURE [Sincronizacion].[sganet_spu_sincronizarclientes]
END
GO

/****** Object:  StoredProcedure [Sincronizacion].[sganet_spu_sincronizarclientes]    Script Date: 20/02/2018 09:14:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [Sincronizacion].[sganet_spu_sincronizarclientes]
/***************************************************************************  
Objetivo  : Sincronizar clientes 
Autor   : DCH               
Fecha Creación : 22/12/2017
Autor Modifica :               
Fecha Modifica :               
Notas   : 
****************************************************************************/    	
	@Nombres VARCHAR(50),
	@ApellidoPaterno VARCHAR(50),
	@ApellidoMaterno VARCHAR(50),
	@Busqueda CHAR(150),
	@FechaNacimiento DATETIME,
	@TipoDocumento CHAR(1),
	@Documento CHAR(20),
	@DocumentoOriginal CHAR(20),
	@CorreoElectronico VARCHAR(50),
	@Sexo CHAR(1),
	@EstadoCivil CHAR(1),
	@UltimoUsuario CHAR(20),
	@Origen CHAR(4),
	@EsCliente CHAR(1),
	@EsProveedor CHAR(1),
	@EsEmpleado CHAR(1),
	@EsOtro CHAR(1),	
	@Direccion CHAR(60),
	@Fax VARCHAR(15),
	@CodigoPostal CHAR(3),
	@Departamento CHAR(3),
	@Provincia CHAR(30),
	@UBIGEO CHAR(6),
	@Pais CHAR(30),
	@Telefono VARCHAR(15),
	@Celular VARCHAR(15),
	@Estado CHAR(1),
	@CorreoElectronicoContacto VARCHAR(50),
	@TelefonoContacto VARCHAR(15),
	@CelularContacto VARCHAR(15),
	@SexoContacto CHAR(1),
	@TipoDocumentoContacto CHAR(1),
	@DocumentoContacto CHAR(20),
	@PersonaDireccion CHAR(10),
	@UltimaFechaModif VARCHAR(MAX),
	@NombreCompleto VARCHAR(150)
AS  
    BEGIN TRY  
	
	DECLARE @Persona INT ,@Secuencia INT
	
	IF (SELECT COUNT(*) FROM PersonaMast WHERE TipoDocumento = @TipoDocumento AND Documento = @Documento) = 0
	BEGIN

		SELECT @Persona = (MAX(ISNULL(Persona,0)) + 1) FROM PersonaMast
		SET @Secuencia = 1
		
		INSERT INTO PersonaMast(Persona,Nombres,ApellidoPaterno,ApellidoMaterno,Busqueda,FechaNacimiento,TipoDocumento,Documento,
		CorreoElectronico,Sexo,EstadoCivil,UltimoUsuario,Origen,EsCliente,EsProveedor,EsEmpleado,EsOtro,NombreCompleto,
		Telefono,Celular)
		VALUES(@Persona,@Nombres,@ApellidoPaterno,@ApellidoMaterno,@Busqueda,@FechaNacimiento,@TipoDocumento,@Documento,
		@CorreoElectronico,@Sexo,@EstadoCivil,@UltimoUsuario,ISNULL(@Origen,'LIMA'),@EsCliente,@EsProveedor,@EsEmpleado,@EsOtro,@NombreCompleto,
		@Telefono,@Celular)

		IF @PersonaDireccion IS NOT NULL AND RTRIM(@PersonaDireccion) <> '-1'
		BEGIN
			INSERT INTO Direccion(Persona,Secuencia,Direccion,Fax,CodigoPostal,Departamento,Provincia,UBIGEO,Pais,Telefono,Estado,UltimoUsuario)
			VALUES(@Persona,@Secuencia,@Direccion,@Fax,@CodigoPostal,@Departamento,@Provincia,@UBIGEO,@Pais,@Telefono,@Estado,@UltimoUsuario)

		END
	END
	ELSE
	BEGIN
		
		UPDATE PersonaMast SET 
			Nombres = @Nombres,
			ApellidoPaterno = @ApellidoPaterno,
			ApellidoMaterno = @ApellidoMaterno,
			Busqueda = @Busqueda,
			FechaNacimiento = @FechaNacimiento,
			CorreoElectronico = @CorreoElectronico,
			Sexo = @Sexo,
			EstadoCivil = @EstadoCivil,
			UltimoUsuario = @UltimoUsuario,
			Origen = ISNULL(@Origen,'LIMA'),
			EsCliente = @EsCliente,
			EsProveedor = @EsProveedor,
			EsEmpleado = @EsEmpleado,
			EsOtro = @EsOtro,
			UltimaFechaModif = @UltimaFechaModif,
			NombreCompleto = @NombreCompleto,
			Telefono = @Telefono,
			Celular = @Celular
		WHERE TipoDocumento = @TipoDocumento AND Documento = @Documento

		
		IF @PersonaDireccion IS NOT NULL AND RTRIM(@PersonaDireccion) <> '-1'
		BEGIN
			SELECT @Persona = (SUBSTRING(@PersonaDireccion,0,CHARINDEX('-',@PersonaDireccion,0)))
			SELECT @Secuencia = (SUBSTRING(@PersonaDireccion,CHARINDEX('-',@PersonaDireccion,0)+1,LEN(@PersonaDireccion)))

			IF (SELECT COUNT(*) FROM Direccion WHERE Persona = @Persona AND Secuencia = @Secuencia) = 0
			BEGIN

				INSERT INTO Direccion(Persona,Secuencia,Direccion,Fax,CodigoPostal,Departamento,Provincia,UBIGEO,Pais,Telefono,Estado,UltimoUsuario)
				VALUES(@Persona,@Secuencia,@Direccion,@Fax,@CodigoPostal,@Departamento,@Provincia,@UBIGEO,@Pais,@Telefono,@Estado,@UltimoUsuario)
					
			
			END
			ELSE
			BEGIN

				UPDATE Direccion SET
					Direccion = @Direccion,
					Fax = @Fax,
					CodigoPostal = @CodigoPostal,
					Departamento = @Departamento,
					Provincia = @Provincia,
					UBIGEO = @UBIGEO,
					Pais = @Pais,
					Telefono = @Telefono,
					Estado = @Estado,
					UltimoUsuario = @UltimoUsuario,
					UltimaFechaModif = @UltimaFechaModif
				WHERE Persona = @Persona AND Secuencia = @Secuencia

			END
		END
	END

	IF (SELECT COUNT(*) FROM PersonaMast WHERE TipoDocumento = @TipoDocumentoContacto AND Documento = @DocumentoContacto) > 0
	BEGIN

		UPDATE PersonaMast SET
			CorreoElectronico = @CorreoElectronicoContacto,
			Telefono = @TelefonoContacto,
			Celular = @CelularContacto,
			Sexo = @SexoContacto,
			UltimoUsuario = @UltimoUsuario,
			UltimaFechaModif = @UltimaFechaModif
		WHERE TipoDocumento = @TipoDocumentoContacto AND Documento = @DocumentoContacto

	END

	END TRY  
    BEGIN CATCH  
        SELECT   
        ERROR_NUMBER() AS ErrorNumber  
       ,ERROR_MESSAGE() AS ErrorMessage;   
    END CATCH;

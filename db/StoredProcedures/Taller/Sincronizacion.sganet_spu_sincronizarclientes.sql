
IF EXISTS(SELECT * FROM sys.procedures  WHERE Name = 'sganet_spu_sincronizarclientes')
BEGIN
    DROP PROCEDURE [Sincronizacion].[sganet_spu_sincronizarclientes]
END
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
  @no_correo VARCHAR(225),
  @co_sexo CHAR(4),
  @co_estado_civil CHAR(2), 
  @fl_inactivo CHAR(1),
  @no_direccion VARCHAR(255),
  @nu_fax VARCHAR(20),
  @coddist CHAR(2),
  @codprov CHAR(2),
  @coddpto CHAR(2),
  @nid_pais INT
AS  
    BEGIN TRY  

	DECLARE @Id_Tipo_Cliente CHAR(2) = '01'

	IF((@co_tipo_documento)='R')
	BEGIN
		SET @no_razon_social= @no_razon_social --@no_ape_pat + ' ' + @no_ape_mat + ' ' + @no_razon_social
	END
	
	IF((@co_tipo_documento)='D')
	BEGIN
		SET @no_razon_social= @no_cliente
	END
	
	IF (SELECT COUNT(*) FROM Glbl_Cliente_Proveedor WHERE Tipo_Docto_Identidad = @co_tipo_documento AND Rut = @nu_documento) = 0
	BEGIN 

		INSERT INTO Glbl_Cliente_Proveedor(ApellidoPaterno, ApellidoMaterno,Razon_Social,Fecha_Nacimiento,
			Tipo_Docto_Identidad, Rut, E_Mail,Telefono, Celular_Cliente, Sexo, Direccion, Fax, ID_REGION, Id_Ciudad, Id_Comuna, Id_Pais,
			Id_Tipo_Cliente,Id_Cliente_Proveedor)
		VALUES(@no_ape_pat,@no_ape_mat,@no_razon_social,@fe_nacimiento,
			@co_tipo_documento,@nu_documento, @no_correo,@nu_telefono,@nu_celular,@co_sexo, @no_direccion, @nu_fax, @coddpto, @codprov, @coddist,@nid_pais,
			@Id_Tipo_Cliente,@nu_documento)
			
	END
	ELSE 
	BEGIN

		UPDATE Glbl_Cliente_Proveedor SET
			ApellidoPaterno = ISNULL(@no_ape_pat, ''), 
			ApellidoMaterno = ISNULL(@no_ape_mat, ''),
			Razon_Social = ISNULL(@no_razon_social, ''),
			Fecha_Nacimiento = @fe_nacimiento,
			Rut = ISNULL(@nu_documento, '')	,
			E_Mail = ISNULL(@no_correo, ''),
			Telefono = ISNULL(@nu_telefono, ''), 
			Celular_Cliente = ISNULL(@nu_celular, ''),
			Sexo = ISNULL(@co_sexo, ''), 
			Direccion = ISNULL(@no_direccion, ''), 
			Fax = ISNULL(@nu_fax, ''), 
			ID_REGION = ISNULL(@coddpto, ''),
			Id_Ciudad = ISNULL(@codprov, ''), 
			Id_Comuna = ISNULL(@coddist, ''), 
			Id_Pais = ISNULL(@nid_pais, '')
		WHERE Tipo_Docto_Identidad = @co_tipo_documento 
			AND Rut = @nu_documento_original 

	END
	
    END TRY  
    BEGIN CATCH  
        SELECT ERROR_NUMBER() AS ERROR;  
    END CATCH;

CREATE DATABASE Prueba_Ismael_Ruge
GO

USE Prueba_Ismael_ruge
GO

CREATE TABLE Pacientes (Id int primary key identity(1,1)
						, TipoDocumento Varchar(2) NOT NULL
						, NumeroDocumento float NOT NULL
						, Nombres Varchar (255) NOT NULL
						, Apellidos Varchar(255) NOT NULL
						, CorreoElectronico VARCHAR(MAX)
						, Telefono float
						, FechaNacimiento DATETIME
						, EstadoAfiliacion BIT NOT NULL)
GO

SELECT * FROM SYS.tables
GO

SELECT * FROM Pacientes
GO

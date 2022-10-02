CREATE TABLE [dbo].[Addresses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Street] VARCHAR(100) NOT NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [Country] VARCHAR(50) NOT NULL, 
    [ZipCode] VARCHAR(5) NULL
)

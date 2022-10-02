CREATE TABLE [dbo].[PeopleAddresses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NOT NULL, 
    [AddressId] INT NOT NULL
)

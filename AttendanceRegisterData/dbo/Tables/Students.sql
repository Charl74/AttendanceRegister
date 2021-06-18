CREATE TABLE [dbo].[Students]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
	[Title] NVARCHAR(5) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [GradeId] INT NOT NULL 
)

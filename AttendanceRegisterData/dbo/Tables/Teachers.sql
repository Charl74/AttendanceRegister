CREATE TABLE [dbo].[Teachers]
(
    [Id] NVARCHAR(128) NOT NULL Primary key,
	[Title] NVARCHAR(5) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(256) NOT NULL 
)

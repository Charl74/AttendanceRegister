CREATE TABLE [dbo].[StudentClasses]
(
	[Id] INT NOT NULL identity PRIMARY KEY, 
    [StudentId] INT NOT NULL, 
    [ClassId] INT NOT NULL
)

CREATE TABLE [dbo].[Classes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TeacherId] NVARCHAR(128) NOT NULL, 
    [DayOfWeek] NVARCHAR(10) NOT NULL, 
    [GradeId] INT NOT NULL, 
    [Subject] NVARCHAR(50) NOT NULL, 
    [CreatedDateTime] DATETIME2 NOT NULL
)

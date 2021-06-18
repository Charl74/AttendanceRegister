CREATE TABLE [dbo].[Attendance]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
    [TeacherId] NVARCHAR(128) NOT NULL,  
    [ClassId] INT NOT NULL, 
    [StudentId] INT NOT NULL, 
    [ClassAttended] BIT NOT NULL, 
    [ClassDate] DATE NOT NULL, 
    [ClassTime] TIME NOT NULL
)

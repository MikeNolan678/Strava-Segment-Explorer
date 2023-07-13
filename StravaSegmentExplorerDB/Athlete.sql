CREATE TABLE [dbo].[Athlete]
(
	[AthleteId] INT NOT NULL PRIMARY KEY, 
    [Id] NVARCHAR(450) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(50) NULL, 
    [Sex] NVARCHAR(10) NULL,
    [Weight] INT NULL
)

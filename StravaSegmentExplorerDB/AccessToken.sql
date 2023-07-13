CREATE TABLE [dbo].[AccessToken]
(
	[AthleteId] INT NOT NULL , 
    [Id] NVARCHAR(450) NULL,
    [AccessToken] NVARCHAR(50) NOT NULL, 
    [ExpiresAt] INT NOT NULL, 
    PRIMARY KEY ([AthleteId])
)

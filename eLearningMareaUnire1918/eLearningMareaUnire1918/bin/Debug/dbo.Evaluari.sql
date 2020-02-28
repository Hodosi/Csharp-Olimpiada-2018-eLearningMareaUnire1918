CREATE TABLE [dbo].[Evaluari]
(
	[IdEvaluare] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdElev] INT NULL, 
    [DataEvaluare] DATETIME NULL, 
    [NotaEvaluare] INT NULL
)

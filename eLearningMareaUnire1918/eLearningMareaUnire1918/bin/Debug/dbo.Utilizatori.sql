CREATE TABLE [dbo].[Utilizatori] (
    [IdUtilizator]          INT  IDENTITY (1, 1) NOT NULL,
    [NumePrenumeUtilizator] VARCHAR(50) NULL,
    [ParolaUtilizator]      VARCHAR(50) NULL,
    [EmailUtilizator]       VARCHAR(50) NULL,
    [ClasaUtilizator]       VARCHAR(50) NULL,
    PRIMARY KEY CLUSTERED ([IdUtilizator] ASC)
);


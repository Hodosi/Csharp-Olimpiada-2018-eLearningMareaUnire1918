CREATE TABLE [dbo].[Itemi] (
    [IdItem]            INT  IDENTITY (1, 1) NOT NULL,
    [TipItem]           INT  NULL,
    [EnuntItem]         VARCHAR(200) NULL,
    [Raspuns1Item]      VARCHAR(200) NULL,
    [Raspuns2Item]      VARCHAR(200) NULL,
    [Raspuns3Item]      VARCHAR(200) NULL,
    [Raspuns4Item]      VARCHAR(200) NULL,
    [RaspunsCorectItem] VARCHAR(200) NULL,
    PRIMARY KEY CLUSTERED ([IdItem] ASC)
);


CREATE TABLE [dbo].[Prefectures] (
    [Id]                 TINYINT       NOT NULL,
    [SubdivisionName_JA] NVARCHAR (16) NOT NULL,
    [SubdivisionName_EN] NVARCHAR (16) NOT NULL,
    CONSTRAINT [PK_Prefectures] PRIMARY KEY CLUSTERED ([Id] ASC)
);


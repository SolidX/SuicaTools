CREATE TABLE [dbo].[EkiData_LineTypes] (
    [Id]      TINYINT       NOT NULL,
    [Name_JA] NVARCHAR (16) NOT NULL,
    [Name_EN] VARCHAR (32)  NOT NULL,
    CONSTRAINT [PK_EkiDataLineTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


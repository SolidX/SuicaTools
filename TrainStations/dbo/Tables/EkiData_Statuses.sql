CREATE TABLE [dbo].[EkiData_Statuses] (
    [Id]      TINYINT      NOT NULL,
    [Name_JA] NVARCHAR (8) NOT NULL,
    [Name_EN] VARCHAR (12) NOT NULL,
    CONSTRAINT [PK_EkiDataStatuses] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[EkiData_CompanyTypes] (
    [Id]      TINYINT      NOT NULL,
    [Name_JA] NVARCHAR (8) NOT NULL,
    [Name_EN] VARCHAR (32) NOT NULL,
    CONSTRAINT [PK_EkiData_CompanyTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


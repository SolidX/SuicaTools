CREATE TABLE [dbo].[EkiData_Companies] (
    [CompanyCode]   INT           NOT NULL,
    [RailroadCode]  INT           NOT NULL,
    [Name]          NVARCHAR (80) NOT NULL,
    [Name_Katakana] NVARCHAR (80) NULL,
    [Name_English]  VARCHAR (80)  NULL,
    [OfficialName]  NVARCHAR (80) NULL,
    [ShortName]     NVARCHAR (80) NULL,
    [Website]       VARCHAR (256) NULL,
    [Type]          TINYINT       NULL,
    [Status]        TINYINT       NULL,
    CONSTRAINT [PK_EkiDataCompanies] PRIMARY KEY CLUSTERED ([CompanyCode] ASC),
    CONSTRAINT [FK_EkiDataCompanies_Status] FOREIGN KEY ([Status]) REFERENCES [dbo].[EkiData_Statuses] ([Id]),
    CONSTRAINT [FK_EkiDataCompanies_Type] FOREIGN KEY ([Type]) REFERENCES [dbo].[EkiData_CompanyTypes] ([Id])
);


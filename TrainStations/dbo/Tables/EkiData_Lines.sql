CREATE TABLE [dbo].[EkiData_Lines] (
    [LineCode]             INT             NOT NULL,
    [CompanyCode]          INT             NOT NULL,
    [Name]                 NVARCHAR (80)   NOT NULL,
    [Name_Katakana]        NVARCHAR (80)   NULL,
    [Name_English]         NVARCHAR (80)   NULL,
    [OfficialName]         NVARCHAR (80)   NULL,
    [OfficialName_English] NVARCHAR (80)   NULL,
    [ColorCode]            CHAR (6)        NULL,
    [ColorName]            NVARCHAR (10)   NULL,
    [Type]                 TINYINT         NULL,
    [CentralLongitude]     DECIMAL (12, 8) NULL,
    [CentralLatitude]      DECIMAL (12, 8) NULL,
    [GoogleMapZoomLevel]   TINYINT         NULL,
    [Status]               TINYINT         NULL,
    CONSTRAINT [PK_EkiDataLines] PRIMARY KEY CLUSTERED ([LineCode] ASC),
    CONSTRAINT [FK_EkiDataLines_Status] FOREIGN KEY ([Status]) REFERENCES [dbo].[EkiData_Statuses] ([Id]),
    CONSTRAINT [FK_EkiDataLines_Type] FOREIGN KEY ([Type]) REFERENCES [dbo].[EkiData_LineTypes] ([Id])
);


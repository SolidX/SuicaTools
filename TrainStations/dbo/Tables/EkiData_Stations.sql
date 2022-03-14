CREATE TABLE [dbo].[EkiData_Stations] (
    [StationCode]      INT             NOT NULL,
    [StationGroupCode] INT             NOT NULL,
    [Name]             NVARCHAR (80)   NOT NULL,
    [Name_English]     NVARCHAR (80)   NULL,
    [LineCode]         INT             NOT NULL,
    [Prefecture]       TINYINT         NULL,
    [PostalCode]       VARCHAR (10)    NULL,
    [Address]          NVARCHAR (300)  NULL,
    [Longitude]        DECIMAL (12, 8) NULL,
    [Latitude]         DECIMAL (12, 8) NULL,
    [OpeningDate]      DATE            NULL,
    [ClosingDate]      DATE            NULL,
    [Status]           TINYINT         NULL,
    CONSTRAINT [PK_EkiDataStations] PRIMARY KEY CLUSTERED ([StationCode] ASC),
    CONSTRAINT [FK_EkiDataLines_LineCode] FOREIGN KEY ([LineCode]) REFERENCES [dbo].[EkiData_Lines] ([LineCode]),
    CONSTRAINT [FK_EkiDataStations_Prefecture] FOREIGN KEY ([Prefecture]) REFERENCES [dbo].[Prefectures] ([Id]),
    CONSTRAINT [FK_EkiDataStations_Status] FOREIGN KEY ([Status]) REFERENCES [dbo].[EkiData_Statuses] ([Id])
);


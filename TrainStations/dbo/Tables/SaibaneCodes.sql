CREATE TABLE [dbo].[SaibaneCodes] (
    [RegionCode]             TINYINT       NOT NULL,
    [LineCode]               TINYINT       NOT NULL,
    [StationCode]            TINYINT       NOT NULL,
    [EkiData_StationCode]    INT           NULL,
    [StationNameOverride_ja] NVARCHAR (80) NULL,
    [StationNameOverride_en] VARCHAR (80)  NULL,
    CONSTRAINT [PK_SaibaneCodes] PRIMARY KEY CLUSTERED ([RegionCode] ASC, [LineCode] ASC, [StationCode] ASC),
    CONSTRAINT [UQ_CodeMap] UNIQUE NONCLUSTERED ([RegionCode] ASC, [LineCode] ASC, [StationCode] ASC, [EkiData_StationCode] ASC)
);


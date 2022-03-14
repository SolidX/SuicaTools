CREATE TABLE [dbo].[SaibaneCodes] (
    [RegionCode]             TINYINT       NOT NULL,
    [LineCode]               TINYINT       NOT NULL,
    [StationCode]            TINYINT       NOT NULL,
    [OperatorName]           NVARCHAR(80)  NOT NULL,
    [LineName]               NVARCHAR(80)  NOT NULL,
    [StationName]            NVARCHAR(80)  NOT NULL,
    [StationName_English]    NVARCHAR(80)  NULL, 
    CONSTRAINT [PK_SaibaneCodes] PRIMARY KEY CLUSTERED ([RegionCode] ASC, [LineCode] ASC, [StationCode] ASC)
);

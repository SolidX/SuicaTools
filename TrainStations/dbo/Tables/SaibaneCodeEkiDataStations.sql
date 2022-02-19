CREATE TABLE [dbo].[SaibaneCodeEkiDataStations]
(
	[RegionCode]          TINYINT NOT NULL,
	[LineCode]            TINYINT NOT NULL,
	[StationCode]         TINYINT NOT NULL,
	[EkiData_StationCode] INT NOT NULL,
	CONSTRAINT [PK_SaibaneCodeEkiDataStations] PRIMARY KEY CLUSTERED ([RegionCode] ASC, [LineCode] ASC, [StationCode] ASC, [EkiData_StationCode] ASC),
	CONSTRAINT [FK_SaibaneCodeEkiDataStations_SaibaneCode] FOREIGN KEY ([RegionCode], [LineCode], [StationCode]) REFERENCES SaibaneCodes([RegionCode], [LineCode], [StationCode]),
	CONSTRAINT [FK_SaibaneCodeEkiDataStations_StationCode] FOREIGN KEY ([EkiData_StationCode]) REFERENCES EkiData_Stations([StationCode])
)

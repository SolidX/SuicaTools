CREATE TABLE [dbo].[IruCa_BusStop]
(
	[Id]             INT           NOT NULL,
	[LineCode]       INT           NOT NULL,
	[StationCode]    INT           NOT NULL,
	[OperatorName]   NVARCHAR(80)  NOT NULL,
	[LineName]       NVARCHAR(80)  NULL,
	[StationName]    NVARCHAR(80)  NOT NULL,
	[Note]           NVARCHAR(80)  NULL,
	CONSTRAINT [PK_IruCa_BusStop] PRIMARY KEY CLUSTERED (Id ASC)
)

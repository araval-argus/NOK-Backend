-- Table to store strategic action impacts.
CREATE TABLE [dbo].[StrategicActionImpacts]
(
  [ImpactId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [Impact] [nvarchar](50) NULL
)
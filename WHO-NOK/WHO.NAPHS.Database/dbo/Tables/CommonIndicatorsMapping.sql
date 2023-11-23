-- Table to store mappings of indicators.
CREATE TABLE [dbo].[CommonIndicatorsMapping]
(
  [CommonIndicatorsMappingId] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [CommonIndicatorId] INT NOT NULL,
  [IndicatorCode] NVARCHAR(10) NULL,
  [IndicatorId] NVARCHAR(10) NULL,
  [Type] INT NOT NULL,
  CONSTRAINT [FK_CommonIndicator_IndicatorId] FOREIGN KEY ([CommonIndicatorId]) REFERENCES [dbo].[CommonIndicators]([IndicatorId])
)
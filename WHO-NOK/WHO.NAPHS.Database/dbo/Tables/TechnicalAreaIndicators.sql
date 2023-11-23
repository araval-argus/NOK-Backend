-- Table to store technical area indicators.
CREATE TABLE [dbo].[TechnicalAreaIndicators]
(
    [TechnicalAreaIndicatorId] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [Name] [nvarchar](max) NULL,
    [TechnicalAreaId] [int] NULL,
    [IndicatorCode] [nvarchar](10) NULL,
    [IndicatorCodeId] NVARCHAR(10),
    [CreatedAt] [datetime2](7) NULL,
    [CreatedBy] [int] NULL,
    [LastUpdatedAt] [datetime2](7) NULL,
    [LastUpdatedBy] [int] NULL,
    CONSTRAINT [FK_TechnicalArea_Indicator] FOREIGN KEY ([TechnicalAreaId]) REFERENCES [dbo].[TechnicalAreas]([TechnicalAreaId])
)

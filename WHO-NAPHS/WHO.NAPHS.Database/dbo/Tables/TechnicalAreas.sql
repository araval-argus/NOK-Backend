-- Table to store technical areas.
CREATE TABLE [dbo].[TechnicalAreas]
(
  [TechnicalAreaId] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [Name] [nvarchar](1000) NULL,
  [AreaCode] [nvarchar](10) NULL,
  [AreaCodeId] [nvarchar](10) NULL,
  [IsActive] [bit] NULL,
  [SourceId] [int] NULL,
  [CountryId] [int] NULL,
  [CommonTechnicalAreaId] [int] NULL,
  [IsCustomTechnicalArea] [bit] NOT NULL DEFAULT 0,
  [CreatedAt] [datetime2](7) NULL,
  [CreatedBy] [int] NULL,
  [LastUpdatedAt] [datetime2](7) NULL,
  [LastUpdatedBy] [int] NULL,
  CONSTRAINT [FK_TechnicalAreas_Source] FOREIGN KEY ([SourceId]) REFERENCES [dbo].[Sources]([SourceId]),
  CONSTRAINT [FK_TechnicalAreas_CommonTechnicalAreas] FOREIGN KEY ([CommonTechnicalAreaId]) REFERENCES [dbo].[CommonTechnicalAreas]([CommonTechnicalAreaId])
)

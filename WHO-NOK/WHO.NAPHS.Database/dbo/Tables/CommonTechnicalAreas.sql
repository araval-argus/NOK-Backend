-- Table to store common technical areas for both JEE/ESPAR.
CREATE TABLE [dbo].[CommonTechnicalAreas]
(
  [CommonTechnicalAreaId] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [DisplayName] [nvarchar](1000) NOT NULL,
  [IndicatorId] [int] NOT NULL,
  [OrderBy] [int] NOT NULL
)

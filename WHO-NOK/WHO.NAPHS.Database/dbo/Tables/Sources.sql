-- Table to store source details of assessments and strategic actions.
CREATE TABLE [dbo].[Sources]
(
  [SourceId] [int] IDENTITY(0, 1) NOT NULL PRIMARY KEY,
  [Name] [nvarchar](200) NULL,
  [Description] NVARCHAR(1000)
)

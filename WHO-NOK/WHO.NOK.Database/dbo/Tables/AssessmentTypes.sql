-- Table to store assessment types.
CREATE TABLE [dbo].[AssessmentTypes]
(
  [AssessmentTypeId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  [Type] NVARCHAR(50) NOT NULL
)

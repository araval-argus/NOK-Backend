-- Table to store list of plan types.
CREATE TABLE [dbo].[PlanTypes]
(
  [PlanTypeId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  [Type] NVARCHAR(50) NOT NULL
)

-- Table to store different stages of plan.
CREATE TABLE [dbo].[PlanStages]
(
  [PlanStageId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  [Stage] NVARCHAR(50) NOT NULL
)

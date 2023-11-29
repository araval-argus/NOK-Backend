-- Table to store list of plan statuses.
CREATE TABLE [dbo].[PlanStatuses]
(
  [PlanStatusId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  [Status] NVARCHAR(MAX) NOT NULL
)

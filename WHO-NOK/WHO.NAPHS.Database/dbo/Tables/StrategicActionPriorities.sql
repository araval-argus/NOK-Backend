-- Table to store list of priorities for strategic actions.
CREATE TABLE [dbo].[StrategicActionPriorities]
(
  [PriorityId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [Priority] [nvarchar](50) NULL
)
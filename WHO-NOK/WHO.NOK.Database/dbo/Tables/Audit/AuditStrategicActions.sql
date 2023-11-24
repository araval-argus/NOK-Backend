-- Audit Country Plan table.
CREATE TABLE [dbo].[AuditStrategicActions]
(
    [AuditStrategicActionId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [AuditLogRowStatus] INT NOT NULL,
    [StrategicActionId] [int] NOT NULL,
    [PlanIndicatorId] [int] NOT NULL,
    [Objective] [nvarchar](MAX),
    [Action] [nvarchar](MAX),
    [Feasibility][int] NOT NULL,
    [Impact][int] NOT NULL,
    [Priority][int] NOT NULL,
    [ImplementationStatus][INT] NOT NULL,
    [ResponsibleAuthority] [nvarchar](1000),
    [EstimatedCost] [float],
    [Comments] [nvarchar](MAX),
    [Source][int] NULL,
    [Score] [int] NULL,
    [Goal] [int] NULL,
    [CreatedAt] [datetime2](7) NOT NULL,
    [CreatedBy] [int] NOT NULL,
    [LastUpdatedAt] [datetime2](7) NOT NULL,
    [LastUpdatedBy] [int] NOT NULL
);

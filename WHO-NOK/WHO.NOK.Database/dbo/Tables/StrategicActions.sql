-- Table to store strategic actions for a plan.
CREATE TABLE dbo.[StrategicActions]
(
  [StrategicActionId] [int] IDENTITY(1, 1) PRIMARY KEY,
  [ReferenceId] INT NULL,
  [PlanIndicatorId] [int] NOT NULL,
  [Objective] [nvarchar](MAX),
  [Action] [nvarchar](MAX),
  [Feasibility][int] NOT NULL,
  [Impact][int] NOT NULL,
  [Priority][int] NOT NULL,
  [ImplementationStatus] INT NOT NULL,
  [ResponsibleAuthority] [nvarchar](1000),
  [EstimatedCost] [float],
  [Comments] [nvarchar](MAX),
  [Source][int] NULL,
  [Score] [int] NULL,
  [Goal] [int] NULL,
  [CreatedAt] [datetime2](7) NOT NULL,
  [CreatedBy] [int] NOT NULL,
  [LastUpdatedAt] [datetime2](7) NOT NULL,
  [LastUpdatedBy] [int] NOT NULL,
  [IsDeleted] [bit] NOT NULL,
  CONSTRAINT [FK_StrategicActions_CountryPlanIndicators] FOREIGN KEY ([PlanIndicatorId]) REFERENCES dbo.CountryPlanIndicators([PlanIndicatorId]),
  CONSTRAINT [FK_StrategicActions_StrategicPlanPriorities] FOREIGN KEY ([Priority]) REFERENCES dbo.StrategicActionPriorities(PriorityId),
  CONSTRAINT [FK_StrategicActions_StrategicPlanFeasibility] FOREIGN KEY ([Feasibility]) REFERENCES dbo.StrategicActionFeasibility(FeasibilityId),
  CONSTRAINT [FK_StrategicActions_StrategicPlanImpacts] FOREIGN KEY ([Impact]) REFERENCES dbo.StrategicActionImpacts(ImpactId),
  CONSTRAINT [FK_StrategicActions_StrategicActions] FOREIGN KEY ([ReferenceId]) REFERENCES [dbo].[StrategicActions]([StrategicActionId])
);


GO
CREATE TRIGGER [dbo].[triggerAuditStrategicActions] on [dbo].[StrategicActions]
AFTER UPDATE, INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @ReferenceId AS INT = (SELECT TOP 1 ReferenceId from inserted)

	IF(@ReferenceId IS NOT NULL) RETURN;

	INSERT INTO [AuditStrategicActions] 
		(
		AuditLogRowStatus,
		StrategicActionId,
		PlanIndicatorId,
		Objective,
		Action,
		Feasibility,
		Impact,
		Priority,
		ImplementationStatus,
		ResponsibleAuthority,
		EstimatedCost,
		Comments,
		Source,
		Score,
		Goal,
		CreatedAt,
		CreatedBy,
		LastUpdatedAt,
		LastUpdatedBy)
    SELECT 
		CASE WHEN IsDeleted = 1 THEN 3  --soft delete operation 
		WHEN EXISTS (SELECT * FROM deleted) THEN 2 --update
		ELSE 1 END, --create

		StrategicActionId,
		PlanIndicatorId,
		Objective,
		Action,
		Feasibility,
		Impact,
		Priority,
		ImplementationStatus,
		ResponsibleAuthority,
		EstimatedCost,
		Comments,
		Source,
		Score,
		Goal,
		GETUTCDATE(),CreatedBy,GETUTCDATE(),LastUpdatedBy
    FROM inserted

END


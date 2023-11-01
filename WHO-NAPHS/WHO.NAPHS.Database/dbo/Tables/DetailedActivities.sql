-- Table to store detailed activities.
CREATE TABLE [dbo].[DetailedActivities] (
    [DetailedActivityId]      INT             IDENTITY (1, 1) NOT NULL,
    [ReferenceId]             INT             NULL,
    [StrategicActionId]       INT             NOT NULL,
    [Description]             NVARCHAR (1000) NULL,
    [StartDate]               DATETIME2 (7)   NULL,
    [EndDate]                 DATETIME2 (7)   NULL,
    [ImplementationStatus]    INT             NOT NULL,
    [Feasibility]             INT             NOT NULL,
    [Impact]                  INT             NOT NULL,
    [Priority]                INT             NOT NULL,
    [ActivityTypeIds]         NVARCHAR (MAX) NULL,
    [Source]                  INT             NULL,
    [RiskName]                NVARCHAR (1000) NULL,
    [RiskLevel]               INT             NULL,
    [Deadline]                NVARCHAR (1000) NULL,
    [Responsible]             NVARCHAR (1000) NULL,
    [ResponsibleAuthority]    NVARCHAR (MAX)  NULL,
    [InstituteId]             INT             NULL,
    [CostAssumptions]         NVARCHAR (1000) NULL,
    [EstimatedCost]           FLOAT (53)      NULL,
    [FundAvailability]        BIT             NULL,
    [EstimatedBudgetSource]   NVARCHAR (1000) NULL,
    [ExistingBudget]          FLOAT (53)      NULL,
    [FinancialGap]            FLOAT (53)      NULL,
    [NeedTechnicalAssistance] BIT             NULL,
    [NeedFinancialAssistance] BIT             NULL,
    [DonorContribution]       FLOAT (53)      NULL,
    [Donor]                   NVARCHAR (1000) NULL,
    [ActualCost]              FLOAT (53)      NULL,
    [Comments]                NVARCHAR (1000) NULL,
    [CreatedAt]               DATETIME2 (7)   NOT NULL,
    [CreatedBy]               INT             NOT NULL,
    [LastUpdatedAt]           DATETIME2 (7)   NOT NULL,
    [LastUpdatedBy]           INT             NOT NULL,
    [IsDeleted]               BIT             NULL,
    CONSTRAINT [PK_DetailedActivities] PRIMARY KEY ([DetailedActivityId]),
    CONSTRAINT [FK_DetailedActivities_StrategicPlanPriorities] FOREIGN KEY ([Priority]) REFERENCES [dbo].[StrategicActionPriorities] ([PriorityId]),
    CONSTRAINT [FK_DetailedActivities_CollaboratingInstitutions] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[CollaboratingInstitutions] ([InstituteId]),
    CONSTRAINT [FK_DetailedActivities_StrategicPlanImpacts] FOREIGN KEY ([Impact]) REFERENCES [dbo].[StrategicActionImpacts] ([ImpactId]),
    CONSTRAINT [FK_DetailedActivities_StrategicPlanFeasibility] FOREIGN KEY ([Feasibility]) REFERENCES [dbo].[StrategicActionFeasibility] ([FeasibilityId]),
    CONSTRAINT [FK_DetailedActivities_StrategicActions] FOREIGN KEY ([StrategicActionId]) REFERENCES [dbo].[StrategicActions] ([StrategicActionId]),
    CONSTRAINT [FK_DetailedActivites_DetailedActivites] FOREIGN KEY ([ReferenceId]) REFERENCES [dbo].[DetailedActivities]([DetailedActivityId])
);


GO
CREATE TRIGGER [dbo].[triggerAuditDetailedActivities] on [dbo].[DetailedActivities]
AFTER UPDATE, INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @ReferenceId AS INT = (SELECT TOP 1 ReferenceId from inserted)

	IF(@ReferenceId IS NOT NULL) RETURN;

	INSERT INTO [AuditDetailedActivities] 
		(
		AuditLogRowStatus,
		DetailedActivityId,
		StrategicActionId,
		Description,
		StartDate,
		EndDate,
		ImplementationStatus,
		Feasibility,
		Impact,
		Priority,
		Source,
		RiskName,
		RiskLevel,
		Responsible,
		ResponsibleAuthority,
		InstituteId,
		CostAssumptions,
		EstimatedCost,
		FundAvailability,
		EstimatedBudgetSource,
		ExistingBudget,
		FinancialGap,
		NeedTechnicalAssistance,
		NeedFinancialAssistance,
		DonorContribution,
		Donor,
		ActualCost,
		Comments,
		CreatedAt,
		CreatedBy,
		LastUpdatedAt,
		LastUpdatedBy)
    SELECT 
		CASE WHEN IsDeleted = 1 THEN 3  --soft delete operation 
		WHEN EXISTS (SELECT * FROM deleted) THEN 2 --update
		ELSE 1 END, --create

		DetailedActivityId,
		StrategicActionId,
		Description,
		StartDate,
		EndDate,
		ImplementationStatus,
		Feasibility,
		Impact,
		Priority,
		Source,
		RiskName,
		RiskLevel,
		Responsible,
		ResponsibleAuthority,
		InstituteId,
		CostAssumptions,
		EstimatedCost,
		FundAvailability,
		EstimatedBudgetSource,
		ExistingBudget,
		FinancialGap,
		NeedTechnicalAssistance,
		NeedFinancialAssistance,
		DonorContribution,
		Donor,
		ActualCost,
		Comments,
		GETUTCDATE(),CreatedBy,GETUTCDATE(),LastUpdatedBy
    FROM inserted

END
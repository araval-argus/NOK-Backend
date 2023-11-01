-- Country Plan table.
CREATE TABLE [dbo].[CountryPlans]
(
    [CountryPlanId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Description] NVARCHAR(MAX) NULL,
    [StrategicPlanId] int NULL,
    [PlanTypeId] INT NOT NULL,
    [CountryId] INT NOT NULL,
    [PlanStartDate] DATETIME2(7) NOT NULL,
    [PlanEndDate] DATETIME2(7) NOT NULL,
    [AssessmentTypeId] INT NOT NULL,
    [PlanStatusId] INT NOT NULL,
    [PlanStageId] INT NOT NULL,
    [PlanCode] NVARCHAR(50) NOT NULL,
    [SendReviewReminder] BIT NOT NULL DEFAULT 1,
    [AdvancedDaysForReviewReminder] INT NULL,
    [CountryISOCode] NVARCHAR(10),
    [HasOfficiallyApprovedPlan] BIT NOT NULL DEFAULT 0,
    [VisibleToAnotherCountries] BIT NOT NULL DEFAULT 0,
    [CountryPlanFrequency] BIT NULL,
    [CreatedAt] [datetime2](7) NOT NULL,
    [CreatedBy] [int] NOT NULL,
    [LastUpdatedAt] [datetime2](7) NOT NULL,
    [LastUpdatedBy] [int] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    CONSTRAINT [FK_CountryPlans_AssessmentTypes] FOREIGN KEY ([AssessmentTypeId]) REFERENCES dbo.[AssessmentTypes](AssessmentTypeId),
    CONSTRAINT [FK_CountryPlans_PlanTypes] FOREIGN KEY ([PlanTypeId]) REFERENCES dbo.[PlanTypes](PlanTypeId),
    CONSTRAINT [FK_CountryPlans_PlanStages] FOREIGN KEY ([PlanStageId]) REFERENCES dbo.[PlanStages](PlanStageId),
    CONSTRAINT [FK_CountryPlans_PlanStatus] FOREIGN KEY ([PlanStatusId]) REFERENCES dbo.[PlanStatuses](PlanStatusId)
);

GO
CREATE NONCLUSTERED INDEX [IX_PlanTypeId]
    ON [dbo].[CountryPlans]([PlanTypeId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_CountryId]
    ON [dbo].[CountryPlans]([CountryId] ASC);

GO
CREATE TRIGGER [dbo].[triggerAuditCountryPlan] on [dbo].[CountryPlans]
AFTER UPDATE, INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [AuditCountryPlans]
		(
		AuditLogRowStatus,
		CountryPlanId,
		Description,
		StrategicPlanId,
		PlanTypeId,
		CountryId,
		PlanStartDate,
		PlanEndDate,
		AssessmentTypeId,
		PlanStatusId,
		PlanStageId,
		PlanCode,
		SendReviewReminder,
		AdvancedDaysForReviewReminder,
		CountryISOCode,
		HasOfficiallyApprovedPlan,
		VisibleToAnotherCountries,
		CountryPlanFrequency,
		CreatedAt,
		CreatedBy,
		LastUpdatedAt,
		LastUpdatedBy)
    SELECT
		CASE WHEN IsDeleted = 1 THEN 3  --soft delete operation 
		WHEN EXISTS (SELECT * FROM deleted WHERE CountryPlanId = CountryPlanId) THEN 2 --update
		ELSE 1 END, --create

		CountryPlanId,
		Description,
		StrategicPlanId,
		PlanTypeId,
		CountryId,
		PlanStartDate,
		PlanEndDate,
		AssessmentTypeId,
		PlanStatusId,
		PlanStageId,
		PlanCode,
		SendReviewReminder,
		AdvancedDaysForReviewReminder,
		CountryISOCode,
		HasOfficiallyApprovedPlan,
		VisibleToAnotherCountries,
		CountryPlanFrequency,
		GETUTCDATE(),CreatedBy,GETUTCDATE(),LastUpdatedBy
    FROM inserted

END
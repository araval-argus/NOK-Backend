-- Table to store country plans and indicator mapping table
CREATE TABLE [dbo].[CountryPlanIndicators]
(
  [PlanIndicatorId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [TechnicalAreaIndicatorId] [int] NOT NULL,
  [CountryPlanId] [int] NOT NULL,
  [Score] [int] NOT NULL,
  [Goal] [int] NOT NULL,
  [IsDeleted] BIT NOT NULL DEFAULT 0,
  [CreatedAt] [datetime2](7) NOT NULL,
  [CreatedBy] [int] NOT NULL,
  [LastUpdatedAt] [datetime2](7) NOT NULL,
  [LastUpdatedBy] [int] NOT NULL,
  CONSTRAINT [FK_CountryPlanIndicators_TechnicalAreaIndicators] FOREIGN KEY([TechnicalAreaIndicatorId]) REFERENCES [dbo].[TechnicalAreaIndicators] ([TechnicalAreaIndicatorId]),
  CONSTRAINT [FK_CountryPlanIndicators_CountryPlans] FOREIGN KEY([CountryPlanId]) REFERENCES [dbo].[CountryPlans] ([CountryPlanId])
);

GO
CREATE TRIGGER [dbo].[triggerAuditCountryPlanIndicators] on [dbo].[CountryPlanIndicators]
AFTER UPDATE, INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [AuditCountryPlanIndicators] 
		(
		AuditLogRowStatus,
		PlanIndicatorId,
		TechnicalAreaIndicatorId,
		CountryPlanId,
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

		PlanIndicatorId,
		TechnicalAreaIndicatorId,
		CountryPlanId,
		Score,
		Goal,
		GETUTCDATE(),CreatedBy,GETUTCDATE(),LastUpdatedBy
    FROM inserted

END
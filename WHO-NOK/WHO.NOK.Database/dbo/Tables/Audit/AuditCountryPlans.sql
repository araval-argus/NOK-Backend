-- Audit Country Plan table.
CREATE TABLE [dbo].[AuditCountryPlans]
(
    [AuditCountryPlanId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [AuditLogRowStatus] INT NOT NULL,
    [CountryPlanId] INT NOT NULL,
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
    [LastUpdatedBy] [int] NOT NULL
);

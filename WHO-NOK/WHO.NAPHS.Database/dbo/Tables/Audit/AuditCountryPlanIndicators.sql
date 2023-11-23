-- Table to store country plans and indicator mapping table
CREATE TABLE [dbo].[AuditCountryPlanIndicators]
(
    [AuditPlanIndicatorId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [AuditLogRowStatus] INT NOT NULL,
    [PlanIndicatorId] [int] NOT NULL,
    [TechnicalAreaIndicatorId] [int] NOT NULL,
    [CountryPlanId] [int] NOT NULL,
    [Score] [int] NOT NULL,
    [Goal] [int] NOT NULL,
    [CreatedAt] [datetime2](7) NOT NULL,
    [CreatedBy] [int] NOT NULL,
    [LastUpdatedAt] [datetime2](7) NOT NULL,
    [LastUpdatedBy] [int] NOT NULL
);
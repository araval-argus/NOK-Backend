-- Table to store information/history regarding file uploads.
CREATE TABLE [dbo].[PlanningTools]
(
    [PlanningToolId] INT NOT NULL IDENTITY(1,1),
    [FilePath] NVARCHAR (MAX) NOT NULL,
    [FileName] NVARCHAR (MAX) NOT NULL,
    [ExcelTypeId] INT NOT NULL,
    [CountryId] INT NULL,
    [CountryPlanId] INT NULL,
    [CreatedAt] DATETIME2(7) NULL,
    [CreatedBy] INT NOT NULL,
    [LastUpdatedAt] DATETIME2(7) NOT NULL,
    [LastUpdatedBy] INT NOT NULL,
    CONSTRAINT [PK_PlanningToolId] PRIMARY KEY CLUSTERED ([PlanningToolId] ASC),
    CONSTRAINT [FK_PlanningTools_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries]([CountryId]),
    CONSTRAINT [FK_PlanningTools_CountryPlans] FOREIGN KEY ([CountryPlanId]) REFERENCES [dbo].[CountryPlans]([CountryPlanId]),
);

--Table to store IHR recommended actions
CREATE TABLE [dbo].[IHRRecommendations]
(
    [IHRRecommendationId] INT NOT NULL IDENTITY(1,1),
    [IndicatorId] NVARCHAR (10) NOT NULL,
    [BenchMark] NVARCHAR (MAX) NOT NULL,
    [Objectives] NVARCHAR (MAX) NOT NULL,
    [Capacity] NVARCHAR(MAX) NOT NULL,
    [CountryId] INT NULL,
    [CountryPlanId] INT NULL,
    [PreviousScore] INT NOT NULL,
    [TargetScore] INT NOT NULL,
    [Actions] VARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2(7) NULL,
    [CreatedBy] INT NOT NULL,
    [LastUpdatedAt] DATETIME2(7) NOT NULL,
    [LastUpdatedBy] INT NOT NULL,
    CONSTRAINT [PK_IHRId] PRIMARY KEY CLUSTERED ([IHRRecommendationId] ASC),
    CONSTRAINT [FK_IHRRecommendations_CountryPlans] FOREIGN KEY ([CountryPlanId]) REFERENCES [dbo].[CountryPlans]([CountryPlanId]),
    CONSTRAINT [FK_IHRRecommendations_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries]([CountryId])
);

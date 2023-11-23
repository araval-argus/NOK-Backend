-- Table to store country plan reviews.
CREATE TABLE CountryPlanReviews
(
	[CountryPlanReviewId] int IDENTITY(1, 1) PRIMARY KEY,
	[CountryPlanId] int NOT NULL,
	[ReviewDate] datetime2(7),
	[ReviewStatus] INT NOT NULL,
	[IsDeleted] bit,
	[CreatedAt] DATETIME2 (7) NOT NULL,
	[CreatedBy] INT NOT NULL,
	[LastUpdatedAt] DATETIME2 (7) NOT NULL,
	[LastUpdatedBy] INT NOT NULL,
	CONSTRAINT [FK_CountryPlanReviews_CountryPlans] FOREIGN KEY ([CountryPlanId]) REFERENCES dbo.CountryPlans([CountryPlanId])
)
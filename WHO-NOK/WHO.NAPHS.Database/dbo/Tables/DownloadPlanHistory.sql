--Table to store data regarding which users have clicked on download plan button.
CREATE TABLE [dbo].[DownloadPlanHistory]
(
    [DownloadPlanId] INT PRIMARY KEY IDENTITY(1,1),
    [CountryPlanId] INT NOT NULL,
    [UserId] INT NULL,
    [CreatedAt] DATETIME2(7) NOT NULL,
    [CreatedBy] INT NOT NULL,
    [LastUpdatedAt] DATETIME2(7) NOT NULL,
    [LastUpdatedBy] INT NOT NULL,
    CONSTRAINT [FK_DownloadPlanData_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([UserId]),
    CONSTRAINT [FK_DownloadPlanData_CountryPlans] FOREIGN KEY([CountryPlanId]) REFERENCES [dbo].[CountryPlans] ([CountryPlanId])
);

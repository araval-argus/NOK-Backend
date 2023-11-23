-- Table to store detailed activity types.
CREATE TABLE [dbo].[DetailedActivityTypes]
(
    [ActivityTypeId] INT IDENTITY (1, 1) NOT NULL,
    [CountryId] INT NULL,
    [Activity] NVARCHAR (1000) NULL,
    [CreatedAt] DATETIME2 (7) NOT NULL,
    [CreatedBy] INT NOT NULL,
    [LastUpdatedAt] DATETIME2 (7) NOT NULL,
    [LastUpdatedBy] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ActivityTypeId] ASC)
);


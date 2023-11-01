-- Table to store collaborating institutes for detailed plan.
CREATE TABLE [dbo].[CollaboratingInstitutions]
(
    [InstituteId] INT IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (1000) NULL,
    [CountryId] INT NULL,
    [CreatedAt] DATETIME2 (7) NOT NULL,
    [CreatedBy] INT NOT NULL,
    [LastUpdatedAt] DATETIME2 (7) NOT NULL,
    [LastUpdatedBy] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([InstituteId] ASC)
);


-- Table to store user details.
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    ProfilePicture NVARCHAR(500),
    CountryId INT,
    PreferredLanguageId INT NOT NULL,
    JobTitle NVARCHAR(500),
    Institution NVARCHAR(1000),
    AffiliationId INT,
    AccessReason NVARCHAR(MAX),
    CreatedAt DATETIME NOT NULL,
    CreatedBy INT NOT NULL,
    LastUpdatedAt DATETIME NOT NULL,
    LastUpdatedBy INT NOT NULL,
    DeactivationRequest BIT NOT NULL,
    IsReadOnly BIT NOT NULL,
    IsDeleted BIT NOT NULL,
    Status INT,
    IsActive BIT NOT NULL,
    CONSTRAINT FK_CountryId FOREIGN KEY (CountryId) REFERENCES Countries(CountryId),
);

GO

CREATE NONCLUSTERED INDEX [IX_Email]
    ON [dbo].[Users]([Email] ASC);


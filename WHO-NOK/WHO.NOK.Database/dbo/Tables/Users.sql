-- Table to store user details.
CREATE TABLE [dbo].[Users]
(
    [UserId] INT IDENTITY(1,1) NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(200) NOT NULL,
    [UserRole] INT NOT NULL,
    [UserStatus] INT NOT NULL,
    [ProfilePicture] NVARCHAR(500) NULL,
    [CountryId] INT NULL,
    [AffiliationId] INT NULL,
    [Institution] NVARCHAR(1000),
    [AccessReason] NVARCHAR(MAX),
    [JobTitle] NVARCHAR (500) NULL,
    [IsDeleted] BIT NOT NULL,
    [CreatedAt] DATETIME2(7) NOT NULL,
    [CreatedBy] INT NOT NULL,
    [LastUpdatedAt] DATETIME2(7) NULL,
    [LastUpdatedBy] INT NULL,
    CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED ([UserId] ASC),
    
)

GO

CREATE NONCLUSTERED INDEX [IX_Email]
    ON [dbo].[Users]([Email] ASC);


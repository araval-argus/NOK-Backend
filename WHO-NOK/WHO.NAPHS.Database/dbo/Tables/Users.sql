-- Table to store user details.
CREATE TABLE [dbo].[Users]
(
    [UserId] INT IDENTITY(1,1) NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(200) NOT NULL,
    [ProfilePicture] NVARCHAR(200) NULL,
    [CountryId] INT NULL,
    [Region] NVARCHAR(10) NULL,
    [PreferredLanguageId] INT,
    [Institution] NVARCHAR(100),
    [Affiliation] NVARCHAR(100),
    [CreatedAt] DATETIME2(7) NOT NULL,
    [CreatedBy] INT NOT NULL,
    [LastUpdatedAt] DATETIME2(7) NOT NULL,
    [LastUpdatedBy] INT NOT NULL,
    [Status] INT NULL,
    [DeactivationRequest] BIT NOT NULL,
    [IsReadOnly] BIT NOT NULL,
    [IsActive] BIT NOT NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_PreferredLanguageId] FOREIGN KEY ([PreferredLanguageId]) REFERENCES [dbo].[Languages]([LanguageId])
)

GO
CREATE NONCLUSTERED INDEX [IX_CountryId]
    ON [dbo].[Users]([CountryId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Email]
    ON [dbo].[Users]([Email] ASC);


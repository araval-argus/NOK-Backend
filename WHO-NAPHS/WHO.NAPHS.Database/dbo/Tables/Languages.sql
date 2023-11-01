-- Table to store list of supported languages.
CREATE TABLE [dbo].[Languages]
(
  [LanguageId] INT NOT NULL,
  [Name] VARCHAR(100) NOT NULL,
  [LanguageCode] VARCHAR(50) NOT NULL,
  CONSTRAINT [PK_LanguageId] PRIMARY KEY CLUSTERED ([LanguageId] ASC)
)

-- Table to store System configurations.
CREATE TABLE [dbo].[Configurations]
(
  [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [Key] NVARCHAR(100) NOT NULL,
  [Value] NVARCHAR(1000) NULL,
)

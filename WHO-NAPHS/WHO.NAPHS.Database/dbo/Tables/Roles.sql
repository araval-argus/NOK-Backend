-- Table to store list of user roles.
CREATE TABLE [dbo].[Roles]
(
  [RoleId] INT NOT NULL,
  [Name] VARCHAR(100) NOT NULL,
  [Precedence] INT,
  [Description] NVARCHAR(200),
  CONSTRAINT [PK_RoleId] PRIMARY KEY CLUSTERED ([RoleId]  ASC)
)

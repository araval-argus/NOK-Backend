-- Table to store mapping for users and it's role
CREATE TABLE [dbo].[UserRoles]
(
  [UserRoleId] INT IDENTITY(1, 1) NOT NULL,
  [UserId] INT NOT NULL,
  [RoleId] INT NOT NULL,
  CONSTRAINT [PK_UserRoleId] PRIMARY KEY CLUSTERED ([UserRoleId] ASC),
  CONSTRAINT [FK_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([UserId]),
  CONSTRAINT [FK_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([RoleId])
)

-- This table defines the minimal role to have in order to perform certain activity. 
CREATE TABLE [dbo].[Permissions]
(
  [PermissionId] INT IDENTITY(1,1) PRIMARY KEY,
  [Activity] NVARCHAR(100),
  [MinimalRoleId] INT
)

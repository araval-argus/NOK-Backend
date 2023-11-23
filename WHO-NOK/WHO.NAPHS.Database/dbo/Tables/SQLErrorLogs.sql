-- Table to store catched errors at the SP level.
CREATE TABLE [dbo].[SQLErrorLogs]
(
  [Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [ErrorSeverity] [nvarchar](max) NULL,
  [ErrorState] [nvarchar](max) NULL,
  [ErrorProcedure] [nvarchar](max) NULL,
  [ErrorLine] [nvarchar](max) NULL,
  [ErrorMessage] [nvarchar](max) NULL,
  [ErrorDate] [datetime] NOT NULL,
  [ModuleName] [nvarchar](max) NULL
)

-- Table to store excel file mapping fields.
CREATE TABLE [dbo].[ExcelMappings]
(
  [Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [ExcelType] [int] NOT NULL,
  [ExcelColumn] [nvarchar](100) NULL,
  [DatabaseField] [nvarchar](100) NULL,
  [TableName] [nvarchar](100) NULL,
  [IsFirstRowColumnName] [bit] NULL,
  [MappingType] [int] NULL,
  [SheetName] [nvarchar](100) NULL
)

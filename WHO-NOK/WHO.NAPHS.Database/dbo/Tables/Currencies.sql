-- Table to store currency details.
CREATE TABLE [dbo].[Currencies]
(
  [CurrencyId] INT IDENTITY (1, 1) NOT NULL,
  [Code] CHAR (3) NOT NULL,
  [Sign] NVARCHAR (3) NULL,
  [IsDefault] BIT NULL,
  [ConversionFactor] DECIMAL (22, 12) NULL,
  [Description] VARCHAR (50) NULL,
  [CreatedAt] DATETIME2(7) NOT NULL,
  [CreatedBy] INT NOT NULL,
  [LastUpdatedAt] DATETIME2(7) NOT NULL,
  [LastUpdatedBy] INT NOT NULL,
  CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([CurrencyId] ASC)
)

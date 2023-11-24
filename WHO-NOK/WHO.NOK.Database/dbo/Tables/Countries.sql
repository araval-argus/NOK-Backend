-- Table to store country details.
CREATE TABLE [dbo].[Countries] (
  [CountryId] INT IDENTITY(1, 1),
  [Name] VARCHAR(100) NOT NULL,
  [ISOCode] VARCHAR(10) NOT NULL,
  [ISO3Code] VARCHAR(10) NULL,
  [Latitude] DECIMAL(20, 15) NULL,
  [Longitude] DECIMAL(20, 15) NULL,
  [Region] VARCHAR(10) NULL,
  [CurrencyId] INT NULL,
  CONSTRAINT [PK_CountryId] PRIMARY KEY CLUSTERED ([CountryId] ASC),
  CONSTRAINT [FK_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies]([CurrencyId]) 
  )

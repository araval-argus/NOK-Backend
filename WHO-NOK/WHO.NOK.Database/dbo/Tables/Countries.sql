-- Write your own SQL object definition here, and it'll be included in your package.
CREATE TABLE Countries (
    CountryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(MAX) NOT NULL,
    ISOCode NVARCHAR(MAX) NOT NULL,
    ISO3Code NVARCHAR(MAX) NOT NULL,
    Latitude DECIMAL(18, 6),
    Longitude DECIMAL(18, 6),
    Region NVARCHAR(MAX),
);
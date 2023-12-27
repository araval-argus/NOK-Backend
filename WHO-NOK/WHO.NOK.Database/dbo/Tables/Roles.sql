-- Write your own SQL object definition here, and it'll be included in your package.
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(20) NOT NULL,
    Description NVARCHAR(20) NOT NULL
);
-- Write your own SQL object definition here, and it'll be included in your package.
CREATE TABLE UserRoles (
    UserRoleId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    CONSTRAINT FK_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
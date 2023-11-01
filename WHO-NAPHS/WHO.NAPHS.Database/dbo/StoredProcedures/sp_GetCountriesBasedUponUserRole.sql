-- ==============================
-- Author:		<Kaif Pipadwala>
-- Create date: <26-09-2023>
-- Description:	<This stored procedure returns the list of countries based upon the role of the user>
-- =============================================

CREATE PROCEDURE [dbo].[sp_GetCountriesBasedUponUserRole]
    -- Add the parameters for the stored procedure here
    @userId INT
AS
BEGIN
    DECLARE @countryId INT, @role VARCHAR(100)
    
    SELECT @countryId = U.CountryId, @role = R.Name
    FROM [dbo].[Users] AS U
        JOIN [dbo].[UserRoles] AS UR ON U.UserId = UR.UserId
        JOIN [dbo].[Roles] AS R ON UR.RoleId = R.RoleId
    WHERE U.UserId = @userId

    IF(@role = 'System Admin' OR @role = 'Global Viewer')
	BEGIN
        SELECT *
        FROM [dbo].[Countries]
        ORDER BY [Name]
    END
	ELSE IF(@role = 'Regional Admin')
	BEGIN
        SELECT *
        FROM [dbo].[Countries]
        WHERE [Region] = (SELECT TOP 1 [Region]
        FROM [dbo].[Users]
        WHERE UserId = @userId)
        ORDER BY [Name]
    END
END
GO
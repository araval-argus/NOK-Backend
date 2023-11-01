-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <27-09-2023>
-- Description:	<Get user calims details>
-- =============================================

CREATE PROCEDURE sp_GetUserClaimsDetails
	-- Add the parameters for the stored procedure here
	@UserEmail nvarchar(100)
AS
BEGIN
	
	SELECT 
		TOP 1
		U.UserId,
		U.Email AS 'PrimaryEmail',
		U.FirstName,
		U.LastName,
		U.FirstName + ' ' + U.LastName AS 'DisplayName',
		U.PreferredLanguageId AS 'LanguageId',
		U.IsActive AS 'Enabled',
		U.IsReadOnly AS 'ReadOnly',
		C.CountryId,
		C.[Name] AS 'CountryName',
		CR.Region,
		UR.RoleId,
		U.ProfilePicture,
		IIF (CC.CurrencyId IS NULL, (SELECT TOP 1 [Sign] FROM Currencies WHERE IsDefault = 1), CC.[Sign]) AS 'Currency'
	FROM
	Users U
	INNER JOIN UserRoles UR
		ON UR.UserId = U.UserId
	LEFT JOIN Countries C
		ON C.CountryId = U.CountryId
	LEFT JOIN Countries CR
		ON CR.Region = U.Region	
	LEFT JOIN Currencies CC
		ON CC.CurrencyId = CR.CurrencyId
	WHERE U.Email = @UserEmail
	AND U.IsActive = 1
	AND U.IsDeleted = 0;

END

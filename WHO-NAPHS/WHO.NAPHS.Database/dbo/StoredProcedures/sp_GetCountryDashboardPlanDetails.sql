-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <30-08-2023>
-- Description:	<Get Dashboard Plan Details>
-- =============================================

CREATE PROCEDURE sp_GetCountryDashboardPlanDetails 
	@UserId int
AS
BEGIN
	
	DECLARE @UserType AS int;
	DECLARE @CountryId AS int;
	DECLARE @UserRegion AS nvarchar(10);

	SELECT 
		@UserType = UR.RoleId,
		@CountryId = U.CountryId,
		@UserRegion = U.Region
	FROM Users U
	INNER JOIN UserRoles UR
		ON UR.UserId = U.UserId
	WHERE UR.UserId = @UserId;

	-- For Global Viewer and System Admin
	IF (@UserType = 1 OR @UserType = 6) 
	BEGIN
		
		-- Return all the countries.
		SELECT 
			'' AS 'CountriesTable', 
			CountryId,
		    [Name],
		    ISOCode,
		    Region
		FROM Countries
		ORDER BY [Name];

		-- Return all the plans that were created in the system
		SELECT 
			'' AS 'PlansDetails', 
			CP.*,
			C.[Name]
		FROM CountryPlans CP
		INNER JOIN Countries C
			ON CP.CountryId = C.CountryId;

	END
	ELSE IF (@UserType = 2) -- Regional Admin
	BEGIN

		DECLARE @RegionCountries AS TABLE
		(
			CountryId int
		);

		INSERT INTO @RegionCountries(CountryId)
		SELECT 
			CountryId
		FROM Countries
		WHERE Region = @UserRegion;

		-- Return all the countries within region.
		SELECT 
			'' AS 'CountriesTable', 
			CountryId,
		    [Name],
		    ISOCode,
		    Region
		FROM Countries
		WHERE CountryId IN
		(
			SELECT CountryId FROM @RegionCountries
		)
		ORDER BY [Name];

		-- Return all the plans that were created in the system within the region.
		SELECT 
			'' AS 'PlansDetails', 
			CP.*,
			C.[Name]
		FROM CountryPlans CP
		INNER JOIN Countries C
			ON CP.CountryId = C.CountryId
		WHERE CP.CountryId IN
		(
			SELECT CountryId FROM @RegionCountries
		);

	END
	ELSE
	BEGIN
		
		SELECT 
			'' AS 'CountriesTable', 
			CountryId,
		    [Name],
		    ISOCode,
		    Region 
		FROM Countries
		WHERE CountryId = @CountryId
		ORDER BY [Name];

		-- Return all the plans that were created in the system within the region.
		SELECT 
			'' AS 'PlansDetails', 
			CP.*,
			C.[Name]
		FROM CountryPlans CP
		INNER JOIN Countries C
			ON CP.CountryId = C.CountryId
		WHERE CP.CountryId = @CountryId;		
		
	END
END

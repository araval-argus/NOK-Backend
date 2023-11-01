-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <23-08-2023>
-- Description:	<Get actions that requires financial/technical assistance.>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAssistanceDashboardData] 
	@NeedFinancialAssistance bit = 1,
	@NeedFilterData bit = 1,
    @UserId INT
AS
BEGIN
--Details of the user
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

    SELECT
        '' AS 'AssistanceDashboardTable',
        CP.PlanCode,
        IIF(CTA.CommonTechnicalAreaId IS NULL, TA.Name, CTA.DisplayName) AS TechnicalArea,
        TAI.Name AS TechnicalAreaIndicator,
        TAI.TechnicalAreaIndicatorId,
        SA.Action,
        DA.DetailedActivityId,
        DA.EstimatedCost,
        IIF(DA.StartDate IS NULL, CP.PlanStartDate, DA.StartDate) AS 'StartDate',
        IIF(DA.StartDate IS NULL, CP.PlanEndDate, DA.EndDate) AS 'EndDate',
        C.Name Country,
        CP.CountryId
    INTO #TempData
	FROM DetailedActivities DA
	INNER JOIN StrategicActions SA
		ON SA.StrategicActionId = DA.StrategicActionId
	INNER JOIN CountryPlanIndicators CPI
		ON CPI.PlanIndicatorId = SA.PlanIndicatorId
	INNER JOIN CountryPlans CP
		ON CP.CountryPlanId = CPI.CountryPlanId
	INNER JOIN TechnicalAreaIndicators TAI
		ON TAI.TechnicalAreaIndicatorId = CPI.TechnicalAreaIndicatorId
	INNER JOIN TechnicalAreas TA
		ON TA.TechnicalAreaId = TAI.TechnicalAreaId
	INNER JOIN Countries C
		ON CP.CountryId = C.CountryId
	LEFT JOIN CommonTechnicalAreas CTA
		ON CTA.CommonTechnicalAreaId = TA.CommonTechnicalAreaId
	WHERE (@NeedFinancialAssistance = 1 AND NeedFinancialAssistance = 1)
    OR (@NeedFinancialAssistance = 0 AND NeedTechnicalAssistance = 1)

	IF(@UserType = 1) --System admin
	BEGIN
		SELECT * FROM #TempData;
	END
	ELSE IF(@UserType = 2) --Regional admin
	BEGIN
		DECLARE @RegionCountries AS TABLE
		(
			CountryId int
		);

		--Get all countries that belongs to user's region.
		INSERT INTO @RegionCountries(CountryId)
		SELECT
			CountryId
		FROM Countries
		WHERE Region = @UserRegion;

		--Get data tha belongs to user's region.
		SELECT * FROM #TempData
		WHERE CountryId IN
		(
			SELECT CountryId FROM @RegionCountries
		);
	END
	ELSE IF(@UserType = 3) --Country admin
	BEGIN
		--Get all data that belongs to user's country.
		SELECT * FROM #TempData
		WHERE CountryId = @CountryId
	END

	IF(@NeedFilterData = 1)
	BEGIN

		SELECT
			'' AS 'TechnicalAreaTable',
			IIF(CTA.CommonTechnicalAreaId IS NULL, TA.Name, CTA.DisplayName) AS 'Name',
			STUFF
			((SELECT ',' + CAST(TA2.TechnicalAreaId AS VARCHAR(10))
			FROM TechnicalAreas TA2
			LEFT JOIN CommonTechnicalAreas CTA2
				ON CTA2.CommonTechnicalAreaId = TA2.CommonTechnicalAreaId
			WHERE IIF(CTA2.CommonTechnicalAreaId IS NULL, TA2.Name, CTA2.DisplayName) = IIF(CTA.CommonTechnicalAreaId IS NULL, TA.Name, CTA.DisplayName)
			FOR XML PATH ('')
				), 1, 1, '') AS 'TechnicalAreaIds'
		FROM TechnicalAreas TA
		LEFT JOIN CommonTechnicalAreas CTA
			ON CTA.CommonTechnicalAreaId = TA.CommonTechnicalAreaId
		WHERE IsActive = 1
		GROUP BY IIF(CTA.CommonTechnicalAreaId IS NULL, TA.Name, CTA.DisplayName)

		SELECT 
			'' AS 'TechnicalAreaIndicatorTable',
			TAI.TechnicalAreaIndicatorId,
			TAI.TechnicalAreaId,
			TAI.Name
		FROM TechnicalAreas TA
		INNER JOIN TechnicalAreaIndicators TAI
			ON TA.TechnicalAreaId = TAI.TechnicalAreaId
		WHERE IsActive = 1
	END

    -- Clean up temporary table
    DROP TABLE IF EXISTS #TempData;
    DROP TABLE IF EXISTS #TechnicalAreaTable;
END
-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <14-08-2023>
-- Description:	<Get initial data to create a new plan.>
-- =============================================

CREATE PROCEDURE sp_GetInitialDataForPlanCreation
	-- Add the parameters for the stored procedure here
	@CountryId int,
	@PlanType int
AS
BEGIN
	
	-- Get all the strategic plans for country.
	SELECT 
		'' AS 'StrategicPlansTable',
		*
	FROM CountryPlans
	WHERE CountryId = @CountryId
	AND (PlanStatusId IN (2, 3, 4)) -- Live, Draft and completed plans only
	AND PlanTypeId = 1 -- Strategic plan.

	-- Get all operational plans for country
	IF (@PlanType = 1)
	BEGIN

		SELECT '' AS 'OperationalPlanTable' WHERE 1 = 2;

	END
	ELSE
	BEGIN

		-- Get all operation plans for country.
		SELECT 
			'' AS 'OperationalPlanTable',
			*
		FROM CountryPlans
		WHERE CountryId = @CountryId
		AND PlanTypeId = 2 -- Operational plan type
		AND (PlanStatusId IN (2, 3)); -- Live and Draft plans only

	END

	-- Get countries custom and all other technical areas from JEE/SPAR
	SELECT 
		'' AS 'TechnicalAreasTable',
		TCA.TechnicalAreaId,
		IIF(CTA.CommonTechnicalAreaId IS NULL, TCA.[Name], CTA.[DisplayName]) AS 'Name',
		TCA.AreaCode,
		TCA.AreaCodeId,
		TCA.SourceId,
		TCA.IsCustomTechnicalArea,
		TCA.CountryId
	FROM TechnicalAreas TCA
	LEFT JOIN CommonTechnicalAreas CTA
		ON CTA.CommonTechnicalAreaId = TCA.CommonTechnicalAreaId
	WHERE (CountryId IS NULL OR CountryId = @CountryId)
	AND IsActive = 1;

	-- Get Technical Area indicators records.

	SELECT 
		'' AS 'TechnicalAreaIndicatorTable',
		TAI.TechnicalAreaIndicatorId,
		TAI.[Name],
		TAI.TechnicalAreaId,
		TAI.IndicatorCodeId,
		TAI.IndicatorCode
	FROM TechnicalAreas TA
	INNER JOIN TechnicalAreaIndicators TAI
		ON TA.TechnicalAreaId = TAI.TechnicalAreaId
	WHERE (CountryId IS NULL OR CountryId = @CountryId)
	AND IsActive = 1;

END
GO

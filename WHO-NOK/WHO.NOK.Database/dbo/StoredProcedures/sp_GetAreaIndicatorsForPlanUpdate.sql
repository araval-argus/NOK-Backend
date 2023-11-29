-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <29-08-2023>
-- Description:	<Get technical areas and indicators for update the plan>
-- =============================================
CREATE PROCEDURE sp_GetAreaIndicatorsForPlanUpdate
	-- Add the parameters for the stored procedure here
	@PlanId int
AS
BEGIN
	
	-- Get the plan assessment type.
	DECLARE @PlanAssessmentType AS int; 
	DECLARE @CountryId AS int;
	
	SELECT TOP 1 
		@PlanAssessmentType = AssessmentTypeId,
		@CountryId = CountryId
	FROM CountryPlans 
	WHERE CountryPlanId = @PlanId;

	DECLARE @PlanIndicators AS TABLE
	(
		TechnicalAreaIndocatorId int,
		IndicatorCode nvarchar(50),
		IndicatorCodeId nvarchar(50)
	);

	INSERT INTO @PlanIndicators(TechnicalAreaIndocatorId, IndicatorCode, IndicatorCodeId)
	SELECT 
		TAI.TechnicalAreaIndicatorId,
		TAI.IndicatorCode,
		TAI.IndicatorCodeId
	FROM TechnicalAreaIndicators TAI
	INNER JOIN CountryPlanIndicators CPI
		ON CPI.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId
		AND CPI.IsDeleted = 0
	WHERE CPI.CountryPlanId = @PlanId;

	DECLARE @AllAssessmentIndicatorsTable AS TABLE
	(
		AutoId int IDENTITY(1, 1),
		TechnicalAreaIndocatorId int,
		IndicatorCode nvarchar(50),
		IndicatorCodeId nvarchar(50)
	);

	INSERT INTO @AllAssessmentIndicatorsTable(TechnicalAreaIndocatorId, IndicatorCode, IndicatorCodeId)
	SELECT 
		TAI.TechnicalAreaIndicatorId,
		TAI.IndicatorCode,
		TAI.IndicatorCodeId
	FROM TechnicalAreaIndicators TAI
	INNER JOIN  TechnicalAreas TA
		ON TA.TechnicalAreaId = TAI.TechnicalAreaId
	WHERE (CountryId IS NULL OR CountryId = @CountryId)
	AND IsActive = 1
	AND (TA.SourceId = @PlanAssessmentType OR TA.CountryId = @CountryId);

	DECLARE @Counter AS int = 1;
	DECLARE @TotalCounts AS int = (SELECT COUNT(1) FROM @AllAssessmentIndicatorsTable);
	DECLARE @IndicatorCode AS nvarchar(50);
	DECLARE @IndicatorCodeId AS nvarchar(50);
	DECLARE @TechnicalAreaIndicatorId int;

	DECLARE @ExistsPlanIndicatoraTable AS TABLE
	(
		TechnicalAreaIndicatorId int
	);

	WHILE (@Counter <= @TotalCounts)
	BEGIN
		
		SELECT
			@TechnicalAreaIndicatorId = TechnicalAreaIndocatorId,
			@IndicatorCode = IndicatorCode,
			@IndicatorCodeId = IndicatorCodeId
		FROM @AllAssessmentIndicatorsTable WHERE AutoId = @Counter;

		IF (EXISTS (SELECT TOP 1 1 FROM @PlanIndicators WHERE TechnicalAreaIndocatorId = @TechnicalAreaIndicatorId))
		BEGIN

			INSERT INTO @ExistsPlanIndicatoraTable(TechnicalAreaIndicatorId)
			SELECT @TechnicalAreaIndicatorId;

		END

		
		SET @Counter = @Counter + 1;
	END
	
	SELECT '' AS 'ExistsIndicatorTable', * FROM @ExistsPlanIndicatoraTable;

	-- Get countries custom and all other technical areas from JEE/SPAR
	SELECT 
		'' AS 'TechnicalAreasTable',
		TCA.TechnicalAreaId,
		IIF(CTA.CommonTechnicalAreaId IS NULL, TCA.[Name], CTA.[DisplayName]) AS 'Name',
		TCA.AreaCode,
		TCA.AreaCodeId,
		TCA.SourceId
	FROM TechnicalAreas TCA
	LEFT JOIN CommonTechnicalAreas CTA
		ON CTA.CommonTechnicalAreaId = TCA.CommonTechnicalAreaId
	WHERE (CountryId IS NULL OR CountryId = @CountryId)
	AND IsActive = 1
	AND (SourceId = @PlanAssessmentType OR CountryId = @CountryId);

	-- Get Technical Area indicators records.

	SELECT 
		'' AS 'TechnicalAreaIndicatorTable',
		TAI.TechnicalAreaIndicatorId,
		TAI.[Name],
		TAI.TechnicalAreaId,
		TAI.IndicatorCodeId,
		TAI.IndicatorCode,
		IIF (CPA.PlanIndicatorId IS NULL, 1, CPA.Score) AS 'Score',
		IIF (CPA.Goal IS NULL, 0, CPA.Goal) AS 'Goal' 
	FROM TechnicalAreas TA
	INNER JOIN TechnicalAreaIndicators TAI
		ON TA.TechnicalAreaId = TAI.TechnicalAreaId
	LEFT JOIN CountryPlanIndicators CPA
		ON CPA.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId
		AND CountryPlanId = @PlanId
		AND CPA.IsDeleted = 0
	WHERE (CountryId IS NULL OR CountryId = @CountryId)
	AND IsActive = 1
	AND (SourceId = @PlanAssessmentType OR CountryId = @CountryId);

END
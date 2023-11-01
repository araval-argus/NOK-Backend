-- =============================================
-- Author:		<Kaif Pipadwala>
-- Create date: <29-08-2023>
-- Description:	<Stored Procedure that is used to get IHR recommendations from CountryPlanId>
-- ============================================

CREATE PROCEDURE sp_GetImportedIHRActionsFromPlanId
    @CountryPlanId INT,
    @IndicatorCodeId NVARCHAR(10) = NULL,
    @IndicatorCode NVARCHAR(10) = NULL
AS
BEGIN

   -- Step -1 priority is planId if data is found return it.
    SELECT
		CPI.Score,
        CPI.Goal,
		CPI.PlanIndicatorId,
		TA.Name AS TechnicalArea,
        TAI.IndicatorCode, --JEE/SPAR Indicator Code and id to match it in table.
        TAI.IndicatorCodeId,
		IHR.IHRRecommendationId,
        IHR.BenchMark,
        IHR.IndicatorId,
        IHR.Actions,
        IHR.TargetScore
    INTO #TempData
    FROM IHRRecommendations IHR
    JOIN CountryPlans CP
        ON CP.CountryPlanId = @CountryPlanId AND CP.IsDeleted = 0
	INNER JOIN CommonIndicatorsMapping CIM
		ON CIM.IndicatorId = IHR.IndicatorId AND CIM.Type = 3 -- IHR Benchmark
	INNER JOIN CommonIndicatorsMapping CIM2
		ON CIM2.CommonIndicatorId = CIM.CommonIndicatorId AND CIM2.Type = CP.AssessmentTypeId
	INNER JOIN TechnicalAreaIndicators TAI
		ON TAI.IndicatorCode = CIM2.IndicatorCode AND TAI.IndicatorCodeId = CIM2.IndicatorId
	INNER JOIN TechnicalAreas TA
		ON TA.TechnicalAreaId = TAI.TechnicalAreaId
	INNER JOIN CountryPlanIndicators CPI
		ON CPI.CountryPlanId = @CountryPlanId AND CPI.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId AND CPI.IsDeleted = 0
    WHERE IHR.CountryPlanId = @CountryPlanId AND IHR.PreviousScore >= CPI.Score AND IHR.TargetScore <= CPI.Goal;

    IF EXISTS(SELECT TOP 1 * FROM #TempData)
    BEGIN

        -- Step 2: Get desired results
        IF @IndicatorCode IS NOT NULL AND @IndicatorCodeId IS NOT NULL
        BEGIN
            SELECT * FROM #TempData WHERE IndicatorCode = @IndicatorCode AND IndicatorCodeId = @IndicatorCodeId;
        END
        ELSE
        BEGIN
            SELECT * FROM #TempData TD;
        END

		-- Clean up temporary table
		DROP TABLE IF EXISTS #TempData;
    END
 	ELSE
	BEGIN
	    -- Step 3: Get data uploaded to global level.
        SELECT
		    CPI.Score,
            CPI.Goal,
		    CPI.PlanIndicatorId,
            TA.Name AS TechnicalArea,
            TAI.IndicatorCode, --JEE/SPAR Indicator Code and id to match it in table.
            TAI.IndicatorCodeId,
            IHR.IHRRecommendationId,
            IHR.BenchMark,
            IHR.IndicatorId,
            IHR.Actions,
            IHR.TargetScore
        INTO #TempData2
        FROM CountryPlanIndicators CPI
        JOIN CountryPlans CP
            ON CP.CountryPlanId = @CountryPlanId AND CP.IsDeleted = 0
        INNER JOIN TechnicalAreaIndicators TAI
            ON CPI.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId
        INNER JOIN TechnicalAreas TA
            ON TA.TechnicalAreaId = TAI.TechnicalAreaId
        INNER JOIN CommonIndicatorsMapping CIM
            ON TAI.IndicatorCode = CIM.IndicatorCode AND TAI.IndicatorCodeId = CIM.IndicatorId AND CIM.Type = CP.AssessmentTypeId
        INNER JOIN CommonIndicatorsMapping CIM2
            ON CIM.CommonIndicatorId = CIM2.CommonIndicatorId AND CIM2.Type = 3
        INNER JOIN IHRRecommendations IHR
            ON CIM2.IndicatorId = IHR.IndicatorId AND IHR.PreviousScore >= CPI.Score AND IHR.TargetScore <= CPI.Goal
        WHERE CPI.CountryPlanId = @CountryPlanId AND CPI.IsDeleted = 0;

            -- Step 4: Get desired results
        IF @IndicatorCode IS NOT NULL AND @IndicatorCodeId IS NOT NULL
        BEGIN
            SELECT * FROM #TempData2 WHERE IndicatorCode = @IndicatorCode AND IndicatorCodeId = @IndicatorCodeId;
        END
        ELSE
        BEGIN
            SELECT * FROM #TempData2 TD;
        END

		-- Step 5: Clean up temporary table
		DROP TABLE IF EXISTS #TempData2;
	END
END
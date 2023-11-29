-- =============================================
-- Author:		<Kaif Pipadwala>
-- Create date: <29-08-2023>
-- Description:	<Stored Procedure that is used to get NBW recommendations from CountryPlanId>
-- ============================================

CREATE PROCEDURE [dbo].[sp_GetImportedNBWActionsFromPlanId]
    @CountryPlanId INT,
    @IndicatorCodeId NVARCHAR(10) = NULL,
    @IndicatorCode NVARCHAR(10) = NULL
AS
BEGIN

    -- -- Step -1 priority is planId if data is found return it.
    -- SELECT
	-- 	CPI.Score,
    --     CPI.Goal,
    --     NBW.TechnicalArea AS TechnicalArea,
	-- 	NBW.NBWRecommendationId,
    --     NBW.IndicatorId,
	-- 	NBW.IndicatorCode,
	-- 	NBW.Feasibility,
	-- 	NBW.Impact,
	-- 	NBW.Objective,
	-- 	NBW.StrategicAction,
	-- 	NBW.DetailedActivity,
	-- 	NBW.StartDate,
	-- 	NBW.EndDate,
	-- 	NBW.ResponsibleAuthority
    -- INTO #TempData
    -- FROM NBWRecommendations NBW
    -- JOIN CountryPlans CP
    --     ON CP.CountryPlanId = @CountryPlanId
	-- INNER JOIN CommonIndicatorsMapping CIM
	-- 	ON CIM.IndicatorId = NBW.IndicatorId AND NBW.IndicatorCode = CIM.IndicatorCode AND CIM.Type = 4 -- NBW Recommendation
	-- INNER JOIN CommonIndicatorsMapping CIM2
	-- 	ON CIM2.CommonIndicatorId = CIM.CommonIndicatorId AND CIM2.Type = CP.AssessmentTypeId
	-- INNER JOIN TechnicalAreaIndicators TAI
	-- 	ON TAI.IndicatorCode = CIM2.IndicatorCode AND TAI.IndicatorCodeId = CIM2.IndicatorId
	-- INNER JOIN TechnicalAreas TA
	-- 	ON TA.TechnicalAreaId = TAI.TechnicalAreaId
	-- INNER JOIN CountryPlanIndicators CPI
	-- 	ON CPI.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId AND CPI.IsDeleted = 0
    -- WHERE NBW.CountryPlanId = @CountryPlanId;

    -- IF EXISTS(SELECT TOP 1 * FROM #TempData)
    -- BEGIN
    --     -- Step 3: Get desired results
    --     IF @IndicatorCode IS NOT NULL AND @IndicatorCodeId IS NOT NULL
    --     BEGIN
    --         SELECT * FROM #TempData WHERE IndicatorCode = @IndicatorCode AND IndicatorId = @IndicatorCodeId;
    --     END
    --     ELSE
    --     BEGIN
    --         SELECT * FROM #TempData TD;
    --     END

	-- 	-- Clean up temporary table
	-- 	DROP TABLE IF EXISTS #TempData;
    -- END
	-- ELSE
	BEGIN
	    SELECT
            CPI.Score,
            CPI.Goal,
			CPI.PlanIndicatorId,
            TA.Name AS TechnicalArea,
			TAI.Name AS IndicatorName,
            NBW.NBWRecommendationId,
            NBW.IndicatorId,
            NBW.IndicatorCode,
            NBW.Feasibility,
            NBW.Impact,
            NBW.Objective,
            NBW.StrategicAction,
            NBW.DetailedActivity,
            NBW.StartDate,
            NBW.EndDate,
            NBW.ResponsibleAuthority
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
            ON CIM.CommonIndicatorId = CIM2.CommonIndicatorId AND CIM2.Type = 4 -- nbw recommendations
        INNER JOIN NBWRecommendations NBW
            ON CIM2.IndicatorId = NBW.IndicatorId AND NBW.IndicatorCode = CIM2.IndicatorCode
        WHERE CPI.IsDeleted = 0 AND CPI.CountryPlanId = @CountryPlanId AND NBW.CountryId = CP.CountryId;

        -- Step 3: Get desired results
        IF @IndicatorCode IS NOT NULL AND @IndicatorCodeId IS NOT NULL
        BEGIN
            SELECT * FROM #TempData2 WHERE IndicatorCode = @IndicatorCode AND IndicatorId = @IndicatorCodeId;
        END;
        ELSE
        BEGIN
            SELECT * FROM #TempData2 TD;
        END;

        -- Clean up temporary table
        DROP TABLE IF EXISTS #TempData2;
    END
END
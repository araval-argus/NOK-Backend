-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <23-08-2023>
-- Description:	<Get complete plan details.>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetPlanDetails] 
	-- Add the parameters for the stored procedure here
	@PlanId int,
	@NeedCountryDetails bit = 1
AS
BEGIN

	DECLARE @PlanType AS int; 
	DECLARE @PlanStartDate AS datetime;
	DECLARE @PlanEndDate AS datetime
	
	SELECT TOP 1 
		@PlanType = PlanTypeId,
		@PlanStartDate = PlanStartDate,
		@PlanEndDate = PlanEndDate
	FROM CountryPlans 
	WHERE CountryPlanId = @PlanId
	AND IsDeleted = 0

	DECLARE @StrategicActionCost AS float = 0;
	DECLARE @DetailedActivityCost AS float = 0;
	DECLARE @StrategicActionCounts AS int = 0;
	DECLARE @DetailedActivityCounts AS int = 0;

	IF (@NeedCountryDetails = 1)
	BEGIN

		-- Get Plan overall details.
		SELECT 
			'' AS 'CountryPlanTable',
			C.[Name],
			CP.*,
			CASE WHEN EXISTS (SELECT TOP 1 1 FROM DownloadPlanHistory WHERE CountryPlanId = @PlanId) THEN CAST(1 AS bit)
				 ELSE CAST(0 AS bit) END AS 'IsPlanDownloaded'
		FROM CountryPlans CP
		INNER JOIN Countries C
			ON C.CountryId = CP.CountryId
		WHERE CP.CountryPlanId = @PlanId
		AND CP.IsDeleted = 0;

	END
	ELSE
	BEGIN
		SELECT '' AS 'CountryPlanTable'
	END

	-- Get all the indicators that used in plan.
	DECLARE @AreaIndicators AS TABLE
	(
		IndicatorId int,
		PlanIndicatorId int
	);

	-- Add plan selected indicators into the table.
	INSERT INTO @AreaIndicators(IndicatorId, PlanIndicatorId)
	SELECT 
		TechnicalAreaIndicatorId,
		PlanIndicatorId
	FROM CountryPlanIndicators
	WHERE CountryPlanId = @PlanId
	AND IsDeleted = 0;

	-- Get the technical areas that has been used in the plan.
	SELECT
		'' AS 'TechnicalAreaTable',
		*	
	FROM (
	SELECT 
		TA.*,
		ROW_NUMBER() OVER (PARTITION BY TA.TechnicalAreaId ORDER BY TA.TechnicalAreaId) AS RN
	FROM TechnicalAreas TA
	INNER JOIN TechnicalAreaIndicators TAI
		ON TAI.TechnicalAreaId = TA.TechnicalAreaId
	WHERE TAI.TechnicalAreaIndicatorId IN
	(
		SELECT IndicatorId FROM @AreaIndicators
	)) AS T
	WHERE T.RN = 1;

	-- Get the technical area indicator that has been used in the plan.
	SELECT 
		'' AS 'TechnicalAreaIndicatorTable',
		TAI.*,
		CPI.Goal,
		CPI.Score,
		CPI.PlanIndicatorId
	FROM TechnicalAreaIndicators TAI
	INNER JOIN CountryPlanIndicators CPI
		ON CPI.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId
		AND CPI.IsDeleted = 0
	WHERE TAI.TechnicalAreaIndicatorId IN
	(
		SELECT IndicatorId FROM @AreaIndicators
	)
	AND CPI.PlanIndicatorId IN
	(
		SELECT PlanIndicatorId FROM @AreaIndicators
	)
	AND CPI.CountryPlanId = @PlanId;

	-- Get the strategic actions that has been used 
	SELECT 
		'' AS 'StrategicActionsTable',
		SA.[StrategicActionId],
		SA.[PlanIndicatorId],
		SA.[Objective],
		SA.[Action],
		SA.[Feasibility],
		SA.[Impact],
		SA.[Priority],
		SA.[ImplementationStatus],
		SA.[ResponsibleAuthority],
		SA.[EstimatedCost],
		SA.[Comments],
		SA.[Source],
		IIF (CPI.Goal IS NULL, SA.[Goal], CPI.Goal) AS 'Goal',
		IIF (CPI.Score IS NULL, SA.[Score], CPI.Score) AS 'Score',
		SA.[CreatedAt],
		SA.[CreatedBy],
		SA.[LastUpdatedAt],
		SA.[LastUpdatedBy],
		SA.[IsDeleted],
		CPI.TechnicalAreaIndicatorId
	INTO #StrategicActionsTable
	FROM StrategicActions SA
	INNER JOIN CountryPlanIndicators CPI
		ON CPI.PlanIndicatorId = SA.PlanIndicatorId
		AND CPI.IsDeleted = 0
	WHERE SA.IsDeleted = 0 
	AND SA.ReferenceId IS NULL 
	AND	CPI.PlanIndicatorId IN
	(
		SELECT PlanIndicatorId FROM @AreaIndicators
	)
	AND CPI.CountryPlanId = @PlanId;

	SET @StrategicActionCost = ISNULL((SELECT SUM(EstimatedCost) FROM #StrategicActionsTable), 0);
	SET @StrategicActionCounts = ISNULL((SELECT COUNT(1) FROM #StrategicActionsTable), 0);

	IF (@PlanType = 2)
	BEGIN		

		-- Get detailed activities for the plan.
		SELECT
			'' AS 'DetailedActivitiesTable',
			 DA.[DetailedActivityId]
			,DA.[ReferenceId]
			,DA.[StrategicActionId]
			,DA.[Description]
			,IIF (DA.[StartDate] IS NULL, @PlanStartDate, DA.[StartDate])  AS 'StartDate'
			,IIF (DA.[EndDate] IS NULL, @PlanEndDate, DA.[EndDate]) AS 'EndDate'
			,DA.[ImplementationStatus]
			,DA.[Feasibility]
			,DA.[Impact]
			,DA.[Priority]
			,DA.[ActivityTypeIds]
			,DA.[Source]
			,DA.[RiskName]
			,DA.[RiskLevel]
			,DA.[Deadline]
			,DA.[Responsible]
			,DA.[ResponsibleAuthority]
			,DA.[InstituteId]
			,DA.[CostAssumptions]
			,DA.[EstimatedCost]
			,DA.[FundAvailability]
			,DA.[EstimatedBudgetSource]
			,DA.[ExistingBudget]
			,DA.[FinancialGap]
			,DA.[NeedTechnicalAssistance]
			,DA.[NeedFinancialAssistance]
			,DA.[DonorContribution]
			,DA.[Donor]
			,DA.[ActualCost]
			,DA.[Comments]
			,DA.[CreatedAt]
			,DA.[CreatedBy]
			,DA.[LastUpdatedAt]
			,DA.[LastUpdatedBy]
			,DA.[IsDeleted]
			,STUFF(
				(
					SELECT ',' + DAT.Activity
					FROM STRING_SPLIT(DA.ActivityTypeIds, ',') AS DT
					JOIN DetailedActivityTypes DAT ON DAT.ActivityTypeId = CAST(DT.value AS INT)
					ORDER BY CHARINDEX(',' + CAST(DT.value AS VARCHAR) + ',', ',' + DA.ActivityTypeIds + ',')
					FOR XML PATH('')
				), 1, 1, ''
			) AS 'Activity'
		INTO #DetailedActivityTable 
		FROM 
		DetailedActivities DA
		WHERE IsDeleted = 0 
		AND [ReferenceId] IS NULL 
		AND	StrategicActionId IN
		(
			SELECT StrategicActionId FROM #StrategicActionsTable
		);		

		SET @DetailedActivityCost = ISNULL((SELECT SUM(EstimatedCost) FROM #DetailedActivityTable), 0);
		SET @DetailedActivityCounts = ISNULL((SELECT COUNT(1) FROM #DetailedActivityTable), 0);
		SET @StrategicActionCost = 0;

		SELECT * FROM #DetailedActivityTable;
		DROP TABLE IF EXISTS #DetailedActivityTable;

	END
	ELSE 
	BEGIN
		SELECT '' AS 'DetailedActivitiesTable' FROM DetailedActivities WHERE 1 = 2;

	END

	SELECT * FROM #StrategicActionsTable;	
	DROP TABLE IF EXISTS #StrategicActionsTable;
	
	SELECT 
		'' AS 'PlanStatsTable', 
		(@StrategicActionCost + @DetailedActivityCost) AS 'Cost', 
		@StrategicActionCounts AS 'StrategicActionCounts',
		@DetailedActivityCounts AS 'DetailedActivityCounts';

END
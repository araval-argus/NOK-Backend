-- =============================================
-- Author:		<Kaif Pipadwala>
-- Create date: <14-09-2023>
-- Description:	<Gets the details of a plan>
-- =============================================

CREATE PROCEDURE [dbo].[sp_DetailsForDownloadPlan]
  @CountryPlanId INT,
  @UserId INT
AS
BEGIN

	DECLARE @PlanTypeId INT;

	-- Checking the plan type.
	SELECT @PlanTypeId = [PlanTypeId]
	FROM CountryPlans
	WHERE CountryPlanId = @CountryPlanId

	-- For checking the plan whether it is strategic or operational.
	SELECT '' AS 'PlanType',
		@PlanTypeId AS [Value];

	-- For Strategic plan.
	IF (@PlanTypeId = 1)
	BEGIN

		-- strategic plan details.
		SELECT
			'' AS 'PlanDetails',
			TA.Name AS [TechnicalArea],
			TAI.Name AS [Indicator],
			dbo.EncryptionFunction(CPI.PlanIndicatorId, 'YOUCANNOTACCESSTHISKEY') AS PlanIndicatorId,
			CPI.Score AS [IndicatorScore],
			CPI.Goal AS [IndicatorGoal],
			dbo.EncryptionFunction(SA.StrategicActionId, 'YOUCANNOTACCESSTHISKEY') AS [StrategicAction],
			SA.Action,
			SA.Objective,
			SA.ResponsibleAuthority AS [StrategicActionResponsibleAuthority],
			SA.EstimatedCost AS [StrategicActionEstimatedCost],
			SA.Comments AS [StrategicActionComments],
			SA.Source AS [StrategicActionSource],
			SA.Score AS [StrategicActionScore],
			SA.Impact AS [StrategicActionImpact],
			SA.Feasibility AS [StrategicActionFeasibility],
			SA.Priority AS [StrategicActionPriority],
			SA.Impact AS [StrategicActionImpact],
			SA.Goal AS [StrategicActionGoal]
		FROM StrategicActions SA
		JOIN CountryPlanIndicators CPI
			ON SA.PlanIndicatorId = CPI.PlanIndicatorId
		JOIN TechnicalAreaIndicators TAI
			ON CPI.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId
		JOIN TechnicalAreas TA
			ON TAI.TechnicalAreaId = TA.TechnicalAreaId
		JOIN CountryPlans CP
			ON CP.CountryPlanId = CPI.CountryPlanId
		WHERE CP.CountryPlanId = @CountryPlanId
		ORDER BY TA.TechnicalAreaId,
		TAI.TechnicalAreaIndicatorId,
		SA.StrategicActionId

	END

	-- For Operational plan.
	ELSE 
	BEGIN

		-- operational plan details.
		SELECT
			'' AS 'PlanDetails',
			TA.Name AS [TechnicalArea],
			TAI.Name AS [Indicator],
			dbo.EncryptionFunction(CPI.PlanIndicatorId, 'YOUCANNOTACCESSTHISKEY') AS PlanIndicatorId,
			CPI.Score AS [IndicatorScore],
			CPI.Goal AS [IndicatorGoal],
			dbo.EncryptionFunction(SA.StrategicActionId, 'YOUCANNOTACCESSTHISKEY') AS [StrategicAction],
			SA.Action,
			SA.Objective,
			SA.Impact AS [StrategicActionImpact],
			SA.Feasibility AS [StrategicActionFeasibility],
			SA.Priority AS [StrategicActionPriority],
			SA.ResponsibleAuthority AS [StrategicActionResponsibleAuthority],
			SA.EstimatedCost AS [StrategicActionEstimatedCost],
			SA.Comments AS [StrategicActionComments],
			SA.Source AS [StrategicActionSource],
			SA.Score AS [StrategicActionScore],
			SA.Goal AS [StrategicActionGoal],
			dbo.EncryptionFunction(DA.DetailedActivityId, 'YOUCANNOTACCESSTHISKEY') AS [DetailedActivity],
			DA.Description AS [Description],

			CASE
			WHEN DA.StartDate IS NULL THEN CP.PlanStartDate
			ELSE DA.StartDate
			END AS [StartDate],

			CASE
			WHEN DA.StartDate IS NULL THEN CP.PlanEndDate
			ELSE DA.EndDate
			END AS [EndDate],

			CASE
			WHEN DA.ImplementationStatus = 1 THEN 'Not Started'
			WHEN DA.ImplementationStatus = 2 THEN 'Just Started'
			WHEN DA.ImplementationStatus = 3 THEN 'On Going'
			WHEN DA.ImplementationStatus = 4 THEN 'Advanced Stage'
			WHEN DA.ImplementationStatus = 5 THEN 'Completed'
			END AS [ImplementationStatus],

			DA.Source AS [DetailedActivitySource],
			DA.Impact AS [DetailedActivityImpact],
			DA.Feasibility AS [DetailedActivityFeasibility],
			DA.Priority AS [DetailedActivityPriority],
			DA.RiskName AS [RiskName],
			DA.Deadline AS [Deadline],
			STUFF(
				(
					SELECT ',' + DAT.Activity
					FROM STRING_SPLIT(DA.ActivityTypeIds, ',') AS DT
					JOIN DetailedActivityTypes DAT ON DAT.ActivityTypeId = CAST(DT.value AS INT)
					ORDER BY CHARINDEX(',' + CAST(DT.value AS VARCHAR) + ',', ',' + DA.ActivityTypeIds + ',')
					FOR XML PATH('')
				), 1, 1, ''
			) AS [Activity],
			DA.Responsible AS [Responsible],
			DA.ResponsibleAuthority AS [DetailedActivityResponsibleAuthority],
			DA.CostAssumptions AS [CostAssumptions],
			DA.EstimatedCost AS [DetailedActivityEstimatedCost],

			CASE 
			WHEN DA.FundAvailability = 1 THEN 'TRUE'
			ELSE 'FALSE'
			END AS [FundAvailability],

			DA.EstimatedBudgetSource AS [EstimatedBudgetSource],
			DA.ExistingBudget AS [ExistingBudget],
			DA.FinancialGap AS [FinancialGap],

			CASE 
			WHEN DA.NeedTechnicalAssistance = 1 THEN 'TRUE'
			ELSE 'FALSE'
			END AS [TechnicalAssistance],

			CASE 
			WHEN DA.NeedFinancialAssistance = 1 THEN 'TRUE'
			ELSE 'FALSE'
			END AS [FinancialAssistance],
			DA.DonorContribution AS [DonorContribution],
			DA.Donor,
			DA.ActualCost AS [ActualCost],
			DA.Comments AS [DetailedActivityComments]
		FROM StrategicActions SA
		JOIN DetailedActivities DA
			ON SA.StrategicActionId = DA.StrategicActionId
		JOIN CountryPlanIndicators CPI
			ON SA.PlanIndicatorId = CPI.PlanIndicatorId
		JOIN TechnicalAreaIndicators TAI
			ON CPI.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId
		JOIN TechnicalAreas TA
			ON TAI.TechnicalAreaId = TA.TechnicalAreaId
		JOIN CountryPlans CP
			ON CP.CountryPlanId = CPI.CountryPlanId
		WHERE CP.CountryPlanId = @CountryPlanId
		ORDER BY TA.TechnicalAreaId,
		TAI.TechnicalAreaIndicatorId,
		SA.StrategicActionId,
		DA.DetailedActivityId

	END

	BEGIN TRY
		BEGIN TRANSACTION DetailsForDownloadPlan

		IF NOT EXISTS(SELECT TOP 1 1 FROM DownloadPlanHistory WHERE CountryPlanId = @CountryPlanId)
		BEGIN	
			INSERT INTO DownloadPlanHistory (CountryPlanId, UserId, CreatedAt, CreatedBy, LastUpdatedAt, LastUpdatedBy)
			VALUES (@CountryPlanId, @UserId, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId)
		END

		COMMIT TRANSACTION DetailsForDownloadPlan

	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
        BEGIN

            ROLLBACK TRANSACTION DetailsForDownloadPlan;

        END

        INSERT INTO SQLErrorLogs(ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage,ErrorDate)
        SELECT ERROR_SEVERITY(),ERROR_STATE(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),GETDATE(); 
	END CATCH

END
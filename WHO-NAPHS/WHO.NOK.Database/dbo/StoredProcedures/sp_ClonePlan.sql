-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <25-09-2023>
-- Description:	<Copy Plan details and create a new plan>
-- =============================================
CREATE PROCEDURE sp_ClonePlan
	-- Add the parameters for the stored procedure here
	@PlanId AS int,
	@StartDate AS datetime,
	@EndDate AS datetime,
	@IncludeStrategicActions AS bit = 0,
	@IncludePriorities AS bit = 0,
	@IncludeEstimatedBudget AS bit = 0,
	@IncludeResponsibleAuthority AS bit = 0,
	@IncludeComments AS bit = 0,
	@UserId AS int
AS
BEGIN

	DECLARE @NewPlanId AS int = 0;

	BEGIN TRY
		
		BEGIN TRANSACTION ClonePlan;

		DECLARE @PlanType AS int;
		DECLARE @CountryId AS int;
		DECLARE @PlanStartDate AS datetime;
		DECLARE @PlanEndDate AS datetime;
		DECLARE @CountryISOCode AS nvarchar(10);

		DECLARE @NewIdTable AS TABLE 
		(
			Id int
		);

		-- Get values from the country plan table.
		SELECT	
			@PlanType = PlanTypeId,
			@PlanStartDate = PlanStartDate,
			@PlanEndDate = PlanEndDate,
			@CountryId = CountryId
		FROM CountryPlans 
		WHERE CountryPlanId = @PlanId;

		-- Get the country ISO code.
		SET @CountryISOCode = (SELECT TOP 1 ISOCode FROM Countries WHERE CountryId = @CountryId)

		-- Add new plan SP or OP.
		INSERT INTO [dbo].[CountryPlans]
			   ([Description]
			   ,[StrategicPlanId]
			   ,[PlanTypeId]
			   ,[CountryId]
			   ,[PlanStartDate]
			   ,[PlanEndDate]
			   ,[AssessmentTypeId]
			   ,[PlanStatusId]
			   ,[PlanStageId]
			   ,[PlanCode]
			   ,[SendReviewReminder]
			   ,[AdvancedDaysForReviewReminder]
			   ,[CountryISOCode]
			   ,[HasOfficiallyApprovedPlan]
			   ,[VisibleToAnotherCountries]
			   ,[CountryPlanFrequency]
			   ,[CreatedAt]
			   ,[CreatedBy]
			   ,[LastUpdatedAt]
			   ,[LastUpdatedBy]
			   ,[IsDeleted])
		OUTPUT inserted.CountryPlanId INTO @NewIdTable(Id)
		SELECT	
				[Description]
			   ,[StrategicPlanId]
			   ,[PlanTypeId]
			   ,[CountryId]
			   ,@StartDate
			   ,@EndDate
			   ,[AssessmentTypeId]
			   ,3 --Draft
			   ,1 -- Not Started
			   ,CASE WHEN PlanTypeId = 1 THEN 
					CONCAT(@CountryISOCode, '-SP-', YEAR(@PlanStartDate), FORMAT(MONTH(@PlanStartDate), '00'), '-', YEAR(@PlanEndDate), FORMAT(MONTH(@PlanEndDate), '00'))
					ELSE 
					CONCAT(@CountryISOCode, '-OP-', YEAR(@PlanStartDate), FORMAT(MONTH(@PlanStartDate), '00'), '-', YEAR(@PlanEndDate), FORMAT(MONTH(@PlanEndDate), '00'))
				END
			   ,[SendReviewReminder]
			   ,[AdvancedDaysForReviewReminder]
			   ,@CountryISOCode
			   ,0
			   ,0
			   ,NULL
			   ,GETUTCDATE()
			   ,@UserId
			   ,GETUTCDATE()
			   ,@UserId
			   ,0
		FROM CountryPlans 
		WHERE CountryPlanId = @PlanId;

		SET @NewPlanId = ISNULL((SELECT TOP 1 Id FROM @NewIdTable), 0);

		IF (@NewPlanId > 0)
		BEGIN
		
			-- Declare table to store the old plan indictors.
			DECLARE @PlanIndicatorTable AS TABLE
			(
				AutoId int IDENTITY(1, 1),
				OldPlanIndicatorId int,
				NewPlanIndicatorId int
			);

			-- Add old plans indicator into the temp table.
			INSERT INTO @PlanIndicatorTable(OldPlanIndicatorId)
			SELECT
				PlanIndicatorId
			FROM CountryPlanIndicators
			WHERE CountryPlanId = @PlanId
			AND IsDeleted = 0;

			-- #Plan Indicators Start

			DECLARE @TotalPlanIndicators AS int = ISNULL((SELECT COUNT(1) FROM @PlanIndicatorTable), 0);
			DECLARE @Counter AS int = 1;
			DECLARE @OldPlanIndicatorId AS int;

			WHILE (@Counter <= @TotalPlanIndicators)
			BEGIN
			
				-- Delete from the new id table
				DELETE FROM @NewIdTable;

				SET @OldPlanIndicatorId = (SELECT TOP 1 OldPlanIndicatorId FROM @PlanIndicatorTable WHERE AutoId = @Counter);

				INSERT INTO [dbo].[CountryPlanIndicators]
				   ([TechnicalAreaIndicatorId]
				   ,[CountryPlanId]
				   ,[Score]
				   ,[Goal]
				   ,[IsDeleted]
				   ,[CreatedAt]
				   ,[CreatedBy]
				   ,[LastUpdatedAt]
				   ,[LastUpdatedBy])
				OUTPUT inserted.PlanIndicatorId INTO @NewIdTable(Id)
				SELECT 
					[TechnicalAreaIndicatorId]
				   ,@NewPlanId
				   ,[Score]
				   ,[Goal]
				   ,[IsDeleted]
				   ,GETUTCDATE()
				   ,@UserId
				   ,GETUTCDATE()
				   ,@UserId
				FROM CountryPlanIndicators
				WHERE PlanIndicatorId = @OldPlanIndicatorId;

				DECLARE @NewPlanIndicatorId AS int = ISNULL((SELECT TOP 1 Id FROM @NewIdTable), 0);

				IF (@NewPlanIndicatorId > 0)
				BEGIN
				
					-- Update the plan indicator temp table and update the new plan indicator to it.
					UPDATE @PlanIndicatorTable
					SET NewPlanIndicatorId = @NewPlanIndicatorId
					WHERE AutoId = @Counter
					AND OldPlanIndicatorId = @OldPlanIndicatorId; 

				END

				SET @Counter = @Counter + 1;
			END

			-- #Plan Indicators End

			-- #Strategic Action Start

			IF (@IncludeStrategicActions  = 1)
			BEGIN

				-- Declare strategic actions table for the plan.
				DECLARE @StrategicActionsTable AS TABLE
				(
					AutoId int IDENTITY(1, 1),
					OldStrategicActionId int,
					NewStrategicActionId int
				);

				-- Store records from the old plan's strategic action. 
				INSERT INTO @StrategicActionsTable(OldStrategicActionId)
				SELECT 
					StrategicActionId
				FROM StrategicActions
				WHERE PlanIndicatorId IN
				(
					SELECT OldPlanIndicatorId FROM @PlanIndicatorTable
				);

				DECLARE @TotalStrategicActions AS int = ISNULL((SELECT COUNT(1) FROM @StrategicActionsTable), 0);
				SET @Counter = 1;
				DECLARE @OldStrategicActionId AS int;

				WHILE (@Counter <= @TotalStrategicActions)
				BEGIN
			
					-- Delete from the new ids table.
					DELETE FROM @NewIdTable;

					SET @OldStrategicActionId = (SELECT TOP 1 OldStrategicActionId FROM @StrategicActionsTable WHERE AutoId = @Counter)

					-- Add new strategic action for new plan.
					INSERT INTO [dbo].[StrategicActions]
					   ([PlanIndicatorId]
					   ,[Objective]
					   ,[Action]
					   ,[Feasibility]
					   ,[Impact]
					   ,[Priority]
					   ,[ResponsibleAuthority]
					   ,[EstimatedCost]
					   ,[Comments]
					   ,[Source]
					   ,[Score]
					   ,[Goal]
					   ,[ImplementationStatus]
					   ,[CreatedAt]
					   ,[CreatedBy]
					   ,[LastUpdatedAt]
					   ,[LastUpdatedBy]
					   ,[IsDeleted])
					OUTPUT inserted.StrategicActionId INTO @NewIdTable(Id)
					SELECT 
						PIT.NewPlanIndicatorId
					   ,[Objective]
					   ,[Action]
					   ,IIF(@IncludePriorities = 1, [Feasibility], 2) -- Medium
					   ,IIF(@IncludePriorities = 1, [Impact], 2) -- Medium
					   ,IIF(@IncludePriorities = 1, [Priority], 3) -- Medium
					   ,IIF(@IncludeResponsibleAuthority = 1, [ResponsibleAuthority], NULL)
					   ,IIF(@IncludeEstimatedBudget = 1, [EstimatedCost], 0)
					   ,IIF(@IncludeComments = 1, [Comments], NULL)
					   ,[Source]
					   ,[Score]
					   ,[Goal]
					   ,1
					   ,GETUTCDATE()
					   ,@UserId
					   ,GETUTCDATE()
					   ,@UserId
					   ,0
					FROM StrategicActions SA
					INNER JOIN @PlanIndicatorTable PIT
						ON PIT.OldPlanIndicatorId = SA.PlanIndicatorId
					WHERE StrategicActionId = @OldStrategicActionId
					AND SA.IsDeleted = 0;

					DECLARE @NewStrategicActionId AS int = ISNULL((SELECT TOP 1 Id FROM @NewIdTable), 0);

					IF (@NewStrategicActionId > 0)
					BEGIN

						-- Update the strategic action temp table.
						UPDATE @StrategicActionsTable
						SET NewStrategicActionId = @NewStrategicActionId
						WHERE AutoId = @Counter
						AND OldStrategicActionId = @OldStrategicActionId;	

					END

					SET @Counter =  @Counter + 1;
				END

				-- #Strategic Action End
		
				-- If the plan is operational plan in that case we will add operational actions as well.
				IF (@PlanType = 2)
				BEGIN

					INSERT INTO [dbo].[DetailedActivities]
						   ([StrategicActionId]
						   ,[Description]
						   ,[StartDate]
						   ,[EndDate]
						   ,[ImplementationStatus]
						   ,[Feasibility]
						   ,[Impact]
						   ,[Priority]
						   ,[Source]
						   ,[RiskName]
						   ,[RiskLevel]
						   ,[Deadline]
						   ,[Responsible]
						   ,[ResponsibleAuthority]
						   ,[InstituteId]
						   ,[CostAssumptions]
						   ,[EstimatedCost]
						   ,[FundAvailability]
						   ,[EstimatedBudgetSource]
						   ,[ExistingBudget]
						   ,[FinancialGap]
						   ,[NeedTechnicalAssistance]
						   ,[NeedFinancialAssistance]
						   ,[DonorContribution]
						   ,[Donor]
						   ,[ActualCost]
						   ,[Comments]
						   ,[ActivityTypeIds]
						   ,[CreatedAt]
						   ,[CreatedBy]
						   ,[LastUpdatedAt]
						   ,[LastUpdatedBy]
						   ,[IsDeleted])
						SELECT 
							SAT.NewStrategicActionId
						   ,[Description]
						   ,NULL
						   ,NULL
						   ,1
						   ,IIF(@IncludePriorities = 1, [Feasibility], 2) -- Medium
						   ,IIF(@IncludePriorities = 1, [Impact], 2) -- Medium
						   ,IIF(@IncludePriorities = 1, [Priority], 3) -- Medium
						   ,[Source]
						   ,[RiskName]
						   ,[RiskLevel]
						   ,[Deadline]
						   ,[Responsible]
						   ,IIF(@IncludeResponsibleAuthority = 1, [ResponsibleAuthority], NULL)
						   ,[InstituteId]
						   ,[CostAssumptions]
						   ,IIF(@IncludeEstimatedBudget = 1, [EstimatedCost], 0)
						   ,[FundAvailability]
						   ,[EstimatedBudgetSource]
						   ,[ExistingBudget]
						   ,[FinancialGap]
						   ,[NeedTechnicalAssistance]
						   ,[NeedFinancialAssistance]
						   ,[DonorContribution]
						   ,[Donor]
						   ,[ActualCost]
						   ,IIF(@IncludeComments = 1, [Comments], NULL)
						   ,[ActivityTypeIds]
						   ,GETUTCDATE()
						   ,@UserId
						   ,GETUTCDATE()
						   ,@UserId
						   ,0
						FROM DetailedActivities DA
						INNER JOIN @StrategicActionsTable SAT
							ON SAT.OldStrategicActionId = DA.StrategicActionId
						WHERE DA.IsDeleted = 0;
				END
			END

			END		

		COMMIT TRANSACTION ClonePlan;

	END TRY
	BEGIN CATCH

		IF (@@TRANCOUNT > 0)
		BEGIN

			ROLLBACK TRANSACTION ClonePlan;

		END

		INSERT INTO SQLErrorLogs(ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage,ErrorDate)
        SELECT ERROR_SEVERITY(),ERROR_STATE(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),GETDATE(); 

		SET @NewPlanId = 0;

	END CATCH

	SELECT CAST(@NewPlanId AS int) AS 'NewPlanId';
END

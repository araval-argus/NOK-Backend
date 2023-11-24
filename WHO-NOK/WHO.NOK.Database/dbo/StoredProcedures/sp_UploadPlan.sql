-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <18-10-2023>
-- Description:	<Upload Plan>
-- =============================================
CREATE PROCEDURE sp_UploadPlan
	-- Add the parameters for the stored procedure here
	@PlanId int,
	@UserId int,
	@PlanDetails nvarchar(MAX)
AS
BEGIN
	
	DECLARE @PlanType AS int;
	DECLARE @AssessmentType AS int;
	DECLARE @CountryId AS int;
	DECLARE @PlanStartDate AS datetime;
	DECLARE @PlanEndDate AS datetime;

	SELECT 
		@PlanType = PlanTypeId,
		@AssessmentType = AssessmentTypeId,
		@CountryId = CountryId,
		@PlanStartDate = PlanStartDate,
		@PlanEndDate = PlanEndDate
	FROM CountryPlans WHERE CountryPlanId = @PlanId;

	-- Declare the row status table
	DECLARE @RowStatusTable AS TABLE
	(
		PlanIndicatorId nvarchar(MAX),
		RowStatus nvarchar(MAX),
		IsSuccess bit
	)

	-- Declare the plan details table.
	DECLARE @PlanDetailsTable AS TABLE
	(
		AutoId int IDENTITY(1, 1),
		TechnicalArea nvarchar(MAX),
		Indicator nvarchar(MAX),
		IndicatorScore int,
		IndicatorGoal int,
		StrategicAction nvarchar(MAX),
		[Action] nvarchar(MAX),
		Objective nvarchar(MAX),
		StrategicActionResponsibleAuthority nvarchar(MAX),
		StrategicActionEstimatedCost float,
		Comments nvarchar(MAX),
		StrategicActionSource int,
		StrategicActionScore int,
		StrategicActionGoal int,
		DetailedActivity nvarchar(MAX),
		[Description] nvarchar(MAX),
		StartDate datetime,
		EndDate datetime,
		ImplementationStatus int,
		DetailedActivitySource int,
		RiskName nvarchar(MAX),
		Deadline nvarchar(MAX),
		Activity nvarchar(MAX),
		Responsible nvarchar(MAX),
		DetailedActivityResponsibleAuthority nvarchar(MAX),
		CostAssumptions nvarchar(MAX),
		DetailedActivityEstimatedCost float,
		FundAvailability bit,
		EstimatedBudgetSource nvarchar(MAX),
		ExistingBudget float,
		FinancialGap float,
		TechnicalAssistance bit,
		FinancialAssistance bit,
		DonorContribution float,
		Donor nvarchar(MAX),
		ActualCost float,
		DetailedActivityComments nvarchar(MAX),
		PlanIndicatorId int,
		StrategicActionFeasibility int,
		StrategicActionImpact int,
		DetailedActivityFeasibility int,
		DetailedActivityImpact int,
		PlanIndicatorIdStr nvarchar(MAX) 
	);

	-- Add data into temp table fron the passes JSON object
	INSERT INTO @PlanDetailsTable(
		TechnicalArea,
		Indicator,
		IndicatorScore,
		IndicatorGoal,
		StrategicAction,
		[Action],
		Objective,
		StrategicActionResponsibleAuthority,
		StrategicActionEstimatedCost,
		Comments,
		StrategicActionSource,
		StrategicActionScore,
		StrategicActionGoal,
		DetailedActivity,
		[Description],
		StartDate,
		EndDate,
		ImplementationStatus,
		DetailedActivitySource,
		RiskName,
		Deadline,
		Activity,
		Responsible,
		DetailedActivityResponsibleAuthority,
		CostAssumptions,
		DetailedActivityEstimatedCost,
		FundAvailability,
		EstimatedBudgetSource,
		ExistingBudget,
		FinancialGap,
		TechnicalAssistance,
		FinancialAssistance,
		DonorContribution,
		Donor,
		ActualCost,
		DetailedActivityComments,
		PlanIndicatorId,
		StrategicActionFeasibility,
		StrategicActionImpact,
		DetailedActivityFeasibility,
		DetailedActivityImpact,
		PlanIndicatorIdStr)
	SELECT 
		TechnicalArea,
		Indicator,
		IndicatorScore,
		IndicatorGoal,
		[dbo].[DecryptionFunction](StrategicAction, 'YOUCANNOTACCESSTHISKEY'),
		[Action],
		Objective,
		StrategicActionResponsibleAuthority,
		StrategicActionEstimatedCost,
		Comments,
		StrategicActionSource,
		StrategicActionScore,
		StrategicActionGoal,
		[dbo].[DecryptionFunction](DetailedActivity, 'YOUCANNOTACCESSTHISKEY'),
		[Description],
		StartDate,
		EndDate,
		CASE WHEN LOWER(ImplementationStatus) = 'not started' THEN 1
			 WHEN LOWER(ImplementationStatus) = 'just started' THEN 2
			 WHEN LOWER(ImplementationStatus) = 'on-going' THEN 3
			 WHEN LOWER(ImplementationStatus) = 'advanced stage' THEN 4
			 WHEN LOWER(ImplementationStatus) = 'completed' THEN 5
			 WHEN @PlanType = 1 THEN 1 
			 ELSE  0 END,
		DetailedActivitySource,
		RiskName,
		Deadline,
		Activity,
		Responsible,
		DetailedActivityResponsibleAuthority,
		CostAssumptions,
		DetailedActivityEstimatedCost,
		CASE WHEN LOWER(FundAvailability) = 'true' THEN 1
			 ELSE 0 END,
		EstimatedBudgetSource,
		ExistingBudget,
		FinancialGap,
		CASE WHEN LOWER(TechnicalAssistance) = 'true' THEN 1
			 ELSE 0 END,
		CASE WHEN LOWER(FinancialAssistance) = 'true' THEN 1
			 ELSE 0 END,
		DonorContribution,
		Donor,
		ActualCost,
		DetailedActivityComments,
		[dbo].[DecryptionFunction](PlanIndicatorId, 'YOUCANNOTACCESSTHISKEY'),
		StrategicActionFeasibility,
		StrategicActionImpact,
		DetailedActivityFeasibility,
		DetailedActivityImpact,
		PlanIndicatorId
	FROM OPENJSON(@PlanDetails)
	WITH
	(
		TechnicalArea nvarchar(MAX),
		Indicator nvarchar(MAX),
		IndicatorScore int,
		IndicatorGoal int,
		StrategicAction nvarchar(MAX),
		Action nvarchar(MAX),
		Objective nvarchar(MAX),
		StrategicActionResponsibleAuthority nvarchar(MAX),
		StrategicActionEstimatedCost float,
		Comments nvarchar(MAX),
		StrategicActionSource int,
		StrategicActionScore int,
		StrategicActionGoal int,
		DetailedActivity nvarchar(MAX),
		[Description] nvarchar(MAX),
		StartDate datetime,
		EndDate datetime,
		ImplementationStatus nvarchar(MAX),
		DetailedActivitySource int,
		RiskName nvarchar(MAX),
		Deadline nvarchar(MAX),
		Activity nvarchar(MAX),
		Responsible nvarchar(MAX),
		DetailedActivityResponsibleAuthority nvarchar(MAX),
		CostAssumptions nvarchar(MAX),
		DetailedActivityEstimatedCost float,
		FundAvailability nvarchar(MAX),
		EstimatedBudgetSource nvarchar(MAX),
		ExistingBudget float,
		FinancialGap float,
		TechnicalAssistance nvarchar(MAX),
		FinancialAssistance nvarchar(MAX),
		DonorContribution float,
		Donor nvarchar(MAX),
		ActualCost float,
		DetailedActivityComments nvarchar(MAX),
		PlanIndicatorId nvarchar(MAX),
		StrategicActionFeasibility int,
		StrategicActionImpact int,
		DetailedActivityFeasibility int,
		DetailedActivityImpact int
	);

	DECLARE @TotalCounts AS int = (SELECT COUNT(1) FROM @PlanDetailsTable);
	DECLARE @Counter AS int = 1;
	DECLARE @IndicatorGoal int;
	DECLARE @Action nvarchar(MAX);
	DECLARE @Objective nvarchar(MAX);
	DECLARE @StrategicActionResponsibleAuthority nvarchar(MAX);
	DECLARE @StrategicActionEstimatedCost float;
	DECLARE @Comments nvarchar(MAX);
	DECLARE @StrategicActionGoal int;
	DECLARE @Description nvarchar(MAX);
	DECLARE @StartDate datetime;
	DECLARE @EndDate datetime;
	DECLARE @ImplementationStatus int;
	DECLARE @RiskName nvarchar(MAX);
	DECLARE @Deadline nvarchar(MAX);
	DECLARE @Activity nvarchar(MAX);
	DECLARE @Responsible nvarchar(MAX);
	DECLARE @DetailedActivityResponsibleAuthority nvarchar(MAX);
	DECLARE @CostAssumptions nvarchar(MAX);
	DECLARE @DetailedActivityEstimatedCost float;
	DECLARE @FundAvailability nvarchar(MAX);
	DECLARE @ExistingBudget float;
	DECLARE @FinancialGap float;
	DECLARE @TechnicalAssistance bit;
	DECLARE @FinancialAssistance bit;
	DECLARE @DonorContribution float;
	DECLARE @Donor nvarchar(MAX);
	DECLARE @ActualCost float;
	DECLARE @DetailedActivityComments nvarchar(MAX);
	DECLARE @PlanIndicatorId int;
	DECLARE @EstimatedBudgetSource nvarchar(MAX);
	DECLARE @StrategicActionId int;
	DECLARE @DetailedActivityId int;
	DECLARE @TechnicalAreaId int;
	DECLARE @IndicatorId int;
	DECLARE @StrategicActionFeasibility int;
	DECLARE @StrategicActionImpact int;
	DECLARE @DetailedActivityFeasibility int;
	DECLARE @DetailedActivityImpact int;
	DECLARE @IndicatorIdEnc nvarchar(MAX);

	WHILE (@Counter <= @TotalCounts)
	BEGIN

		-- Fetch field from the table.
		SELECT 
			 @IndicatorGoal = IndicatorGoal,
			 @Action = [Action],
			 @Objective = Objective,
			 @StrategicActionResponsibleAuthority = StrategicActionResponsibleAuthority,
			 @StrategicActionEstimatedCost = StrategicActionEstimatedCost,
			 @Comments = Comments,
			 @Description = [Description],
			 @StartDate = StartDate, 
			 @EndDate = EndDate,
			 @ImplementationStatus = ImplementationStatus,
			 @RiskName = RiskName,
			 @Deadline =Deadline,
			 @Activity = Activity,
			 @Responsible = Responsible,
			 @DetailedActivityResponsibleAuthority = DetailedActivityResponsibleAuthority,
			 @CostAssumptions = CostAssumptions,
			 @DetailedActivityEstimatedCost = DetailedActivityEstimatedCost,
			 @FundAvailability = FundAvailability,
			 @EstimatedBudgetSource = EstimatedBudgetSource,
			 @ExistingBudget = ExistingBudget,
			 @FinancialGap =FinancialGap,
			 @TechnicalAssistance = TechnicalAssistance,
			 @FinancialAssistance = FinancialAssistance,
			 @DonorContribution = DonorContribution,
			 @Donor = Donor,
			 @ActualCost = ActualCost,
			 @DetailedActivityComments = DetailedActivityComments,
			 @PlanIndicatorId = PlanIndicatorId,
			 @StrategicActionFeasibility = StrategicActionFeasibility,
			 @StrategicActionImpact = StrategicActionImpact,
			 @DetailedActivityFeasibility = DetailedActivityFeasibility,
			 @DetailedActivityImpact = DetailedActivityImpact,
			 @StrategicActionGoal = IndicatorGoal,
			 @StrategicActionId = StrategicAction,
			 @DetailedActivityId = DetailedActivity,
			 @IndicatorIdEnc = PlanIndicatorIdStr
		FROM @PlanDetailsTable 
		WHERE AutoId = @Counter;	
		
		-- Check if the strategic action is the part of the plan and plan is the part of the country or not.
		DECLARE @IsValidSA AS int = ISNULL((SELECT COUNT(1) FROM StrategicActions SA
								INNER JOIN CountryPlanIndicators CPI
									ON CPI.PlanIndicatorId = SA.PlanIndicatorId
								INNER JOIN CountryPlans CP
									ON CP.CountryPlanId = CPI.CountryPlanId
								WHERE StrategicActionId = @StrategicActionId
								AND CP.CountryPlanId = @PlanId
								AND CP.CountryId = @CountryId), 0);


		IF (@IsValidSA = 0)
		BEGIN
			
			INSERT INTO @RowStatusTable(PlanIndicatorId, IsSuccess, RowStatus)
			VALUES (@IndicatorIdEnc, 0, 'Strategic action is not valid.');

		END
		ELSE IF (@ImplementationStatus = 0)
		BEGIN

			INSERT INTO @RowStatusTable(PlanIndicatorId, IsSuccess, RowStatus)
			VALUES (@IndicatorIdEnc, 0, 'Implementation status is not valid.');	

		END
		ELSE
		BEGIN

			DECLARE @IsValidChange AS bit = 1;

			-- If the plan type is operational plan
			IF (@PlanType = 2)
			BEGIN

				-- Check if the DA is for the same country plan or not and it's for the valid Strategic plan or not.
				DECLARE @IsValidDA AS int = ISNULL((SELECT COUNT(1) FROM DetailedActivities DA
											INNER JOIN StrategicActions SA
												ON DA.StrategicActionId = SA.StrategicActionId
											WHERE SA.StrategicActionId = @StrategicActionId
											AND DA.DetailedActivityId = @DetailedActivityId), 0);				 
    
				IF (@IsValidDA = 0)
				BEGIN

					INSERT INTO @RowStatusTable(PlanIndicatorId, IsSuccess, RowStatus)
					VALUES (@IndicatorIdEnc, 0, 'Detailed activity is not valid.');
					
					SET @IsValidChange = 0;

				END
				-- If plan date is not valid
				-- We will check if the activity date is with in the range of the plan start date and end date
				ELSE IF (NOT (CAST(@StartDate AS date) >= CAST(@PlanStartDate AS date) AND CAST(@EndDate AS date) <= CAST(@PlanEndDate AS date)))
				BEGIN

					INSERT INTO @RowStatusTable(PlanIndicatorId, IsSuccess, RowStatus)
					VALUES (@IndicatorIdEnc, 0, 'Start date and end date is not valid.');
					
					SET @IsValidChange = 0;

				END
			END

			IF (@IsValidChange = 1)
			BEGIN

				BEGIN TRY
				
					-- Update the strategic action.
					UPDATE [dbo].[StrategicActions]
					   SET 
						   [Feasibility] = @StrategicActionFeasibility
						  ,[Impact] = @StrategicActionImpact
						  ,[Priority] = [dbo].[CalculatePriority](@StrategicActionFeasibility, @StrategicActionImpact)
						  ,[ImplementationStatus] = @ImplementationStatus
						  ,[ResponsibleAuthority] = @StrategicActionResponsibleAuthority
						  ,[EstimatedCost] = @StrategicActionEstimatedCost
						  ,[Comments] = @Comments
						  ,[Goal] = @StrategicActionGoal
						  ,[LastUpdatedAt] = GETUTCDATE()
						  ,[LastUpdatedBy] = @UserId
					 WHERE StrategicActionId = @StrategicActionId;

					 IF (@PlanType = 2)
					 BEGIN

						DECLARE @ActivityTags AS nvarchar(MAX) = NULL;
						
						-- Check if activity is exists or not.
						IF (EXISTS (SELECT TOP 1 1 FROM dbo.SplitStringToTable(@Activity, ',')))
						BEGIN
							
							-- Insert the non existing detailed activities type
							INSERT INTO [dbo].[DetailedActivityTypes]
							(
								 [CountryId],
								 [Activity],
								 [CreatedAt],
								 [CreatedBy],
								 [LastUpdatedAt],
								 [LastUpdatedBy]
							)
							SELECT 
								@CountryId,
								Item,
								GETUTCDATE(),
								@UserId,
								GETUTCDATE(),
								@UserId
							FROM dbo.SplitStringToTable(@Activity, ',')
							WHERE Item NOT IN
							(
								SELECT 
									Activity 
								FROM DetailedActivityTypes
								WHERE (CountryId IS NULL OR CountryId = @CountryId)
							);

							SET @ActivityTags = STUFF((
														SELECT ',' + CAST(ActivityTypeId AS nvarchar(MAX))
														FROM DetailedActivityTypes	
														WHERE Activity IN
														(
															SELECT 
																Item 
															FROM dbo.SplitStringToTable(@Activity, ',')
														)
														AND (CountryId IS NULL OR CountryId = @CountryId)
														FOR XML PATH('')
														), 1, 1, '');


						END

						-- Update the detailed activity.
						UPDATE [dbo].[DetailedActivities]
						   SET 						  
							   [Description] = @Description
							  ,[StartDate] = @StartDate
							  ,[EndDate] = @EndDate
							  ,[ImplementationStatus] = @ImplementationStatus
							  ,[Feasibility] = @DetailedActivityFeasibility
							  ,[Impact] = @DetailedActivityImpact
							  ,[Priority] = [dbo].[CalculatePriority](@DetailedActivityFeasibility, @DetailedActivityImpact)
							  ,[ActivityTypeIds] = @ActivityTags
							  ,[RiskName] = @RiskName
							  ,[RiskLevel] = RiskLevel
							  ,[Deadline] = @Deadline
							  ,[Responsible] = @Responsible
							  ,[ResponsibleAuthority] = @DetailedActivityResponsibleAuthority
							  ,[InstituteId] = [InstituteId]
							  ,[CostAssumptions] = @CostAssumptions
							  ,[EstimatedCost] = @DetailedActivityEstimatedCost
							  ,[FundAvailability] = @FundAvailability
							  ,[EstimatedBudgetSource] = @EstimatedBudgetSource
							  ,[ExistingBudget] = @ExistingBudget
							  ,[FinancialGap] = @FinancialGap
							  ,[NeedTechnicalAssistance] = @TechnicalAssistance
							  ,[NeedFinancialAssistance] = @FinancialAssistance
							  ,[DonorContribution] = @DonorContribution
							  ,[Donor] = @Donor
							  ,[ActualCost] = @ActualCost
							  ,[Comments] = @DetailedActivityComments
							  ,[LastUpdatedAt] = GETUTCDATE()
							  ,[LastUpdatedBy] = @UserId
						 WHERE DetailedActivityId = @DetailedActivityId

					 END

						INSERT INTO @RowStatusTable(PlanIndicatorId, IsSuccess, RowStatus)
						VALUES (@IndicatorIdEnc, 1, 'Success.');

				 END TRY
				 BEGIN CATCH

					INSERT INTO @RowStatusTable(PlanIndicatorId, IsSuccess, RowStatus)
					VALUES (@IndicatorIdEnc, 0, 'Something went wrong.');

				 END CATCH

			END

		END
		
		SET @Counter = @Counter + 1;
	END

	SELECT 
		RowStatus,
		IsSuccess,
		PlanIndicatorId
	FROM @RowStatusTable


END

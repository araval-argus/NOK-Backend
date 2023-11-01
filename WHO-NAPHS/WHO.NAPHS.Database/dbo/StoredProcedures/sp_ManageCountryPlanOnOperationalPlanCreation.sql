-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <05-09-2023>
-- Description:	<Manage strategic plan on new operation plan creation.>
-- =============================================

CREATE PROCEDURE sp_ManageCountryPlanOnOperationalPlanCreation
	-- Add the parameters for the stored procedure here
	@PlanId int
AS
BEGIN
	DECLARE @PlanStartDate as datetime;
	DECLARE @PlanEndDate AS datetime;
	DECLARE @CountryId AS int;
	DECLARE @ParentStrategicPlanId AS int;
	
	SELECT TOP 1 
		@CountryId = CountryId,
		@PlanEndDate = PlanEndDate,
		@PlanStartDate = PlanStartDate,
		@ParentStrategicPlanId = StrategicPlanId
	FROM CountryPlans 
	WHERE CountryPlanId = @PlanId
	AND IsDeleted = 0;

	-- Create new strategic plan if no active strategic plan is created.
	IF (NOT EXISTS (SELECT TOP 1 1 FROM CountryPlans 
					WHERE PlanTypeId = 1 
					AND CountryId = @CountryId 
					AND (PlanStatusId = 2 OR PlanStatusId = 3
					AND IsDeleted = 0)))
	BEGIN
		
		DECLARE @NewPlanIdTable AS TABLE
		(
			PlanId int
		);

		INSERT INTO [dbo].[CountryPlans]
           ([Description]
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
           ,[CreatedAt]
           ,[CreatedBy]
           ,[LastUpdatedAt]
           ,[LastUpdatedBy]
           ,[IsDeleted])
	 OUTPUT inserted.CountryPlanId INTO @NewPlanIdTable(PlanId)
     SELECT TOP 1
			[Description]
           ,1
           ,[CountryId]
           ,[PlanStartDate]
           ,DATEADD(YEAR, 5, [PlanStartDate])
           ,[AssessmentTypeId]
           ,[PlanStatusId]
           ,[PlanStageId]
           ,CONCAT([CountryISOCode], '-SP-', YEAR([PlanStartDate]), FORMAT(MONTH([PlanStartDate]), '00'), '-', YEAR(DATEADD(YEAR, 5, [PlanStartDate])), FORMAT(MONTH(DATEADD(YEAR, 5, [PlanStartDate])), '00'))--LY-OP-202309-202509 
           ,[SendReviewReminder]
           ,[AdvancedDaysForReviewReminder]
           ,[CountryISOCode]
           ,[HasOfficiallyApprovedPlan]
           ,[VisibleToAnotherCountries]
           ,GETUTCDATE()
           ,[CreatedBy]
           ,GETUTCDATE()
           ,[LastUpdatedBy]
           ,[IsDeleted]
	FROM CountryPlans
	WHERE CountryPlanId = @PlanId
	AND IsDeleted = 0;

	DECLARE @StrategicPlanId AS int = (SELECT TOP 1 PlanId FROM @NewPlanIdTable);

	IF (@StrategicPlanId > 0)
	BEGIN
		
		DELETE FROM @NewPlanIdTable;

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
		OUTPUT inserted.CountryPlanId INTO @NewPlanIdTable(PlanId)
		SELECT 
			[TechnicalAreaIndicatorId]
           ,@StrategicPlanId
           ,[Score]
           ,[Goal]
           ,[IsDeleted]
           ,GETUTCDATE()
           ,[CreatedBy]
           ,GETUTCDATE()
           ,[LastUpdatedBy]
		FROM CountryPlanIndicators 
		WHERE CountryPlanId = @PlanId
		AND IsDeleted = 0;

		DECLARE @NewAddedStrategicPlan AS int = (SELECT TOP 1 PlanId FROM @NewPlanIdTable)

		IF (@NewAddedStrategicPlan > 0)
		BEGIN

			-- Update operation plan's parent strategic plan id.
			UPDATE CountryPlans
			SET 
				StrategicPlanId = @NewAddedStrategicPlan
			WHERE CountryPlanId = @PlanId;
			
		END

	END


	END
	ELSE -- IF we have already exists strategic plan then we will add strategic action from the SP to the OP
	BEGIN

		-- Add actions of the selected strategic actions from the SP
		IF (@ParentStrategicPlanId > 0)
		BEGIN

			INSERT INTO [dbo].[StrategicActions]
			   ([PlanIndicatorId]
			   ,[Objective]
			   ,[Action]
			   ,[Feasibility]
			   ,[Impact]
			   ,[Priority]
			   ,[ImplementationStatus]
			   ,[ResponsibleAuthority]
			   ,[EstimatedCost]
			   ,[Comments]
			   ,[Source]
			   ,[Score]
			   ,[Goal]
			   ,[CreatedAt]
			   ,[CreatedBy]
			   ,[LastUpdatedAt]
			   ,[LastUpdatedBy]
			   ,[IsDeleted])     
			SELECT 
				(SELECT TOP 1 
					PlanIndicatorId 
				 FROM CountryPlanIndicators 
				 WHERE CountryPlanId = @PlanId 
				 AND TechnicalAreaIndicatorId = CP.TechnicalAreaIndicatorId)
			   ,[Objective]
			   ,[Action]
			   ,[Feasibility]
			   ,[Impact]
			   ,[Priority]
			   ,SA.[ImplementationStatus]
			   ,[ResponsibleAuthority]
			   ,[EstimatedCost]
			   ,[Comments]
			   ,[Source]
			   ,SA.[Score]
			   ,SA.[Goal]
			   ,CP.[CreatedAt]
			   ,CP.[CreatedBy]
			   ,CP.[LastUpdatedAt]
			   ,CP.[LastUpdatedBy],
			   0
			FROM CountryPlanIndicators CP
			INNER JOIN StrategicActions SA
				ON SA.PlanIndicatorId = CP.PlanIndicatorId
			WHERE CountryPlanId = @ParentStrategicPlanId
			AND CP.TechnicalAreaIndicatorId IN
			(
				SELECT 
					TechnicalAreaIndicatorId 
				FROM CountryPlanIndicators 
				WHERE CountryPlanId = @PlanId 
				AND IsDeleted = 0
			);

		END

	END

END
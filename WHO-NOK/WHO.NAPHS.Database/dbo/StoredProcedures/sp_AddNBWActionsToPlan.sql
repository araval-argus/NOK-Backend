-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <29-08-2023>
-- Description:	<Stored Procedure that is used to get NBW recommendations from CountryPlanId>
-- ============================================

CREATE PROCEDURE [dbo].[sp_AddNBWActionsToPlan]
    @NBWActions NVARCHAR(MAX),
	@CountryPlanId INT,
	@UserId INT
AS
BEGIN

	BEGIN TRY

        BEGIN TRANSACTION AddNBWActionsToPlan;

		--STEP 1 : Plan must be operational.
		DECLARE @CountryId AS INT;

		SELECT @CountryId = CountryId
		FROM CountryPlans
		WHERE CountryPlanId = @CountryPlanId
		AND IsDeleted = 0
		AND PlanTypeId = 2 -- operational plan

		IF(@CountryId IS NULL)
		BEGIN
			-- Throw an error for invalid plan ID
			THROW 50001, 'The specified CountryPlanId does not exist or is not an operational plan.', 1
		END

        DECLARE @NBWRecommendations AS TABLE
        (
			AutoId int IDENTITY(1, 1),
            NBWIndicatorId int
        )

		--temporary table to store inserted ids.
		DECLARE @NewIdTable AS TABLE
		(
			Id int
		)

        -- Add data from the parameter passed as string and convert that JSON string into table.
        INSERT INTO @NBWRecommendations(NBWIndicatorId)
        SELECT VALUE FROM OPENJSON(@NBWActions)

        DECLARE @Counter AS int = 1;
        DECLARE @TotalCounts AS int = (SELECT COUNT(1) FROM @NBWRecommendations);

		WHILE (@Counter <= @TotalCounts)
			BEGIN
				DECLARE @NBWIndicatorId AS int = NULL;
				DECLARE @NBWTag AS nvarchar(100) = NULL;
				DECLARE @Objective AS nvarchar(MAX) = NULL;
				DECLARE @ActivityTypeId AS INT = NULL;
				DECLARE @StrategicActionId AS INT = NULL;
				DECLARE @DetailedActivityId AS INT = NULL;

				--get id from current row number
				SELECT
					@NBWIndicatorId = NBWIndicatorId
				FROM @NBWRecommendations WHERE AutoId = @Counter;

				SELECT
					@NBWTag = Tags,
					@Objective = Objective
				FROM NBWRecommendations WHERE NBWRecommendationId = @NBWIndicatorId

				--STEP 2 : check if SA with same objective is present or not
				SELECT TOP 1 @StrategicActionId = SA.StrategicActionId
				FROM StrategicActions SA
				INNER JOIN CountryPlanIndicators CPI
					ON CPI.PlanIndicatorId = SA.PlanIndicatorId AND CPI.IsDeleted = 0
				WHERE Objective = @Objective AND SA.IsDeleted = 0 AND CPI.CountryPlanId = @CountryPlanId

				--STEP 3 : if strategic action is not present then create.
				IF(@StrategicActionId IS NULL)
				BEGIN
					
					INSERT INTO StrategicActions(PlanIndicatorId, Objective, Action, Feasibility, Impact, Priority, ImplementationStatus, ResponsibleAuthority, EstimatedCost, Source, Score, Goal, CreatedAt, LastUpdatedAt, CreatedBy, LastUpdatedBy, IsDeleted)
					OUTPUT inserted.StrategicActionId INTO @NewIdTable(Id)
					SELECT CPI.PlanIndicatorId,
						   NBW.Objective,
						   NBW.StrategicAction,
						   NBW.Feasibility,
						   NBW.Impact,
						   dbo.[CalculatePriority] (NBW.Feasibility, NBW.Impact),
						   1, --not started
						   NBW.ResponsibleAuthority,
						   0, --estimated cost
						   0, --custom source
						   CPI.Score,
						   CPI.Goal,
						   GETUTCDATE(),
						   GETUTCDATE(),
						   @UserId,
						   @UserId,
						   0 -- is deleted

					FROM NBWRecommendations NBW
					JOIN CountryPlans CP
						ON CP.CountryPlanId = @CountryPlanId
					INNER JOIN CommonIndicatorsMapping CIM
						ON CIM.IndicatorId = NBW.IndicatorId AND NBW.IndicatorCode = CIM.IndicatorCode AND CIM.Type = 4 -- NBW Recommendation
					INNER JOIN CommonIndicatorsMapping CIM2
						ON CIM2.CommonIndicatorId = CIM.CommonIndicatorId AND CIM2.Type = CP.AssessmentTypeId
					INNER JOIN TechnicalAreaIndicators TAI
						ON TAI.IndicatorCode = CIM2.IndicatorCode AND TAI.IndicatorCodeId = CIM2.IndicatorId
					INNER JOIN CountryPlanIndicators CPI
						ON CPI.TechnicalAreaIndicatorId = TAI.TechnicalAreaIndicatorId AND CP.CountryPlanId = CPI.CountryPlanId AND CPI.IsDeleted = 0
					WHERE NBW.NBWRecommendationId = @NBWIndicatorId;

					SET @StrategicActionId = (SELECT TOP 1 Id FROM @NewIdTable);
					DELETE FROM @NewIdTable
				END

				IF(@NBWTag IS NOT NULL)
				BEGIN

					-- STEP 4 : check if tag exists or not.
					SELECT TOP 1
						@ActivityTypeId = ActivityTypeId
					FROM DetailedActivityTypes
					WHERE Activity = @NBWTag
					AND (CountryId IS NULL OR CountryId = @CountryId);

					--STEP 5 : if tag is not there then we have to save it first 
					IF(@ActivityTypeId IS NULL)
					BEGIN
						INSERT INTO DetailedActivityTypes (CountryId, Activity, CreatedAt, LastUpdatedAt, CreatedBy, LastUpdatedBy)
						OUTPUT inserted.ActivityTypeId INTO @NewIdTable(Id)
						VALUES (@CountryId, @NBWTag, GETUTCDATE(), GETUTCDATE(), @UserId, @UserId);

						SET @ActivityTypeId = (SELECT TOP 1 Id FROM @NewIdTable);
						DELETE FROM @NewIdTable;
					END

				END

				--STEP 6 : Add detailed activity.
				INSERT INTO DetailedActivities
				(
					StrategicActionId,
					Description,
					ImplementationStatus,
					Feasibility,
					Impact,
					Priority,
					ResponsibleAuthority,
					Source,
					ActivityTypeIds,
					NeedFinancialAssistance,
					NeedTechnicalAssistance,
					EstimatedCost,
					CreatedAt, LastUpdatedAt, CreatedBy, LastUpdatedBy, IsDeleted)
				OUTPUT inserted.DetailedActivityId INTO @NewIdTable(Id)
				SELECT @StrategicActionId,
					NBW.DetailedActivity,
					1, --implementation status (NOT STARTED) 
					SA.Feasibility,
					SA.Impact,
					SA.Priority,
					NBW.ResponsibleAuthority,
					4, --nbw recommendation
					@ActivityTypeId,
					0,--need financial assistance
					0, --need technical assistance
					SA.EstimatedCost,
					GETUTCDATE(), GETUTCDATE(), --createdAt, updatedAt
					@UserId,
					@UserId,
					0 -- isDeleted
				FROM NBWRecommendations NBW, StrategicActions SA
				WHERE NBW.NBWRecommendationId = @NBWIndicatorId
					AND SA.StrategicActionId = @StrategicActionId
					AND NOT EXISTS(
						SELECT TOP 1 1 FROM DetailedActivities
							WHERE StrategicActionId = @StrategicActionId AND Description = NBW.DetailedActivity
					);

				DELETE FROM @NewIdTable

				SET @Counter = @Counter + 1;
			END


        COMMIT TRANSACTION AddNBWActionsToPlan;

    END TRY
    BEGIN CATCH

        IF (@@TRANCOUNT > 0)
        BEGIN

            ROLLBACK TRANSACTION AddNBWActionsToPlan;

        END

        INSERT INTO SQLErrorLogs(ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage,ErrorDate)
        SELECT ERROR_SEVERITY(),ERROR_STATE(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),GETDATE();	

    END CATCH
END
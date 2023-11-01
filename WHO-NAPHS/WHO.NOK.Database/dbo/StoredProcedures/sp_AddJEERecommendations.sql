-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <24-08-2023>
-- Description:	<Add JEE Recommendations to plan indicator.>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddJEERecommendations] 
	-- Add the parameters for the stored procedure here
	@PlanId int,
	@Recommendations nvarchar(MAX),
	@UserId int
AS
BEGIN

	BEGIN TRY

		BEGIN TRANSACTION ManageJEERecommendations;

		-- Declare output table.
		DECLARE @JEERecommendationOutputTable AS TABLE
		(
			[Message] nvarchar(MAX),
			AreaCode nvarchar(50),
			AreaCodeId nvarchar(50),
			AreaName nvarchar(1000),
			IndicatorCode nvarchar(50),
			IndicatorCodeId nvarchar(50),
			IndicatorName nvarchar(1000)
		)

		DECLARE @PlanIndicatorTable AS TABLE
		(
			PlanIndicatorId int,
			Score int,
			Goal int,
			TechnicalAreaIndicatorId int
		)
	
		-- Add data into plan indicator table.
		INSERT INTO @PlanIndicatorTable(PlanIndicatorId, Score, Goal, TechnicalAreaIndicatorId)
		SELECT 
			  PlanIndicatorId, 
			  Score,
			  Goal,
			  TechnicalAreaIndicatorId
		FROM 
		CountryPlanIndicators
		WHERE CountryPlanId = @PlanId
		AND IsDeleted = 0;

		-- Recommendation table.
		DECLARE @RecommendationsTable AS TABLE
		(
			AutoId int IDENTITY(1, 1),
			IndicatorId int,
			JEERecommendation nvarchar(MAX)
		);

		-- Add data from the paramater passed as string and convert that JSON string into table.
		INSERT INTO @RecommendationsTable(IndicatorId, JEERecommendation)
		SELECT 
			IndicatorId,
			JEERecommendation
		FROM 
		OPENJSON(@Recommendations)
		WITH
		(
			IndicatorId int,
			JEERecommendation nvarchar(MAX)
		);

		DECLARE @Counter AS int = 1;
		DECLARE @TotalCounts AS int = (SELECT COUNT(1) FROM @RecommendationsTable);
		DECLARE @IndicatorId int;
		DECLARE @JEERecommendation AS nvarchar(MAX);

		WHILE (@Counter <= @TotalCounts)
		BEGIN

			SELECT 
				@JEERecommendation = JEERecommendation,
				@IndicatorId = IndicatorId
			FROM @RecommendationsTable 
			WHERE AutoId = @Counter;

			-- Check if same name indicator exists in this plan indicator's strategic action.
			IF (NOT EXISTS (SELECT TOP 1 1 FROM StrategicActions SA 
				INNER JOIN @PlanIndicatorTable PIT
					ON PIT.PlanIndicatorId = SA.PlanIndicatorId
				WHERE PIT.TechnicalAreaIndicatorId = @IndicatorId
				AND SA.ReferenceId IS NULL
				AND SA.[Objective] = @JEERecommendation
				AND SA.IsDeleted = 0))
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
					P.PlanIndicatorId,
					@JEERecommendation,
					@JEERecommendation,
					2, -- Medium,
					2, -- Medium,
					3, -- Medium,
					1, --not started
					NULL,
					0,
					NULL,
					1,
					P.Score,
					P.Goal,
					GETUTCDATE(),
					@UserId,
					GETUTCDATE(),
					@UserId,
					0
				 FROM @PlanIndicatorTable P
				 WHERE P.TechnicalAreaIndicatorId = @IndicatorId;

			END
			-- If same description record exists then we will save that in the output table.
			ELSE
			BEGIN

				INSERT INTO @JEERecommendationOutputTable([Message], AreaCode, AreaCodeId, AreaName, IndicatorCode, IndicatorCodeId, IndicatorName)
				SELECT 
					@JEERecommendation,
					TA.AreaCode,
					TA.AreaCodeId,
					TA.[Name],
					TAI.IndicatorCode,
					TAI.IndicatorCodeId,
					TAI.[Name]
				FROM TechnicalAreas TA
				INNER JOIN TechnicalAreaIndicators TAI
					ON TAI.TechnicalAreaId = TA.TechnicalAreaId
				WHERE TAI.TechnicalAreaIndicatorId = @IndicatorId

			END


			SET @Counter = @Counter + 1;

		END

		COMMIT TRANSACTION ManageJEERecommendations;

	END TRY
	BEGIN CATCH

		IF (@@TRANCOUNT > 0)
		BEGIN

			ROLLBACK TRANSACTION ManageJEERecommendations;

		END

		INSERT INTO SQLErrorLogs(ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage,ErrorDate)
		SELECT ERROR_SEVERITY(),ERROR_STATE(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),GETDATE();

	END CATCH

	SELECT '' AS 'OutputTable', * FROM @JEERecommendationOutputTable;
END

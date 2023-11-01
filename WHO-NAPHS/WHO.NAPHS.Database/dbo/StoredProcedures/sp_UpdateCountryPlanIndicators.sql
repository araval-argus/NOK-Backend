-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <30-08-2023>
-- Description:	<Update the plan indicators.>
-- =============================================

CREATE PROCEDURE [dbo].[sp_UpdateCountryPlanIndicators] 
	-- Add the parameters for the stored procedure here
	@Indicators as nvarchar(MAX),
	@PlanId int,
	@UserId int
AS
BEGIN
	
	BEGIN TRY

		BEGIN TRANSACTION SaveIndicators;

		DECLARE @IndicatorTable AS TABLE
		(
			TechnicalAreaIndicatorId int,
			Score int,
			Goal int
		);

		INSERT INTO @IndicatorTable(TechnicalAreaIndicatorId, Score, Goal)
		SELECT 
			TechnicalAreaIndicatorId, 
			Score, 
			Goal
		FROM OPENJSON(@Indicators)
		WITH
		(
			TechnicalAreaIndicatorId int,
			Score int,
			Goal int
		)

		SELECT * FROM @IndicatorTable

		-- Set is deleted flag to true those indicator was removed from the plan.
		UPDATE CountryPlanIndicators
		SET 
		LastUpdatedAt = GETUTCDATE(),
		LastUpdatedBy = @UserId,
		IsDeleted = 1
		WHERE CountryPlanId = @PlanId
		AND TechnicalAreaIndicatorId NOT IN
		(
			SELECT TechnicalAreaIndicatorId FROM @IndicatorTable
		);

		-- Add newly added indicators to the plan.
		INSERT INTO CountryPlanIndicators
			(TechnicalAreaIndicatorId,
			CountryPlanId,
			Score,
			Goal,
			IsDeleted,
			CreatedAt,
			CreatedBy,
			LastUpdatedAt,
			LastUpdatedBy)
		 SELECT 
			TechnicalAreaIndicatorId,
			@PlanId,
			Score,
			Goal,
			0,
			GETUTCDATE(),
			@UserId,
			GETUTCDATE(),
			@UserId
		 FROM @IndicatorTable
		 WHERE
		 TechnicalAreaIndicatorId NOT IN
		 (
			SELECT	
				TechnicalAreaIndicatorId
			FROM CountryPlanIndicators
			WHERE CountryPlanId = @PlanId
			AND IsDeleted = 0
		 );

		COMMIT TRANSACTION SaveIndicators;

		SELECT CAST(1 AS bit) AS 'Status';

	END TRY
	BEGIN CATCH

		IF (@@TRANCOUNT > 0)
		BEGIN

			ROLLBACK TRANSACTION SaveIndicators;
			
		END

		INSERT INTO SQLErrorLogs(ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage,ErrorDate)
		SELECT ERROR_SEVERITY(),ERROR_STATE(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),GETDATE();

		SELECT CAST(0 AS bit) AS 'Status'

	END CATCH

END

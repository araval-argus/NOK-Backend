-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <09-08-2023>
-- Description:	<Update Strategic action estimated cost if Operational activities are added or updated.>
-- =============================================

CREATE PROCEDURE [dbo].[sp_UpdateStrategicActionsEstimatedCost]
	@StrategicActionId INT,
    @UserId INT
AS
BEGIN

	BEGIN TRY

        BEGIN TRANSACTION UpdateSAEstimatedCost;

        --Step 1 : Get sum of all Detailed activities related to strategic action.
		Declare @TotalCost as FLOAT = (SELECT SUM(EstimatedCost)
										FROM DetailedActivities
										WHERE StrategicActionId = @StrategicActionId
										AND EstimatedCost IS NOT NULL
										AND IsDeleted = 0);
		
        --Step 2 : Get Current cost of Strategic action.
        Declare @CurrentCost as FLOAT = (SELECT EstimatedCost FROM StrategicActions WHERE StrategicActionId = @StrategicActionId);

        --Step 3 : Assign default values.
        IF(@TotalCost IS NULL)
		BEGIN
            SET @TotalCost = 0;
		END

		IF(@CurrentCost IS NULL)
		BEGIN
            SET @TotalCost = 0;
		END

        --Step 4 : update estimated cost.
		IF (@CurrentCost < @TotalCost)
		BEGIN
			UPDATE StrategicActions
			SET
				EstimatedCost = @TotalCost,
				LastUpdatedBy = @UserId,
				LastUpdatedAt = GETUTCDATE()
			WHERE StrategicActionId = @StrategicActionId;
		END

        COMMIT TRANSACTION UpdateSAEstimatedCost;

	END TRY
    BEGIN CATCH

        IF (@@TRANCOUNT > 0)
        BEGIN
            ROLLBACK TRANSACTION UpdateSAEstimatedCost;
        END

        INSERT INTO SQLErrorLogs(ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage,ErrorDate)
        SELECT ERROR_SEVERITY(),ERROR_STATE(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),GETDATE(); 

    END CATCH
END

-- =============================================
-- Author:      <Shaishav D. Shukla>
-- Create date: <29-08-2023>
-- Description: <Stored Procedure that is used to get IHR recommendations from CountryPlanId>
-- ============================================

CREATE PROCEDURE sp_AddIHRActionsToPlan
    @IHRActions NVARCHAR(MAX),
    @UserId INT
AS
BEGIN

    BEGIN TRY

        BEGIN TRANSACTION AddIHRActionsToPlan;

        DECLARE @IHRRecommendations AS TABLE
        (
            IHRIndicatorId int,
            PlanIndicatorId int
        );

        -- Add data from the parameter passed as string and convert that JSON string into table.
        INSERT INTO @IHRRecommendations(IHRIndicatorId, PlanIndicatorId)
        SELECT IHRRecommendationId, PlanIndicatorId
        FROM
        OPENJSON(@IHRActions)
        WITH (
            IHRRecommendationId INT,
            PlanIndicatorId INT
        );

        INSERT INTO StrategicActions (PlanIndicatorId, Objective, Action, Feasibility, Impact, Priority, ImplementationStatus, Source, Score, Goal, CreatedAt, LastUpdatedAt, CreatedBy, LastUpdatedBy, IsDeleted)
        SELECT IHR2.PlanIndicatorId, 
               IHR.Objectives,
               IHR.Actions,
               2, --feasibility
               2, --impact
               3, --priority
               1, --not started
               3, --ihr benchmarks
               IHR.PreviousScore,
               IHR.TargetScore,
               GETUTCDATE(),
               GETUTCDATE(),
                @UserId, @UserId, 0
        FROM IHRRecommendations IHR
        INNER JOIN @IHRRecommendations IHR2
            ON IHR.IHRRecommendationId = IHR2.IHRIndicatorId
        WHERE NOT EXISTS (
            SELECT 1
            FROM StrategicActions
            WHERE PlanIndicatorId = IHR2.PlanIndicatorId
              AND Action = IHR.Actions
              AND IsDeleted = 0
        );

        COMMIT TRANSACTION AddIHRActionsToPlan;

    END TRY
    BEGIN CATCH

        IF (@@TRANCOUNT > 0)
        BEGIN

            ROLLBACK TRANSACTION AddIHRActionsToPlan;

        END

        INSERT INTO SQLErrorLogs(ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage,ErrorDate)
        SELECT ERROR_SEVERITY(),ERROR_STATE(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),GETDATE(); 

    END CATCH
END
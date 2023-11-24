-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <09-08-2023>
-- Description:	<Save the technical areas and it's indicators.>
-- =============================================

CREATE PROCEDURE [dbo].[sp_SaveTechnicalAreaAndIndicator] 
	-- Add the parameters for the stored procedure here
	@TechnicalAreas nvarchar(MAX),
	@AreaIndicators nvarchar(MAX),
	@SourceId int,
	@UserId int
AS
BEGIN
	
	BEGIN TRY

		BEGIN TRANSACTION SaveTechnicalAreas;

		-- Update the last added technical area status inactive.
		-- We will only consider the non custom technical areas to be set inactive.
		UPDATE TechnicalAreas 
		SET IsActive = 0,
			LastUpdatedAt = GETUTCDATE(),
			LastUpdatedBy = @UserId
		WHERE SourceId = @SourceId
		AND IsCustomTechnicalArea = 0;

		-- Declare Technical areas temp table
		DECLARE @TechnicalAreasTempTable AS TABLE
		(
			AutoId int IDENTITY(1, 1),
			[Index] int,
			[Value] nvarchar(1000),
			[AreaCode] nvarchar(10),
			[AreaCodeId] nvarchar(10)
		);

		-- Parse the technical area json to the temp table.
		INSERT INTO @TechnicalAreasTempTable([Index], [Value], AreaCode, AreaCodeId)
		SELECT 
			[Index],
			[Value],
			[AreaCode],
			[AreaCodeId]
		FROM 
		OPENJSON(@TechnicalAreas)
		WITH
		(
			[Index] int,
			[Value] nvarchar(1000),
			[AreaCode] nvarchar(10),
			[AreaCodeId] nvarchar(10)
		);

		-- Declare the indicators temp table
		DECLARE @IndicatorsTempTable AS TABLE
		(
			[Value] nvarchar(1000),
			[IndicatorId] nvarchar(10),
			[ParentIndex] int,
			[IndicatorCode] nvarchar(10)
		);

		-- Parse the indicator json to the temp table.
		INSERT INTO @IndicatorsTempTable([ParentIndex], [Value], [IndicatorId], [IndicatorCode])
		SELECT 
			[ParentIndex], 
			[Value], 
			[IndicatorId],
			[IndicatorCode]
		FROM 
		OPENJSON(@AreaIndicators)
		WITH
		(
			[Value] nvarchar(1000),
			[IndicatorId] nvarchar(10),
			[ParentIndex] int,
			[IndicatorCode] nvarchar(10)
		);

		DECLARE @NewAddedArea AS TABLE
		(
			NewAddedId int
		);

		DECLARE @Counter AS int = 1;
		DECLARE @TotalCount AS int = ISNULL((SELECT COUNT(1) FROM @TechnicalAreasTempTable), 0);
		DECLARE @AreaValue AS nvarchar(1000);
		DECLARE @AreaIndex AS int;
		DECLARE @AreaCode AS nvarchar(10);
		DECLARE @AreaCodeId AS nvarchar(10);

		WHILE (@Counter <= @TotalCount)
		BEGIN

			DELETE FROM @NewAddedArea;

			SELECT 
				@AreaValue = [Value],
				@AreaIndex = [Index],
				@AreaCode = [AreaCode],
				@AreaCodeId = [AreaCodeId]
			FROM @TechnicalAreasTempTable
			WHERE AutoId = @Counter;

			DECLARE @CommonAreaId AS int = NULL;

			-- Get the common technical area code from the old record that have same name and same source code.
			IF (EXISTS (SELECT TOP 1 1 FROM TechnicalAreas WHERE [Name] = @AreaValue AND SourceId = @SourceId AND IsActive = 0))
			BEGIN

				SET @CommonAreaId = (SELECT TOP 1 CommonTechnicalAreaId 
										FROM TechnicalAreas 
										WHERE 
										[Name] = @AreaValue 
										AND SourceId = @SourceId 
										ORDER BY TechnicalAreaId DESC);

			END


			-- Add new technical areas.
			INSERT INTO TechnicalAreas([Name], SourceId, AreaCode, AreaCodeId, IsActive, IsCustomTechnicalArea, CommonTechnicalAreaId, CreatedAt, LastUpdatedAt, CreatedBy, LastUpdatedBy)
			OUTPUT inserted.TechnicalAreaId INTO @NewAddedArea(NewAddedId)
			VALUES (@AreaValue, @SourceId, @AreaCode, @AreaCodeId, 1, 0, @CommonAreaId, GETUTCDATE(), GETUTCDATE(), @UserId, @UserId);


			DECLARE @NewAddedAreaId AS int = ISNULL((SELECT TOP 1 NewAddedId FROM @NewAddedArea), 0);

			IF (@NewAddedAreaId > 0)
			BEGIN

				INSERT INTO TechnicalAreaIndicators(IndicatorCode, IndicatorCodeId, [Name], TechnicalAreaId, CreatedAt, LastUpdatedAt, CreatedBy, LastUpdatedBy)
				SELECT 
					[IndicatorCode],
					[IndicatorId],
					[Value],
					@NewAddedAreaId,
					GETUTCDATE(),
					GETUTCDATE(),
					@UserId,
					@UserId
				FROM @IndicatorsTempTable 
				WHERE ParentIndex = @AreaIndex;

			END

			

			SET @Counter =  @Counter + 1;
		END

		COMMIT TRANSACTION SaveTechnicalAreas;

	END TRY
	BEGIN CATCH

		IF (@@TRANCOUNT > 0)
		BEGIN

			ROLLBACK TRANSACTION SaveTechnicalAreas;

		END

		INSERT INTO SQLErrorLogs(ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage,ErrorDate)
		SELECT ERROR_SEVERITY(),ERROR_STATE(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),GETDATE();	

	END CATCH

END

-- =============================================
-- Author:		<Shaishav Shukla>
-- Create date: <30-08-2023>
-- Description:	<Get table from the comma sep string>
-- =============================================

CREATE FUNCTION [dbo].[CalculatePriority]
(
	-- Add the parameters for the function here
	@Feasibility INT,
	@Impact INT
)
RETURNS INT
AS
BEGIN
	DECLARE @Priority as INT = 5; --default

	IF(@Feasibility = 1) --EASY
	BEGIN
		IF(@Impact = 3) SET @Priority = 3 --Impact = Difficult   Priority = Medium
		ELSE IF(@Impact = 1) SET @Priority = 1 --Impact = Easy	Priority = Very high
		ELSE IF(@Impact = 2) SET @Priority = 2 -- Impact = Medium Priority = high
		ELSE SET @Priority = 5
	END
	ELSE IF(@Feasibility = 2) --MEDIUM
	BEGIN
		IF(@Impact = 3) SET @Priority = 4 -- low
		ELSE IF(@Impact = 1) SET @Priority = 2 -- high
		ELSE IF(@Impact = 2) SET @Priority = 3 -- medium
		ELSE SET @Priority = 5
	END
	ELSE IF(@Feasibility = 3) --MEDIUM
	BEGIN
		IF(@Impact = 3) SET @Priority = 5 --very low
		ELSE IF(@Impact = 1) SET @Priority = 3 --medium
		ELSE IF(@Impact = 2) SET @Priority = 4 --low
		ELSE SET @Priority = 5
	END

	RETURN @Priority
END


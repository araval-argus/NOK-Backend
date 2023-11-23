-- =============================================
-- Author:		<Kaif Pipadwala>
-- Create date: <24-10-2023>
-- Description:	<This stored procedure returns the details of all the reviews until next N months>
-- =============================================
CREATE PROCEDURE sp_GetReviews 
	@userId INT, -- Id of the current user.
	@months INT -- months to be added into current date.
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @userRole INT;
	
	SELECT @userRole = UR.RoleId
	FROM UserRoles UR JOIN Users U ON UR.UserId = U.UserId
	WHERE U.UserId = @userId;

	IF (@userRole = 1) -- system admin
	BEGIN

		SELECT CPR.CountryPlanReviewId,
			CPR.CountryPlanId,
			CPR.ReviewDate,
			CPR.ReviewStatus,
			CP.Description,
			CP.PlanTypeId,
			CP.CountryId,
			CP.PlanStartDate,
			CP.PlanEndDate,
			CP.AssessmentTypeId,
			CP.PlanStatusId,
			CP.PlanStageId,
			CP.PlanCode,
			C.Name AS 'CountryName'
		FROM  CountryPlanReviews CPR 
		JOIN CountryPlans CP ON CPR.CountryPlanId = CP.CountryPlanId
		JOIN Countries C ON CP.CountryId = C.CountryId
		WHERE CPR.IsDeleted = CAST(0 AS BIT) AND
				CP.IsDeleted = CAST(0 AS BIT) AND 
				DATEDIFF(DAY, CPR.ReviewDate, DATEADD(MONTH, @months, GETUTCDATE())) >= 0

	END

	ELSE IF(@userRole = 2) -- regional admin
	BEGIN

    -- region of the user.
		DECLARE @region VARCHAR(100); 

		SELECT @region = Region
		FROM Users
		WHERE UserId = @userId;

		SELECT CPR.CountryPlanReviewId,
			CPR.CountryPlanId,
			CPR.ReviewDate,
			CPR.ReviewStatus,
			CP.Description,
			CP.PlanTypeId,
			CP.CountryId,
			CP.PlanStartDate,
			CP.PlanEndDate,
			CP.AssessmentTypeId,
			CP.PlanStatusId,
			CP.PlanStageId,
			CP.PlanCode,
			C.Name AS 'CountryName'
		FROM  CountryPlanReviews CPR 
		JOIN CountryPlans CP ON CPR.CountryPlanId = CP.CountryPlanId
		JOIN Countries C ON CP.CountryId = C.CountryId 
		WHERE C.Region = @region AND 
			CPR.IsDeleted = CAST(0 AS BIT) AND
			CP.IsDeleted = CAST(0 AS BIT) AND
			DATEDIFF(DAY, CPR.ReviewDate, DATEADD(MONTH, @months, GETUTCDATE())) >= 0

	END

	ELSE IF(@userRole = 3) -- country admin
	BEGIN
		
    -- country id of the user.
		DECLARE @countryId INT;
		
		SELECT @countryId = CountryId
		FROM Users
		WHERE UserId = @userId;

		SELECT CPR.CountryPlanReviewId,
			CPR.CountryPlanId,
			CPR.ReviewDate,
			CPR.ReviewStatus,
			CP.Description,
			CP.PlanTypeId,
			CP.CountryId,
			CP.PlanStartDate,
			CP.PlanEndDate,
			CP.AssessmentTypeId,
			CP.PlanStatusId,
			CP.PlanStageId,
			CP.PlanCode,
			C.Name AS 'CountryName'
		FROM  CountryPlanReviews CPR  
		JOIN CountryPlans CP ON CPR.CountryPlanId = CP.CountryPlanId
		JOIN Countries C ON CP.CountryId = C.CountryId 
		WHERE C.CountryId = @countryId AND 
			CPR.IsDeleted = CAST(0 AS BIT) AND 
			CP.IsDeleted = CAST(0 AS BIT) AND
			DATEDIFF(DAY, CPR.ReviewDate, DATEADD(MONTH, @months, GETUTCDATE())) >= 0

	END
END
GO
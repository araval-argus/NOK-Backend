-- =============================================
-- Author:		<Kaif Pipadwala>
-- Create date: <05-10-2023>
-- Description:	<This procedure checks whether the user has permission to perform certain activity or not.>
-- =============================================
CREATE PROCEDURE [dbo].[sp_IsCurrentUserHasPermission]
  @currentUserId INT,
  @activity VARCHAR(100),
  -- Defined in Permissions table. 
  @countryId INT = NULL,
  @planId INT = NULL,
  @strategicActionId INT = NULL,
  @detailedActivityId INT = NULL,
  @userId INT = NULL,
  @region NVARCHAR(100) = NULL
AS
BEGIN

  -- declaring variables for currently logged in user.
  DECLARE @currentUserRolePrecedence INT,
		  @currentUserCountryId INT, 
		  @currentUserRole INT;

  SELECT
    @currentUserRolePrecedence = Roles.Precedence,
    @currentUserCountryId = COALESCE(Users.CountryId, NULL),
    @currentUserRole = UserRoles.RoleId
  From Users
    JOIN UserRoles ON Users.UserId = UserRoles.UserId
    JOIN Roles ON UserRoles.RoleId = Roles.RoleId
  WHERE Users.UserId = @currentUserId

  -- For system admin.
  IF(@currentUserRole = 1)
	  BEGIN
    SELECT CAST(1 AS BIT);
    RETURN;
  END

  DECLARE @minimalRole INT;

  -- Get minimal role to perform the activity.
  SELECT @minimalRole = MinimalRoleId
  FROM Permissions
  WHERE Activity = @activity;

  -- Check if current user role's precedence satisfies the minimality
  IF(@minimalRole > @currentUserRolePrecedence)
	  BEGIN
    SELECT CAST(0 AS BIT);
    RETURN
  END

  -- declaring the variable for country id which is going to be compared for authorization.
  DECLARE @countryIdToBeCompared INT;
  IF (@countryId IS NOT NULL)
		BEGIN
    SET @countryIdToBeCompared = @countryId;
  END
	ELSE IF (@planId IS NOT NULL)
		BEGIN
    SELECT
      @countryIdToBeCompared = CountryId
    FROM CountryPlans
    WHERE CountryPlanId = @planId;
  END
	ELSE IF (@strategicActionId IS NOT NULL)
	BEGIN
    SELECT
      @countryIdToBeCompared = CP.CountryId
    FROM StrategicActions AS SA
      JOIN CountryPlanIndicators AS CPI ON SA.PlanIndicatorId = CPI.PlanIndicatorId
      JOIN CountryPlans AS CP ON CPI.CountryPlanId = CP.CountryPlanId
  END
	ELSE IF (@detailedActivityId IS NOT NULL)
	BEGIN
    SELECT
      @countryIdToBeCompared = CP.CountryId
    FROM DetailedActivities AS DA
      JOIN StrategicActions AS SA ON DA.StrategicActionId = SA.StrategicActionId
      JOIN CountryPlanIndicators AS CPI ON SA.PlanIndicatorId = CPI.CountryPlanId
      JOIN CountryPlans AS CP ON CPI.CountryPlanId = CP.CountryPlanId
  END
	ELSE IF (@userId IS NOT NULL)
	BEGIN

    --If the user id is of system admin or regional admin then any modification by other user except system admin cannot be made.
    IF((SELECT TOP 1
      RoleId
    FROM Users JOIN UserRoles ON Users.UserId = UserRoles.UserId) IN (1, 2))
		BEGIN
      SELECT CAST(0 AS BIT)
    END

    IF ((SELECT TOP 1
      CountryId
    FROM Users
    WHERE UserId = @userId) IS NOT NULL)
		BEGIN
      SELECT
        @countryIdToBeCompared = CountryId
      FROM Users
      WHERE UserId = @userId
    END
  END

  -- For Regional admin.
  IF(@currentUserRole = 2)
	BEGIN

    DECLARE @countryRegion NVARCHAR(10), 
			@userRegion NVARCHAR(10);

    SELECT @userRegion = Region
    FROM Users
    WHERE UserId = @currentUserId

    IF( @region IS NULL )
    BEGIN

      SELECT @countryRegion = Region
      FROM Countries
      WHERE CountryId = @countryIdToBeCompared

    END
    ELSE
      SET @countryRegion = @region

    IF(@userRegion = @countryRegion)
	BEGIN
      SELECT CAST(1 AS BIT);
      RETURN;
    END

    SELECT CAST(0 AS BIT);
    RETURN;

  END

  -- For user limited to country.
  IF(
(@currentUserRole = 3 -- Country Admin
    OR @currentUserRole = 4 -- Country User
    OR @currentUserRole = 5 -- Secretariat
    OR @currentUserRole = 7) -- Country Viewer
    AND @countryIdToBeCompared = @currentUserCountryId) 
BEGIN
    SELECT CAST(1 AS BIT);
    RETURN;
  END

  SELECT CAST(0 AS BIT);
END
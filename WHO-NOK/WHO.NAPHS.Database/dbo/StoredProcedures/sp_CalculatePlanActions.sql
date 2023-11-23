-- =============================================
-- Author:		<Shaishav D. Shukla>
-- Create date: <18-10-2023>
-- Description:	<Stored Procedure that is used to get Number of Actions in a Plan>
-- ============================================

CREATE PROCEDURE [dbo].[sp_CalculatePlanCount]
  @PlanId INT
AS
BEGIN
  DECLARE @planType INT;
  DECLARE @planCount INT;

  -- Get the planType from countryPlans based on planId
  SELECT @planType = PlanTypeId 
  FROM CountryPlans
  WHERE countryPlanId = @PlanId;

  IF (@planType = 1)
  BEGIN
    -- Calculate the actions count for planType SP
    SELECT @planCount = ISNULL((SELECT COUNT(*) FROM StrategicActions sa
                                 INNER JOIN countryPlanIndicators cpi ON sa.PlanIndicatorId = cpi.PlanIndicatorId
                                 INNER JOIN countryPlans cp ON cp.countryPlanId = cpi.countryPlanId
                                 WHERE cp.countryPlanId = @PlanId), 0);
  END
  ELSE IF (@planType = 2)
  BEGIN
    -- Calculate the actions count for planType OP
    SELECT @planCount = ISNULL((SELECT COUNT(*) FROM DetailedActivities da
                                 INNER JOIN StrategicActions sa ON sa.StrategicActionId = da.StrategicActionId
                                 INNER JOIN countryPlanIndicators cpi ON sa.PlanIndicatorId = cpi.PlanIndicatorId
                                 INNER JOIN countryPlans cp ON cp.countryPlanId = cpi.countryPlanId
                                 WHERE cp.countryPlanId = @PlanId), 0);
  END
  ELSE
  BEGIN
    -- Handle other planTypes if needed
    SET @planCount = 0;
  END

  SELECT @planCount as 'PlanCount';
END;
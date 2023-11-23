-- =============================================
-- Author:		<Shaishav Shukla>
-- Create date: <30-08-2023>
-- Description:	<Get table from the comma sep string>
-- =============================================
CREATE FUNCTION [dbo].[SplitStringToTable]
(
	-- Add the parameters for the function here
	@String nvarchar(MAX),
	@Seperator nvarchar(10)
)
RETURNS 
@Table TABLE 
(
	Item nvarchar(MAX)
)
AS
BEGIN
	
	INSERT INTO @Table(Item)
	SELECT value FROM  STRING_SPLIT(@String, @Seperator)
	WHERE LTRIM(RTRIM(value)) <> ''
	
	RETURN 
END


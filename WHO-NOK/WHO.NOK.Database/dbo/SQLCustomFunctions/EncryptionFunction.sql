-- =============================================
-- Author:		<Shaishav Shukla>
-- Create date: <30-08-2023>
-- Description:	<Encrypt given value with provided key.>
-- =============================================
CREATE FUNCTION [dbo].[EncryptionFunction](@Data VARCHAR(MAX), @ManualKey VARCHAR(MAX))
RETURNS VARCHAR(MAX)
AS
BEGIN

	DECLARE @binEncryptedText VARBINARY(MAX) = EncryptByPassPhrase(@ManualKey, @Data)
	DECLARE @strEncryptedText VARCHAR(MAX) = CONVERT(VARCHAR(MAX), @binEncryptedText, 2)

	RETURN @strEncryptedText
END;

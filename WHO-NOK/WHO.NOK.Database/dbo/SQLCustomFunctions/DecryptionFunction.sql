-- =============================================
-- Author:		<Shaishav Shukla>
-- Create date: <30-08-2023>
-- Description:	<Decrypt encrypted data by given key.>
-- =============================================
CREATE FUNCTION [dbo].[DecryptionFunction](@Data VARCHAR(MAX), @ManualKey VARCHAR(MAX))
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @binDecryptedText VARBINARY(MAX) = CONVERT(VARBINARY(MAX), @Data, 2)
	DECLARE @strDecryptedText VARCHAR(MAX) = CONVERT(VARCHAR(MAX), DecryptByPassPhrase(@ManualKey, @binDecryptedText))

	RETURN @strDecryptedText
END;
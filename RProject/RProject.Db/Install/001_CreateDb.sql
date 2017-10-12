DECLARE @dbname NVARCHAR(80);
SET @dbname = N'RPojectDB';

IF (EXISTS (
	SELECT name
	FROM master.dbo.sysdatabases
	WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
	PRINT N'Database ' + @dbname + ' already exists.';
END
ELSE
BEGIN
	EXECUTE('CREATE DATABASE ' + @dbname);
END

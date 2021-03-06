﻿IF(NOT EXISTS(SELECT *
FROM INFORMATION_SCHEMA.SCHEMATA
WHERE SCHEMA_NAME = 'Rights'))
BEGIN
	EXEC ('CREATE SCHEMA Rights;');
	GRANT INSERT ON SCHEMA :: Rights TO dbo; 
END

IF ( NOT EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE
	TABLE_SCHEMA = 'Rights'
	AND TABLE_NAME = 'Users'
))
BEGIN
	CREATE TABLE [Rights].[Users](
		[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
		[IsDeleted] BIT NOT NULL DEFAULT 0,
		[Login] NVARCHAR(50) NOT NULL UNIQUE,
		[FirstName] NVARCHAR(32) NOT NULL,
		[LastName] NVARCHAR(32) NOT NULL,
		[HPWord] NVARCHAR(80) NOT NULL,
		[Roles] INT NOT NULL
	)
END

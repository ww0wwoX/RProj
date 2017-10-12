CREATE PROCEDURE [Rights].[CreateUser]
	@login NVARCHAR(50),
	@firstName NVARCHAR(32),
	@lastName NVARCHAR(32),
	@hpword NVARCHAR(128),
	@roles INT = 1
AS
BEGIN
	INSERT
	INTO [Rights].[Users] (
		[Login]
		, [FirstName]
		, [LastName]
		, [HPWord]
		, [Roles]
	) VALUES (
		@login
		, @firstName
		, @lastName
		, @hpword
		, @roles
	)
END

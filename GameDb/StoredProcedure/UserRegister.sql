CREATE PROCEDURE [dbo].[UserRegister]
	@email VARCHAR(100),
	@password VARCHAR(100),
	@nickname VARCHAR(100)
AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SET @salt = CONCAT(NEWID(), NEWID(), NEWID())

	DECLARE @pwdHash VARBINARY(64)
	SET @pwdHash = HASHBYTES('SHA2_512', CONCAT(@salt, @password, @email, dbo.GetSecretKey()))

	INSERT INTO Users (Email, PasswordHash, Nickname, Salt) VALUES
	(@email, @pwdHash, @nickname, @salt)
END

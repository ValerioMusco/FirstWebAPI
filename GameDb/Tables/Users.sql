﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	Email VARCHAR(100) NOT NULL UNIQUE,
	Nickname VARCHAR(100) NOT NULL UNIQUE,
	PasswordHash VARBINARY(64) NOT NULL,
	Salt VARCHAR(100) NOT NULL,
	RoleId int DEFAULT 1 NOT NULL
)

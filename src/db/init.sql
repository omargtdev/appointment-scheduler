-- init.sql 
--   Create the database schema and all credentials 
--   It's require to execute this file with an superuser, e.g. 'sa'
-- DO NOT CHANGE THIS FILE! (Well, you should not unless you know what you are doing)
--   
--   Credentials:
--     LoginName: AppointmentSchedulerLogin
--     LoginPassword: P@ssw0rd123
--     DatabaseName: AppointmentScheduler
--     DatabaseUser: AppointmentSchedulerUser


USE MASTER;
GO

-- Useful temporals
CREATE PROCEDURE #uspTemp_Print 
@type VARCHAR(10),
@message VARCHAR(255)
AS
PRINT '[' + @type + ']' + ': ' + @message;
GO


-- Creating database and credentials
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'AppointmentSchedulerLogin')
BEGIN
	EXEC #uspTemp_Print 'INFO', 'Creating login "AppointmentSchedulerLogin".';
	CREATE LOGIN AppointmentSchedulerLogin WITH PASSWORD = 'P@ssw0rd123';
END
ELSE
	EXEC #uspTemp_Print 'WARN', '"AppointmentSchedulerLogin" login name already exists! Skipping creation...';
GO


IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = 'AppointmentScheduler')
BEGIN
	EXEC #uspTemp_Print 'INFO', 'Creating database "AppointmentScheduler".';

	CREATE DATABASE AppointmentScheduler;
	USE AppointmentScheduler;

	-- DB Structure
	EXEC #uspTemp_Print 'INFO', 'Creating table "AppointmentScheduler.dbo.User".';
	CREATE TABLE [User] (
        [UserID] int PRIMARY KEY IDENTITY(1, 1),
        [Email] varchar(320) NOT NULL,
        [Password] varchar(255) NOT NULL,
        [Name] varchar(50) NOT NULL,
        [LastName] varchar(100) NOT NULL,
        [Age] int,
        [Active] bit NOT NULL DEFAULT (1)
    );

	EXEC #uspTemp_Print 'INFO', 'Creating table "AppointmentScheduler.dbo.State".';
    CREATE TABLE [AppointmentState] (
        [AppointmentStateID] int PRIMARY KEY,
        [Name] varchar(20) NOT NULL,
        [Description] varchar(200)
    );

	EXEC #uspTemp_Print 'INFO', 'Creating table "AppointmentScheduler.dbo.Contact".';
    CREATE TABLE [Contact] (
        [ContactID] int PRIMARY KEY IDENTITY(1, 1),
        [FullName] varchar(100) NOT NULL,
        [Nickname] varchar(50),
        [PhoneNumber] varchar(15),
        [Email] varchar(320),
        [ContactUserID] int NOT NULL
    );

	EXEC #uspTemp_Print 'INFO', 'Creating table "AppointmentScheduler.dbo.Appointment".';
    CREATE TABLE [Appointment] (
        [AppointmentID] int PRIMARY KEY IDENTITY(1, 1),
        [Subject] varchar(20) NOT NULL,
        [Date] date NOT NULL,
        [Time] time NOT NULL,
        [Notes] text,
        [AppointmentStateID] int NOT NULL,
        [AppointmentContactID] int,
        [AppointmentUserID] int NOT NULL,
        [CreatedAt] datetime NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] datetime
    );

    ALTER TABLE [Contact] ADD CONSTRAINT [FK_Contact_UserID] FOREIGN KEY ([ContactUserID]) REFERENCES [User] ([UserID]);

    ALTER TABLE [Appointment] ADD CONSTRAINT [FK_Appointment_ContactID] FOREIGN KEY ([AppointmentContactID]) REFERENCES [Contact] ([ContactID]);

    ALTER TABLE [Appointment] ADD CONSTRAINT [FK_Appointment_UserID] FOREIGN KEY ([AppointmentUserID]) REFERENCES [User] ([UserID]);

    ALTER TABLE [Appointment] ADD CONSTRAINT [FK_Appointment_AppointmentStateID] FOREIGN KEY ([AppointmentStateID]) REFERENCES [AppointmentState] ([AppointmentStateID]);
END
ELSE
	EXEC #uspTemp_Print 'WARN', '"AppointmentScheduler" database already exists! Skipping creation...';
GO


USE AppointmentScheduler;
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'AppointmentSchedulerUser')
BEGIN
	EXEC #uspTemp_Print 'INFO', 'Creating user "AppointmentSchedulerUser" on database "AppointmentScheduler".';
	CREATE USER AppointmentSchedulerUser FOR LOGIN AppointmentSchedulerLogin;
	GRANT CONTROL ON DATABASE::AppointmentScheduler TO AppointmentSchedulerUser;
END
ELSE
	EXEC #uspTemp_Print 'WARN', '"AppointmentSchedulerUser" user already exists on database "AppointmentScheduler"! Skipping creation...';
GO


-- Removing temporals
USE MASTER;
DROP PROCEDURE #uspTemp_Print;
GO
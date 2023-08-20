-- drop.sql 
--   Delete the database schema and all credentials 
--   It's require to execute this file with an superuser, e.g. 'sa'
-- USE IT WITH PREACUTION, THIS FILE IS VERY DESTRUCTIVE, IT WILL DELETE ALL DATA IN THE DATABASE TOO

USE MASTER;
GO

-- Useful temporals
CREATE PROCEDURE #uspTemp_Print 
@type VARCHAR(10),
@message VARCHAR(255)
AS
PRINT '[' + @type + ']' + ': ' + @message;
GO


-- Deleting database andcredentials
USE MASTER;
IF EXISTS(SELECT 1 FROM sys.databases WHERE name = 'AppointmentScheduler')
BEGIN
	EXEC #uspTemp_Print 'INFO', 'Deleting database "AppointmentScheduler" (Including "AppointmentSchedulerUser" user).';
	DROP DATABASE AppointmentScheduler;
END
ELSE
	EXEC #uspTemp_Print 'WARN', '"AppointmentScheduler" database does not exist! Skipping deletion...';
GO


IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'AppointmentSchedulerLogin')
BEGIN
	EXEC #uspTemp_Print 'INFO', 'Deleting login "AppointmentSchedulerLogin".';
	DROP LOGIN AppointmentSchedulerLogin;
END
ELSE
	EXEC #uspTemp_Print 'WARN', '"AppointmentSchedulerLogin" login name does not exist! Skipping deletion...';
GO

-- Removing temporals
USE MASTER;
DROP PROCEDURE #uspTemp_Print;
GO
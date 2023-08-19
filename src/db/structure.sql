-- Execute this query in administrator mode
USE MASTER;
GO

IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = 'AppointmentScheduler')
BEGIN
    CREATE DATABASE AppointmentScheduler;
    GO

    USE AppointmentScheduler;
    GO

    CREATE TABLE [User] (
        [UserID] int PRIMARY KEY IDENTITY(1, 1),
        [Email] varchar(320) NOT NULL,
        [Password] varchar(255) NOT NULL,
        [Name] varchar(50) NOT NULL,
        [LastName] varchar(100) NOT NULL,
        [Age] int,
        [Active] bit NOT NULL DEFAULT (1)
    )
    GO

    CREATE TABLE [AppointmentState] (
        [AppointmentStateID] int PRIMARY KEY,
        [Name] varchar(20) NOT NULL,
        [Description] varchar(200)
    )
    GO

    CREATE TABLE [Contact] (
        [ContactID] int PRIMARY KEY IDENTITY(1, 1),
        [FullName] varchar(100) NOT NULL,
        [Nickname] varchar(50),
        [PhoneNumber] varchar(15),
        [Email] varchar(320),
        [ContactUserID] int NOT NULL
    )
    GO

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
    )
    GO

    ALTER TABLE [Contact] ADD CONSTRAINT [FK_Contact_UserID] FOREIGN KEY ([ContactUserID]) REFERENCES [User] ([UserID])
    GO

    ALTER TABLE [Appointment] ADD CONSTRAINT [FK_Appointment_ContactID] FOREIGN KEY ([AppointmentContactID]) REFERENCES [Contact] ([ContactID])
    GO

    ALTER TABLE [Appointment] ADD CONSTRAINT [FK_Appointment_UserID] FOREIGN KEY ([AppointmentUserID]) REFERENCES [User] ([UserID])
    GO

    ALTER TABLE [Appointment] ADD CONSTRAINT [FK_Appointment_AppointmentStateID] FOREIGN KEY ([AppointmentStateID]) REFERENCES [AppointmentState] ([AppointmentStateID])
    GO
END

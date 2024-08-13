USE master;

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Payments')
BEGIN
    CREATE DATABASE Payments;
END
GO

USE Payments;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Payment')
BEGIN
    CREATE TABLE Payment(
		Id UNIQUEIDENTIFIER PRIMARY KEY,
		[Description] NVARCHAR(MAX) NOT NULL,
		QtyProducts INT NOT NULL,
		Amount DECIMAL(18, 2) NOT NULL,
		Sender VARCHAR(MAX) NOT NULL,
		Receiver VARCHAR(MAX) NOT NULL,
		[Status] VARCHAR(50) NOT NULL DEFAULT 'Pending'
	);
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name='AddPayment')
BEGIN
 	DROP PROCEDURE [dbo].[AddPayment]
END
GO

CREATE PROCEDURE [dbo].[AddPayment](
	@Description VARCHAR(MAX),
	@QtyProducts VARCHAR(MAX),
	@Amount DECIMAL(18, 2),
	@Sender VARCHAR(MAX),
	@Receiver VARCHAR(MAX),
	@Status VARCHAR(50)
)
AS
BEGIN
	DECLARE @Id UNIQUEIDENTIFIER = NEWID();
	INSERT INTO [dbo].[Payment] (Id, [Description], QtyProducts, Amount, Sender, Receiver, [Status])
	VALUES (@Id, @Description, @QtyProducts, @Amount, @Sender, @Receiver, @Status);

	SELECT @Id;
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name='SetPaymentStatus')
BEGIN
 	DROP PROCEDURE [dbo].[SetPaymentStatus]
END
GO

CREATE PROCEDURE [dbo].[SetPaymentStatus](
	@Id UNIQUEIDENTIFIER,
	@Status VARCHAR(50)
)
AS
BEGIN
	UPDATE [dbo].[Payment]
	SET [Status] = @Status
	WHERE Id = @Id;
END
GO


IF EXISTS (SELECT * FROM sysobjects WHERE name='GetPaymentStatus')
BEGIN
 	DROP PROCEDURE [dbo].[GetPaymentStatus]
END
GO

CREATE PROCEDURE [dbo].[GetPaymentStatus](
	@Id UNIQUEIDENTIFIER
)
AS
BEGIN
	SELECT p.Id paymentId, p.Status
	FROM [dbo].[Payment] p
	WHERE Id = @Id;
END
GO







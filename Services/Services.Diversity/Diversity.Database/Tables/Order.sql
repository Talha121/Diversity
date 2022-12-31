﻿CREATE TABLE [dbo].[Order]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [UserId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [OrderStatus] NVARCHAR(20) NOT NULL,
    [OrderId] UniqueIdentifier NOT NULL default newid(), 
    [CreatedDate] DATETIME NULL, 
    [CompletedDate] DATETIME NULL, 
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_Order_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_Order_UserDetail] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDetail] ([Id])
)

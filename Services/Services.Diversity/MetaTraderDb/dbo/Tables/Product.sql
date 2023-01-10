﻿CREATE TABLE [dbo].[Product] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (MAX) NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [OrderNum]        INT            NULL,
    [Amount]          INT            NULL,
    [Quantity]        INT            NULL,
    [EstimatedReturn] INT            NULL,
    [Commission]      INT            NULL,
    [IsActive]        BIT            NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

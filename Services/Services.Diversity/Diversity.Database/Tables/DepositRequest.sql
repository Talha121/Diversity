CREATE TABLE [dbo].[DepositRequest]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [UserId] INT NOT NULL, 
    [Amount] FLOAT NULL, 
    [Type] NCHAR(20) NULL, 
    [ProofPath] VARCHAR(MAX) NULL, 
    [OtherDetails] NCHAR(10) NULL,
    [Status] NCHAR(20) NULL, 
    CONSTRAINT [PK_DepositRequest] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_DepositRequest_UserDetail] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDetail] ([Id])
)

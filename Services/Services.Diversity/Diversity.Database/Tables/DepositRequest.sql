CREATE TABLE [dbo].[DepositRequest]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [UserId] INT NOT NULL, 
    [Amount] FLOAT NULL, 
    [Type] NVARCHAR(15) NULL, 
    [ProofPath] NVARCHAR(MAX) NULL, 
    [PublicId] NVARCHAR(MAX) NULL,
    [OtherDetails] NVARCHAR(MAX) NULL,
    [Status] NVARCHAR(20) NULL, 
    CONSTRAINT [PK_DepositRequest] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_DepositRequest_UserDetail] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDetail] ([Id])
)

CREATE TABLE [dbo].[WithdrawRequest]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[UserId] INT NOT NULL, 
    [Amount] FLOAT NULL,
    [Status] NCHAR(10) NULL, 
    CONSTRAINT [PK_WithdrawRequest] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_WithdrawRequest_UserDetail] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDetail] ([Id])
)

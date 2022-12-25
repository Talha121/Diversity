CREATE TABLE [dbo].[UserAccount]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [BalanceAmount] FLOAT NULL, 
    [RechargeAmount] FLOAT NULL, 
    [TotalCommission] FLOAT NULL,
    [TotalWithdraw] FLOAT NULL, 
    [TotalDeposit] FLOAT NULL, 
    [UserId] INT NOT NULL,
    CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_UserAccount_UserDetail] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDetail] ([Id])
)

CREATE TABLE [dbo].[UserAccount] (
    [Id]              INT        IDENTITY (1, 1) NOT NULL,
    [BalanceAmount]   FLOAT (53) NULL,
    [RechargeAmount]  FLOAT (53) NULL,
    [TotalCommission] FLOAT (53) NULL,
    [TotalWithdraw]   FLOAT (53) NULL,
    [TotalDeposit]    FLOAT (53) NULL,
    [UserId]          INT        NOT NULL,
    CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserAccount_UserDetail] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDetail] ([Id])
);


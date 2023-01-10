CREATE TABLE [dbo].[BankDetails] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ImagePath] NVARCHAR (MAX) NULL,
    [PublicId]  NVARCHAR (MAX) NULL,
	[AccountTitle] NVARCHAR(MAX) NULL, 
    [AccountNumber] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_BankDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


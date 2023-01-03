CREATE TABLE [dbo].[UserKYC]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [UserId] INT NOT NULL, 
    [IdentityType] NVARCHAR(MAX) NULL, 
    [DocumentNumber] NVARCHAR(MAX) NULL, 
    [DocumentImageOne] NVARCHAR(MAX) NULL, 
    [DocumentImageTwo] NVARCHAR(MAX) NULL,
    [Status] NVARCHAR(20) NULL, 
    CONSTRAINT [PK_UserKYC] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_UserKYC_UserDetail] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDetail] ([Id])
)

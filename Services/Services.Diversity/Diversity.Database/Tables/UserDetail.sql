CREATE TABLE [dbo].[UserDetail]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [Password] NVARCHAR(50) NULL, 
    [ImageUrl] NVARCHAR(MAX) NULL,
    [Role] NVARCHAR(20) NULL,
    [PhoneNumber] NVARCHAR(20) NULL, 
    [IsActive] BIT NOT NULL,
    CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED (Id ASC),
)

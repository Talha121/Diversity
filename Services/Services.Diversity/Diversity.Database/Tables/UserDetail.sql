CREATE TABLE [dbo].[UserDetail]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [Name] NCHAR(50) NULL, 
    [Email] NCHAR(250) NULL, 
    [Password] NCHAR(100) NULL, 
    [ImageUrl] VARCHAR(MAX) NULL,
    [Role] NCHAR(20) NULL,
    [PhoneNumber] NCHAR(20) NULL, 
    [IsActive] BIT NOT NULL,
    CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED (Id ASC),
)

CREATE TABLE [dbo].[UserDetail]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [Name] NCHAR(10) NULL, 
    [Email] NCHAR(10) NULL, 
    [Password] NCHAR(10) NULL, 
    [ImageUrl] NCHAR(10) NULL, 
    [PhoneNumber] NCHAR(10) NULL, 
    [IsActive] BIT NOT NULL,
    CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED (Id ASC),
)

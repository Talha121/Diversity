CREATE TABLE [dbo].[ProductImage]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [ImageName] NCHAR(10) NULL, 
    [ImagePath] NCHAR(10) NULL,
    [ProductId] INT NOT NULL, 
    CONSTRAINT [PK_ProductImage] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_ProductImage_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
)

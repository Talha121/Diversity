CREATE TABLE [dbo].[ProductImage]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [ImageName] NVARCHAR(MAX) NULL, 
    [PublicId] NVARCHAR(MAX) NULL,
    [ImagePath] NVARCHAR(MAX) NULL,
    [ProductId] INT NOT NULL, 
    CONSTRAINT [PK_ProductImage] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_ProductImage_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
)

using AutoMapper;
using Diversity.Application.Models;
using Diversity.Application.Services.Interfaces;
using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        public ProductService(IProductImageRepository productImageRepository,IProductRepository productRepository, IMapper mapper, IFileService fileService)
        {
            this.productImageRepository = productImageRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        public async Task<bool> ChangeProductStatus(int productId,bool IsActive)
        {
            var data = await this.productRepository.GetByIdAsync(productId);
            data.IsActive = IsActive;
            await this.productRepository.UpdateAsync(data);
            return true;
        }

        public async Task<bool> CreateProduct(ProductDTO product)
        {
            // var productData=this.mapper.Map<Product>(product);
            Product productData = new Product()
            {
                Title= product.Title,
                Amount=product.Amount,
                IsActive=true,
                Commission=product.Commission,
                Description=product.Description,
                EstimatedReturn=product.EstimatedReturn,
                OrderNum = product.OrderNum,
                Quantity=product.Quantity
            };
            var productresponse = await this.productRepository.AddAsync(productData);
            
            if(product.ProductImages.Count() > 0)
            {
                List<ProductImage> images = new List<ProductImage>();
                foreach(var item in product.ProductImages)
                {
                    var fileObj = await this.fileService.UploadedFile(item);
                    ProductImage image = new ProductImage()
                    {
                        ImageName = fileObj.Url,
                        ImagePath = fileObj.Url,
                        PublicId = fileObj.PublicId,
                        ProductId = productresponse.Id
                    };
                    images.Add(image);
                }
                var imageResponse = await this.productImageRepository.AddAllImageAsync(images);
               
            }
            return true;
        }

        public async Task<List<GetProductDTO>> GetAllProducts()
        {
            var data = await this.productRepository.GetAllProducts();
            return this.mapper.Map<List<GetProductDTO>>(data);
        }

        public async Task<GetProductDTO> GetProductById(int id)
        {
            var data= await this.productRepository.GetProductById(id);
            return this.mapper.Map<GetProductDTO>(data);
        }

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            //var productData = this.mapper.Map<Product>(product);
            Product productData = new Product()
            {
                Id= (int)product.Id,
                Title = product.Title,
                Amount = product.Amount,
                IsActive =true,
                Commission = product.Commission,
                Description = product.Description,
                EstimatedReturn = product.EstimatedReturn,
                OrderNum = product.OrderNum,
                Quantity = product.Quantity
            };
            var response = this.productRepository.UpdateAsync(productData);
            if (product.ProductImages!=null)
            {
                var deleteImages =await this.productImageRepository.DeleteImagesByProductId((int)product.Id);
                if (deleteImages)
                {
                    List<ProductImage> images = new List<ProductImage>();
                    foreach (var item in product.ProductImages)
                    {
                        var fileObj = await this.fileService.UploadedFile(item);
                        ProductImage image = new ProductImage()
                        {
                            ImageName = fileObj.Url,
                            ImagePath = fileObj.Url,
                            PublicId= fileObj.PublicId,
                            ProductId = (int)product.Id
                        };
                        images.Add(image);
                    }
                    var imageResponse = await this.productImageRepository.AddAllImageAsync(images);
                }
            }
            return true;
        }
    }
}

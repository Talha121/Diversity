using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Diversity.Application.Models;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetAllProducts", Name = "GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var data = await this.productService.GetAllProducts();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductById", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById()
        {
            try
            {
                var data = await this.productService.GetProductById(1);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateProduct", Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm]ProductDTO dto)
        {
            try
            {
                var data = await this.productService.CreateProduct(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateProduct", Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductDTO dto)
        {
            try
            {
                var data = await this.productService.UpdateProduct(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ChangeProductStatus", Name = "ChangeProductStatus")]
        public async Task<IActionResult> ChangeProductStatus(int productId,bool status)
        {
            try
            {
                var data = await this.productService.ChangeProductStatus(productId,status);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

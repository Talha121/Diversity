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
    public class WithdrawController : ControllerBase
    {
        private readonly IWithdrawRequestService withdrawRequestService;
        public WithdrawController(IWithdrawRequestService withdrawRequestService)
        {
            this.withdrawRequestService = withdrawRequestService;
        }

        [HttpGet("WithdrawRequest", Name = "WithdrawRequest")]
        public async Task<IActionResult> GetAllWithdrawRequests()
        {
            try
            {
                var data = await this.withdrawRequestService.GetAllWithdrawRequests();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUserWithdrawRequests", Name = "GetUserWithdrawRequests")]
        public async Task<IActionResult> GetUserWithdrawRequests()
        {
            try
            {
                var data = await this.withdrawRequestService.GetUserWithdrawRequests(12);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateWithdrawRequest", Name = "CreateWithdrawRequest")]
        public async Task<IActionResult> CreateWithdrawRequest(WithdrawRequestDTO dto)
        {
            try
            {
                var data = await this.withdrawRequestService.CreateWithdrawRequest(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateWithdrawRequest", Name = "UpdateWithdrawRequest")]
        public async Task<IActionResult> UpdateWithdrawRequest(WithdrawRequestDTO dto)
        {
            try
            {
                var data = await this.withdrawRequestService.UpdateWithdrawRequest(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

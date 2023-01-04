using Diversity.Application.Models;
using Diversity.Application.Services.Interfaces;
using Diversity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositRequestController : ControllerBase
    {
        private readonly IDepositRequestService depositRequestService;
        public DepositRequestController(IDepositRequestService depositRequestService)
        {
            this.depositRequestService = depositRequestService;

        }
        [HttpGet("DepositRequests", Name = "DepositRequests")]
        public async Task<IActionResult> GetDepositRequests()
        {
            try
            {
                var data = await this.depositRequestService.GetAllDepositRequests();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateDeposit", Name = "CreateDepositRequests")]
        public async Task<IActionResult> CreateDepositRequest([FromForm] DepositRequestDTO depositRequest)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                depositRequest.UserId = int.Parse(userId);
                depositRequest.Status = "Pending";
                var data = await this.depositRequestService.CreateDepositRequest(depositRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getUserDepositRequests", Name = "GetUserDepositRequest")]
        public async Task<IActionResult> getUserDepositRequests()
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var data = await this.depositRequestService.GetUserDepositRequests(int.Parse(userId));
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateDepositRequest", Name = "UpdateDepositRequest")]
        public async Task<IActionResult> UpdateDepositRequest(DepositRequestDTO dto)
        {
            try
            {
                var data = await this.depositRequestService.UpdateDepositRequest(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateBankDetails", Name = "CreateBankDetails")]
        public async Task<IActionResult> CreateBankDetails(IFormFile file)
        {
            try
            {
                var data = await this.depositRequestService.CreateBankDetails(file);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetBankDetails", Name = "GetBankDetails")]
        public async Task<IActionResult> GetBankDetails()
        {
            try
            {
                var data = await this.depositRequestService.GetBankDetails();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Diversity.Application.Models;
using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
                var data = await this.depositRequestService.CreateDepositRequest(depositRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("getUserDepositRequests", Name = "GetUserDepositRequest")]
        public async Task<IActionResult> getUserDepositRequests()
        {
            try
            {
                var data = await this.depositRequestService.GetUserDepositRequests(12);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateDepositRequest", Name = "UpdateDepositRequest")]
        public async Task<IActionResult> UpdateDepositRequest(int? depositId, string status)
        {
            try
            {
                DepositRequestDTO dto = new DepositRequestDTO();
                dto.Id = depositId;
                dto.Status= status;
                var data = await this.depositRequestService.UpdateDepositRequest(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

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
    }
}

using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService userAccountService;
        public UserAccountController(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;

        }
        [HttpGet("GetUserAccountList", Name = "GetUserAccountList")]
        public async Task<IActionResult> GetUserAccountList()
        {
            try
            {
                var data = await this.userAccountService.GetUserAccountList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUserAccountById", Name = "GetUserAccountById")]
        public async Task<IActionResult> GetUserAccountById()
        {
            try
            {
                var data = await this.userAccountService.GetUserAccountById(12);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

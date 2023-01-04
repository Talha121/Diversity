using Diversity.Application.Models;
using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Security.Claims;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserKYCController : ControllerBase
    {
        private readonly IUserKYCService userKYCService;
        public UserKYCController(IUserKYCService userKYCService)
        {
            this.userKYCService = userKYCService;
        }
        [HttpPost("CreateKYC", Name = "CreateKYC")]
        public async Task<IActionResult> CreateKYC([FromForm] UserKYCDTO dto)
        {
            try
            {
                dto.UserId= int.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var data = await this.userKYCService.CreateKYC(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllKYC", Name = "GetAllKYC")]
        public async Task<IActionResult> GetAllKYC()
        {
            try
            {
                var data = await this.userKYCService.GetAllUsersKYC();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetKYCByUser", Name = "GetKYCByUser")]
        public async Task<IActionResult> GetKYCByUser()
        {
            try
            {
                var userId= int.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var data = await this.userKYCService.GetUserKYC(userId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("updatekycstatus", Name = "updatekycstatus")]
        public async Task<IActionResult> updatekycstatus(UserKYCDTO dto)
        {
            try
            {
                var data = await this.userKYCService.UpdateStatus(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

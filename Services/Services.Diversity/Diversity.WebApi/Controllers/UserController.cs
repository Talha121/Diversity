using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Diversity.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDetailService userDetailService;
        public readonly IConfiguration config;
        public UserController(IUserDetailService userDetailService, IConfiguration config)
        {
            this.userDetailService = userDetailService;
            this.config = config;
        }
        [HttpPost("Register", Name = "Register")]
        public async Task<IActionResult> Register(UserDetailDTO userDetail)
        {
            try
            {
                var check = await this.userDetailService.GetUserByEmail(userDetail.Email);
                if (check == null)
                {
                    var user=await this.userDetailService.CreateUser(userDetail);
                    var token=CreateToken(user);
                    return Ok(new { res = token });
                }
                else
                {
                    return BadRequest("User Already Exist With This Email");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDetailDTO userDetail)
        {
            try
            {
                var check = await this.userDetailService.GetUserByEmail(userDetail.Email);
                if (check != null)
                {
                    if(check.Password == userDetail.Password)
                    {
                        var token = CreateToken(check);
                        return Ok(new { res = token });
                    }
                    return BadRequest("Wrong Password");

                }
                else
                {
                    return BadRequest("User does not exist with this email");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CreateToken(UserDetailDTO userDetail)
        {
            var claims = new[]
              {
                          new Claim(ClaimTypes.NameIdentifier, userDetail.Id.ToString()),
                         new Claim(ClaimTypes.Name,userDetail.Name.ToString()),
                          new Claim("Name", userDetail.Name),
                          new Claim(ClaimTypes.Role,userDetail.Role),
                          new Claim("ID",userDetail.Id.ToString())

                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(config.GetSection("JWT:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var Createtoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(Createtoken);

            return token;
        }
    }
}

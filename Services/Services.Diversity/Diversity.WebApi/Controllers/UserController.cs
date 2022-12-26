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
using Diversity.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDetailService userDetailService;
        private readonly IUserAccountService userAccountService;
        private readonly IOrderService orderService;
        public readonly IConfiguration config;
        public readonly IMapper mapper;
        public UserController(IUserDetailService userDetailService, IConfiguration config,IMapper mapper, IUserAccountService userAccountService, IOrderService orderService)
        {
            this.userDetailService = userDetailService;
            this.config = config;
            this.mapper = mapper;
            this.userAccountService = userAccountService;
            this.orderService = orderService;
        }
        [HttpPost("Register", Name = "Register")]
        public async Task<IActionResult> Register(UserLoginDTO userDetail)
        {
            try
            {
                var check = await this.userDetailService.GetUserByEmail(userDetail.Email);
                if (check == null)
                {
                    UserDetailDTO detail = this.mapper.Map<UserDetailDTO>(userDetail);

                    var user=await this.userDetailService.CreateUser(detail);
                    UserAccountDTO useracc = new UserAccountDTO()
                    {
                        UserId = user.Id,
                        BalanceAmount = 0,
                        RechargeAmount = 0,
                        TotalCommission = 0,
                        TotalDeposit = 0,
                        TotalWithdraw = 0
                    };
                    var userAccount = await this.userAccountService.CreateUserAccount(useracc);

                    var userOrder = await this.orderService.CreateOrder((int)user.Id);

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
        public async Task<IActionResult> Login(UserLoginDTO userDetail)
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
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var data =await this.userDetailService.GetAllUsers();
                return Ok(data);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    //IEnumerable<Claim> claims = identity.Claims;
                    // or
                    var UserId = identity.FindFirst("Id").Value;

                    var data = this.userDetailService.GetUserProfile(int.Parse(UserId));
                    return Ok(data);
                }
                return Unauthorized();
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromForm] UserDetailDTO userDetail)
        {
            try
            {
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //if (identity != null)
                //{
                    //IEnumerable<Claim> claims = identity.Claims;
                    // or
                    //var UserId = identity.FindFirst("Id").Value;
                    //userDetail.Id = int.Parse(UserId);
                    var data = this.userDetailService.UpdateUserProfile(userDetail);
                    return Ok(data);
                //}
                //return Unauthorized();

            }
            catch (Exception ex)
            {
                throw ex;
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

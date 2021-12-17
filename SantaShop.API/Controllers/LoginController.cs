using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SantaShop.Core.Interfaces;
using Microsoft.Data.SqlClient;
using SantaShop.Core.TablesModels;
using SantaShop.Core;
using Microsoft.Extensions.Configuration;
using SantaShop.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Data;
using Dapper;
using SantaShop.API.DTO;
using Dapper.Contrib.Extensions;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SantaShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDbConnection _connection;

        public LoginController(IConfiguration config)
        {
            _config = config;
            this._connection = new SqlConnection(_config.GetConnectionString("SantaBD"));
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
            
        [HttpPost("signup")]
        [AllowAnonymous]
        public IActionResult CreateUser([FromBody] CreateDto user)
        {
            IActionResult response = Unauthorized();
            User userCredentials = new User
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Password = user.Password,
                UserRole = user.UserRole,
            };

            long id = _connection.Insert(userCredentials);

            if(id > 0)
            {
                response = NoContent();
            }

            return response;
        }



        private object GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                    new Claim(ClaimTypes.Name, userInfo.FullName.ToString()),
                    new Claim(ClaimTypes.Role, userInfo.UserRole),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"], audience: _config["Jwt:Audience"], claims: claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(LoginDTO login)
        {
            //TODO: Procurar utilizador na Base de Dados
            User user = null;

            var queryParams = new
            {
                UserName = login.UserName,
                Password = login.Password
            };

            string sql = "SELECT * FROM SantaShop.Users WHERE UserName=@UserName AND Password=@Password";

            user = _connection.Query<User>(sql, queryParams).FirstOrDefault();

            return user;
        }
    }
}

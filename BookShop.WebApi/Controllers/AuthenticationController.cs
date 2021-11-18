using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookShop.Configuration.Models;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUsersStorageManager _usersSm;
        private readonly AppConfiguration _configuration;

        public AuthenticationController(IUsersStorageManager usersSm, AppConfiguration configuration)
        {
            _usersSm = usersSm;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _usersSm.GetByLoginModelAsync(model);
            if (user != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Authentication.SecretKey));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration.Authentication.Issuer,
                    audience: _configuration.Authentication.Audience,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: signingCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                user.Token = tokenString;
                return Ok(user);
            }
            return Unauthorized();
        }
    }
}

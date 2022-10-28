using CRUDApplication.API.ApplicationDbContext;
using CRUDApplication.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUDApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AccountController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await CheckIfUserValid(login.Email, login.Password);
            if (user != null)
            {
                var role = await GetRoleNameByRoleId(user.RoleId);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("UserId", Convert.ToString(user.Id), ClaimValueTypes.Integer),
                    new Claim("RoleId",Convert.ToString(user.RoleId), ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.Role, role.Name, ClaimValueTypes.String)

                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }
        private async Task<User> CheckIfUserValid(string email, string password)
        {
            var user = await (from u in _context.User
                              where u.Email == email && u.Password == password
                              select u).FirstOrDefaultAsync();
            return user;
        }

        private async Task<Role> GetRoleNameByRoleId(int id)
        {
            var role = await (from u in _context.User
                              join r in _context.Role on u.RoleId equals r.Id
                              where r.Id == id
                              select r).FirstOrDefaultAsync();
            return role;
        }
    }
}

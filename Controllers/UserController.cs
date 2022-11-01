using CRUDApplication.API.ApplicationDbContext;
using CRUDApplication.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {

            _context = context;
        }

        [HttpGet("GetUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<User>> GetUser()
        {
            var user = await _context.User.ToListAsync();
            return user;
        }

        [HttpPost("CreateUser")]
        [Authorize(Policy = "All")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var isExist = await CheckIfAlreadyExist(user.Email);
            if (!isExist) 
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest("User already exist with provided email...");
            }
        }

        private async Task<bool> CheckIfAlreadyExist(string email)
        {
            var user = await (from u in _context.User
                              where u.Email == email
                              select u).CountAsync();

            return user <= 0 ? false : true;
        }

        private bool CheckIfAlreadyExistt(string email)
        {
            var user = (from u in _context.User
                        where u.Email == email
                        select u).Any();

            return user;
        }
    }
}

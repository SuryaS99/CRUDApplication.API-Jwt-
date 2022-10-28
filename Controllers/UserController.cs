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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {

            _context = context;
        }

        [HttpGet("GetUser")]
        public async Task<IEnumerable<User>> GetUser()
        {
            var user = await _context.User.ToListAsync();
            return user;
        }

        [HttpPost("CreateUser")]
        public async Task<User> CreateUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}

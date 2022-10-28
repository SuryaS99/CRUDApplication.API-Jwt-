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
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RoleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetRole")]
        public async Task<IEnumerable<Role>> GetRole()
        {
            var role = await _context.Role.ToListAsync();
            return role;
        }

        [HttpPost("CreateRole")]
        public async Task<Role> CreateRoleAsync(Role role)
        {
            await _context.Role.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }
    }
}

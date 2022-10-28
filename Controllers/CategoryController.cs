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
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCategory")]
        public async Task<IEnumerable<Category>> GetCategory()
        {
            var cat = await _context.categories.ToListAsync();
            return cat;

        }

        [HttpPost("GetCategoryById")]
        public async Task<Category> GetCategoryById(int id)
        {
            var cat = await _context.categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            return cat;
        }

        [HttpPost("CreateCategory")]
        public async Task<CategoryDto> CreateCategory(CategoryDto category)
        {
            //var cat = await _context.categories.SingleOrDefaultAsync();
            var _category = new Category();
            _category.Name = category.Name;
            _category.IsActive = category.IsActive;
            _context.categories.Add(_category);
           await _context.SaveChangesAsync();
            return category;
        }

        [HttpPut("UpdateCategory")]
        public async Task<Category> UpdateCategory(int id, Category category)
        {
            var cat = await _context.categories.SingleOrDefaultAsync(c => c.CategoryId == id);

            if (cat != null)
            {
                cat.Name = category.Name;
            }
            await _context.SaveChangesAsync();

            return cat;
        }

        [HttpDelete("DeleteCategory")]
        public async Task<Category> DeleteCategory(int id)
        {
            var cat = await _context.categories.SingleOrDefaultAsync(c => c.CategoryId == id);
            if (cat != null)
            {
                _context.categories.Remove(cat);
                await _context.SaveChangesAsync();
            }
            return cat;

        }
        
    }
}

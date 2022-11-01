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
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(policy:"All")]
        //[HttpGet("GetCategory")]
        [HttpGet]
        [Route("Categories")]
        public async Task<IEnumerable<Category>> GetCategory()
        {
            var categories = await _context.categories.ToListAsync();
            return categories;

        }

        [Authorize(policy: "All")]
        //[HttpPost("GetCategoryById")]
        [HttpGet]
        [Route("Category/{id}")]
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            return category;
        }

        [Authorize(Roles ="Admin")]
        //[HttpPost("CreateCategory")]
        [HttpPost]
        [Route("Category")]
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

        [Authorize(Roles = "Admin")]
        //[HttpPut("UpdateCategory")]
        [HttpPut]
        [Route("Category/{id}")]
        public async Task<Category> UpdateCategory(int id, Category category)
        {
            var _category = await _context.categories.SingleOrDefaultAsync(c => c.CategoryId == id);

            if (_category != null)
            {
                _category.Name = category.Name;
            }
            await _context.SaveChangesAsync();

            return _category;
        }

        [Authorize(Roles = "Admin")]
        //[HttpDelete("DeleteCategory")]
        [HttpDelete]
        [Route("Category/{id}")]
        public async Task<Category> DeleteCategory(int id)
        {
            var category = await _context.categories.SingleOrDefaultAsync(c => c.CategoryId == id);
            if (category != null)
            {
                _context.categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return category;

        }
        
    }
}

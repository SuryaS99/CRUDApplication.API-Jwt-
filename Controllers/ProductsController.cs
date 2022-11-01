using CRUDApplication.API.ApplicationDbContext;
using CRUDApplication.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CRUDApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetProduct")]
        public async Task<IEnumerable<Product>> GetProduct()
        {
            var pro = await _context.products.ToListAsync(); 
            return pro;
        }

        [HttpGet("GetProductById")]
        public async Task<Product> GetProductById(int id)
        {
            var pro =await _context.products.SingleOrDefaultAsync(p => p.ProductId == id);
            if (pro == null)
            {
                throw new Exception("Not Found");
            }
            return pro;

        }

        [HttpGet("GetProducts")]
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var pro = await (from p in _context.products
                             join c in _context.categories on p.CategoryId equals c.CategoryId
                             select new ProductDto
                             {
                                  ProductId  = p.ProductId,
                                  Name=p.ProductName,
                                 CategoryName = c.Name
                             }).ToListAsync();
            return pro;
        }




        [HttpPost("CreateProduct")]
        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
               await _context.products.AddAsync(product);
               await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex ;
            }
           
           


            //await _context.products.AddAsync(product);
            // await _context.SaveChangesAsync();
            //  return product;

        }
    }
}

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
            var product = await (from p in _context.products
                             join c in _context.categories on p.CategoryId equals c.CategoryId
                             select new ProductDto
                             {
                                  ProductId  = p.ProductId,
                                  Name=p.ProductName,
                                 CategoryName = c.Name
                             }).ToListAsync();
            if (product != null)
            {
                return product;
            }
            return null;
        }




        [HttpPost("Product")]
        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
               await _context.products.AddAsync(product);
               await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw ex ;
            }
           
        }
        [HttpPut]
        [Route("Product")]
        public async Task<Product> UpdateProduct(ProductDto productDto)
        {
            var product = new Product();
            product.ProductId = productDto.ProductId;
            product.ProductName = productDto.Name;

            await _context.products.FirstOrDefaultAsync(p => p.ProductId == productDto.ProductId);
            if(product != null)
            {
                _context.Entry(productDto).State = EntityState.Modified;
               // _context.products.Update(product);
               await _context.SaveChangesAsync();
                return product;
            }
            return null;
            
           
        }

    }
}

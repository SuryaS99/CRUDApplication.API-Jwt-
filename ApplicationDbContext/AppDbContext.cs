using CRUDApplication.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CRUDApplication.API.ApplicationDbContext
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}

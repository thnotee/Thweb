using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thweb.Model.Model;

namespace Thweb.Data.DbContext
{
    public class ThwebDbContext : IdentityDbContext<IdentityUser>
    {
        public ThwebDbContext(DbContextOptions<ThwebDbContext> options) : base(options)
        {

        }

        public DbSet<ThwebUser> ThwebUsers { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Image> Image { get; set; }

        public DbSet<Cart> Cart { get; set; }

    }
}

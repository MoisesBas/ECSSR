using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ECSSR.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECSSR.DOMAIN
{
    public class ECSSRDbContext:DbContext
    {
        public ECSSRDbContext(DbContextOptions<ECSSRDbContext> dbOptions)
          : base(dbOptions)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

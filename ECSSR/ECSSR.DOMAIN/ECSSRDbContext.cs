using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ECSSR.DOMAIN.Entities;
using ECSSR.UTILITY.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECSSR.DOMAIN
{
    public class ECSSRDbContext:DbContext, IECSSRDbContext
    {
        public ECSSRDbContext(DbContextOptions<ECSSRDbContext> dbOptions)
          : base(dbOptions)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

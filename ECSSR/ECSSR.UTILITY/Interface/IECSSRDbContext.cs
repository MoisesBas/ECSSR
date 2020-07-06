using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECSSR.UTILITY.Interface
{
   
    public interface IECSSRDbContext<T> : IECSSRDbContext where T : DbContext
    {

    }
    public interface IECSSRDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Shopper.Database
{
    public class AdminAppDbContext : DbContext
    {
        public AdminAppDbContext([NotNull] DbContextOptions<AdminAppDbContext> options) : base(options)
        {
        }
    }
}
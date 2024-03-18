using Microsoft.EntityFrameworkCore;
using REST.API.EFCore.Models;

namespace REST.API.EFCore.Context
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
        {

        }
        public DbSet<SampleTable> SampleTables { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}

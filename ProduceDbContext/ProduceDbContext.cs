using Microsoft.EntityFrameworkCore;
using ProduceDb.Entities;
namespace ProduceDb
{
    public class ProduceDbContext : DbContext, IProduceDbContext
    {
        public DbSet<Record> Records => Set<Record>();

        public ProduceDbContext(DbContextOptions options) : base (options) { }
    }
}
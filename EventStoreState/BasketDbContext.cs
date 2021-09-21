using Microsoft.EntityFrameworkCore;

namespace EventStoreState
{
    public class BasketDbContext : DbContext
    {
        public DbSet<BasketState> BasketStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\basketDb.db");
        }
    }
}

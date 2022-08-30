using Microsoft.EntityFrameworkCore;

namespace PostgresEF
{
    public class PostgresContext : DbContext
    {
        public DbSet<WowAuction> WowAuctions { get; set; }
        public DbSet<WowItem> WowItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configurations.PostgresConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
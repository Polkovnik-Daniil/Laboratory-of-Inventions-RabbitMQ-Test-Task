using Laboratory_of_Inventions_RabbitMQ.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Laboratory_of_Inventions_RabbitMQ.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            bool IsCreated = Database.GetService<IRelationalDatabaseCreator>().Exists();
            if (!IsCreated)
            {
                Database.EnsureCreated();
            }
        }
    }
}


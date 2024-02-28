using Laboratory_of_Inventions_RabbitMQ.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Laboratory_of_Inventions_RabbitMQ.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}


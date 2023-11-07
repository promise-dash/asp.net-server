using fullstack_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fullstack_api
{
    public class fullstackDbContext : DbContext
    {
        public fullstackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}

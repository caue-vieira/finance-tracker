using finance_tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace finance_tracker.Context
{
    public class TrackerDbContext : DbContext
    {
        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options)
        {

        }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<User> Users { get; set; }

    }
}

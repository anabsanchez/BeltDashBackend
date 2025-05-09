using BeltDash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeltDash.Infrastructure.Data
{
    public class BeltDashDbContext : DbContext
    {
        public BeltDashDbContext(DbContextOptions<BeltDashDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<User> Users { get; set; }

    }
}

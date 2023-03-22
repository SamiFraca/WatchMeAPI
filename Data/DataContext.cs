using Microsoft.EntityFrameworkCore;
using WatchMe.Models;

namespace WatchMe.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Bar> Bars { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Show> Shows { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>().HasOne(b => b.MyBar).WithOne(e => e.Shows);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WatchMe.Models;
namespace WatchMe.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        public DbSet<Bar> Bars { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}

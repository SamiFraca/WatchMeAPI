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
        DbSet<Bar> Bars { get; set; } = null!;
        DbSet<User> Users { get; set; } = null!;
    }
}

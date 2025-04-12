using Demo2.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo2.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}

using Jinder.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Auth
{
    public class JinderAuthContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        
        public JinderAuthContext(DbContextOptions options) : base(options)
        {
        }

        public JinderAuthContext()
            : base(new DbContextOptionsBuilder().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=JinderAuth;Trusted_Connection=True;").Options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasAlternateKey(a => a.Login);

            modelBuilder.Entity<Account>()
                .HasAlternateKey(a => a.UserId);
        }
    }
}
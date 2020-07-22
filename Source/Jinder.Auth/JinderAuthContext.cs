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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasAlternateKey(a => a.Login);

            modelBuilder.Entity<Account>()
                .HasAlternateKey(a => a.UserId);
        }
    }
}
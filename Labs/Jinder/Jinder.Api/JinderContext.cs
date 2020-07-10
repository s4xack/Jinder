using Jinder.Poco.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Api
{
    public class JinderContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<SummarySuggestion> SummarySuggestions { get; set; }
        public DbSet<VacancySuggestion> VacancySuggestions { get; set; }

        public JinderContext()
        {
            // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Jinder;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SummarySkill>()
                .HasKey(ss => new {ss.SkillName, ss.SummaryId});

            modelBuilder.Entity<VacancySkill>()
                .HasKey(vs => new {vs.SkillName, vs.VacancyId});

            modelBuilder.Entity<Match>()
                .HasAlternateKey(m => new {m.SummaryId, m.VacancyId});

            modelBuilder.Entity<SummarySuggestion>()
                .HasAlternateKey(s => new {s.SummaryId, s.VacancyId});

            modelBuilder.Entity<VacancySuggestion>()
                .HasAlternateKey(v => new {v.SummaryId, v.VacancyId});
        }
    }
}
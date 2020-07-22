using Jinder.Dal.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal
{
    public class JinderContext : DbContext
    {

        public DbSet<DbUser> Users { get; set; }

        public DbSet<DbSkill> Skills { get; set; }
        public DbSet<DbSpecialization> Specializations { get; set; }

        public DbSet<DbSummary> Summaries { get; set; }
        public DbSet<DbVacancy> Vacancies { get; set; }

        public DbSet<DbMatch> Matches { get; set; }

        public DbSet<DbSummarySuggestion> SummarySuggestions { get; set; }
        public DbSet<DbVacancySuggestion> VacancySuggestions { get; set; }

        public JinderContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbSummarySkill>()
                .HasKey(ss => new {ss.SkillId, ss.SummaryId});

            modelBuilder.Entity<DbVacancySkill>()
                .HasKey(vs => new {vs.SkillId, vs.VacancyId});

            modelBuilder.Entity<DbMatch>()
                .HasAlternateKey(m => new {m.SummaryId, m.VacancyId});

            modelBuilder.Entity<DbSummarySuggestion>()
                .HasAlternateKey(s => new {s.SummaryId, s.VacancyId});

            modelBuilder.Entity<DbVacancySuggestion>()
                .HasAlternateKey(v => new {v.SummaryId, v.VacancyId});

            modelBuilder.Entity<DbSkill>()
                .HasAlternateKey(s => s.Name);

            modelBuilder.Entity<DbSpecialization>()
                .HasAlternateKey(s => s.Name);
        }
    }
}
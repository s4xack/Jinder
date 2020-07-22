using System;
using System.ComponentModel.DataAnnotations.Schema;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.DbEntities
{
    public class DbMatch
    {
        public Int32 Id { get; set; }

        [ForeignKey("Summary")]
        public Int32 SummaryId { get; set; }
        public DbSummary Summary { get; set; }

        [ForeignKey("Vacancy")]
        public Int32 VacancyId { get; set; }
        public DbVacancy Vacancy { get; set; }

        public MatchStatus Status { get; set; }

        public DbMatch()
        {
        }

        public static DbMatch FromModel(Match match)
        {
            return new DbMatch()
            {
                SummaryId = match.Summary.Id,
                VacancyId = match.Vacancy.Id,
                Status = match.Status
            };
        }

        public Match ToModel()
        {
            return new Match(Summary.ToModel(), Vacancy.ToModel(), Status) {Id = Id};
        }
    }
}
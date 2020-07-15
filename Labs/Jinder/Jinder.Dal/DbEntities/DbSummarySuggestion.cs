using System;
using System.ComponentModel.DataAnnotations.Schema;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.DbEntities
{
    public class DbSummarySuggestion
    {
        public Int32 Id { get; set; }

        [ForeignKey("Summary")]
        public Int32 SummaryId { get; set; }
        public DbSummary Summary { get; set; }

        [ForeignKey("Vacancy")]
        public Int32 VacancyId { get; set; }
        public DbVacancy Vacancy { get; set; }
        
        public SuggestionStatus Status { get; set; }

        public DbSummarySuggestion()
        {
        }

        public static DbSummarySuggestion FromModel(SummarySuggestion summarySuggestion)
        {
            return new DbSummarySuggestion()
            {
                Id = summarySuggestion.Id,
                SummaryId = summarySuggestion.Summary.Id,
                VacancyId = summarySuggestion.Vacancy.Id,
                Status = summarySuggestion.Status
            };
        }

        public SummarySuggestion ToModel()
        {
            return new SummarySuggestion(Vacancy.ToModel(), Summary.ToModel(), Status) {Id = Id};
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.DbEntities
{
    public class DbVacancySuggestion
    {
        public Int32 Id { get; set; }

        [ForeignKey("Summary")]
        public Int32 SummaryId { get; set; }
        public DbSummary Summary { get; set; }

        [ForeignKey("Vacancy")]
        public Int32 VacancyId { get; set; }
        public DbVacancy Vacancy { get; set; }
        
        public SuggestionStatus Status { get; set; }

        public DbVacancySuggestion()
        {
        }

        public static DbVacancySuggestion FromModel(VacancySuggestion vacancySuggestion)
        {
            return new DbVacancySuggestion()
            {
                Id = vacancySuggestion.Id,
                SummaryId = vacancySuggestion.Summary.Id,
                VacancyId = vacancySuggestion.Vacancy.Id,
                Status = vacancySuggestion.Status
            };
        }

        public VacancySuggestion ToModel()
        {
            return new VacancySuggestion(Summary.ToModel(), Vacancy.ToModel(), Status) {Id = Id};
        }
    }
}
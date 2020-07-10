using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class VacancySuggestion
    {
        public Int32 Id { get; set; }

        [ForeignKey("Vacancy")]
        public Int32 VacancyId { get; private set; }
        public Summary Summary { get; private set; }

        [ForeignKey("Summary")]
        public Int32 SummaryId { get; private set; }
        public Vacancy Vacancy { get; private set; }

        public SuggestionStatus Status { get; private set; }

        public VacancySuggestion()
        {
        }

        public VacancySuggestion(Summary summary, Vacancy vacancy)
        {
            Summary = summary;
            SummaryId = summary.Id;

            Vacancy = vacancy;
            VacancyId = vacancy.Id;

            Status = SuggestionStatus.Ready;
        }

        public VacancySuggestion Copy()
        {
            return new VacancySuggestion(Summary, Vacancy) {Id = Id, Status = Status};
        }

        public void Accept()
        {
            Status = SuggestionStatus.Accepted;
        }

        public void Reject()
        {
            Status = SuggestionStatus.Rejected;
        }

        public void Skip()
        {
            Status = SuggestionStatus.Skipped;
        }

        public void Reset()
        {
            Status = SuggestionStatus.Ready;
        }
    }
}

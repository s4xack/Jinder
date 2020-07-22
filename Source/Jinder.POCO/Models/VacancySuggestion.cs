using System;
using System.Collections.Generic;
using System.Text;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class VacancySuggestion
    {
        public Int32 Id { get; set; }

        public Summary Summary { get; }
        public Vacancy Vacancy { get; }

        public SuggestionStatus Status { get; private set; }

        public VacancySuggestion(Summary summary, Vacancy vacancy, SuggestionStatus status = SuggestionStatus.Ready)
        {
            Summary = summary;
            Vacancy = vacancy;
            Status = status;
        }

        public VacancySuggestion Copy()
        {
            return new VacancySuggestion(Summary, Vacancy) { Status = Status};
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

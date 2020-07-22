using System;
using System.Collections.Generic;
using System.Text;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class SummarySuggestion
    {
        public Int32 Id { get; set; }

        public Summary Summary { get; }
        public Vacancy Vacancy { get; }
        
        public SuggestionStatus Status { get; private set; }

        public SummarySuggestion(Vacancy vacancy, Summary summary, SuggestionStatus status = SuggestionStatus.Ready)
        {
            Vacancy = vacancy;
            Summary = summary;
            Status = status;
        }

        public SummarySuggestion Copy()
        {
            return new SummarySuggestion(Vacancy, Summary) { Status = Status};
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

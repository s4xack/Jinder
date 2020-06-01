using System;
using System.Collections.Generic;
using System.Text;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class SummarySuggestion
    {
        public Int32 Id { get; set; }
        public Int32 VacancyId { get; }
        public Summary Summary { get; }
        public SuggestionStatus Status { get; private set; }

        public SummarySuggestion(Int32 vacancyId, Summary summary)
        {
            VacancyId = vacancyId;
            Summary = summary;
            Status = SuggestionStatus.Ready;
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

using System;
using System.Collections.Generic;
using System.Text;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class Suggestion
    {
        public Int32 SummaryId { get; }
        public Int32 VacancyId { get; }
        public SuggestionStatus Status { get; private set; }

        public Suggestion(Int32 summaryId, Int32 vacancyId)
        {
            SummaryId = summaryId;
            VacancyId = vacancyId;
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

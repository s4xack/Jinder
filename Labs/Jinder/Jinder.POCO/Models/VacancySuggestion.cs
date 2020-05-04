using System;
using System.Collections.Generic;
using System.Text;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class VacancySuggestion
    {
        public Int32 Id { get; }
        public Int32 SummaryId { get; }
        public Vacancy Vacancy { get; }
        public SuggestionStatus Status { get; private set; }

        public VacancySuggestion(Int32 id, Int32 summaryId, Vacancy vacancy)
        {
            Id = id;
            SummaryId = summaryId;
            Vacancy = vacancy;
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

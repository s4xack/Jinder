using System;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class Match
    {
        public Int32 Id { get; set; }
        public Int32 SummaryId { get; }
        public Int32 VacancyId { get; }
        public MatchStatus Status { get; private set; }

        public Match(Int32 summaryId, Int32 vacancyId)
        {
            SummaryId = summaryId;
            VacancyId = vacancyId;
            Status = MatchStatus.Half;
        }

        public void Update()
        {
            Status = MatchStatus.Full;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class Match
    {
        public Int32 Id { get; set; }

        [ForeignKey("Vacancy")]
        public Int32 VacancyId { get; private set; }
        public Summary Summary { get; private set; }

        [ForeignKey("Summary")]
        public Int32 SummaryId { get; private set; }
        public Vacancy Vacancy { get; private set; }
        
        public MatchStatus Status { get; private set; }

        public Match()
        {
        }

        public Match(Summary summary, Vacancy vacancy)
        {
            Summary = summary;
            SummaryId = summary.Id;

            Vacancy = vacancy;
            VacancyId = vacancy.Id;

            Status = MatchStatus.Half;
        }

        public void Update()
        {
            Status = MatchStatus.Full;
        }
    }
}
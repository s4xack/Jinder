using System;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class Match
    {
        public Int32 Id { get; set; }

        public Summary Summary { get; }
        public Vacancy Vacancy { get; }
        
        public MatchStatus Status { get; private set; }

        public Match(Summary summary, Vacancy vacancy, MatchStatus status = MatchStatus.Half)
        {
            Summary = summary;
            Vacancy = vacancy;
            Status = status;
        }

        public void Update()
        {
            Status = MatchStatus.Full;
        }
    }
}